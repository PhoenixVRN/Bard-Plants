using System.Collections;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectorGnome : MonoBehaviour
{
    public Transform _target;

    public Transform idlePoint;

    // private NavMeshAgent _agent;
    public bool MoveToGrydka;
    public Grydka grydka;
    public bool WePlant;

    public SkeletonAnimation CollectorGnomeAnimation;
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;

    private GameModel _gameModel;
    private Coroutine myCoroutine;

    public float speedMove;
    public float jobTime;
    public float RecreationTime;
    
    public float SpeedCharacteristicsIndex;
    public float JobCharacteristicsIndex;
    public float RecreationCharacteristicsIndex;
    
    private float finalSpeedValue;
    private float finaljobTime;
    private float finalRecreationTime;
    void Start()
    {
        spineAnimationState = CollectorGnomeAnimation.AnimationState;
        skeleton = CollectorGnomeAnimation.Skeleton;
        spineAnimationState.SetAnimation(0, "Idle", true);
        _gameModel = Reference.GameModel;
        _gameModel.AnimationCollectorGnome.Subscribe(Animation);
    }

    void Update()
    {
        if (WePlant) return;
        finalSpeedValue = speedMove * (1 +_gameModel.CollectorGnomeLevel.Value.lvlSpeed * SpeedCharacteristicsIndex);
        if (TargetGardenBed() == null)
        {
            // _agent.SetDestination(idlePoint.position);
            if (Vector2.Distance(transform.position, idlePoint.position) < 0.1)
            {
                // if (_gameModel.AnimationCollectorGnome.Value != eTypeAnimation.Idle)
                // {
                _gameModel.AnimationCollectorGnome.Value = eTypeAnimation.Idle;
                // }
            }
            else
            {
                directAnim(idlePoint.position);
                // if (_gameModel.AnimationCollectorGnome.Value != eTypeAnimation.Walk)
                // {
                _gameModel.AnimationCollectorGnome.Value = eTypeAnimation.Walk;
                // }
                transform.position =
                    Vector3.MoveTowards(transform.position, idlePoint.position,  finalSpeedValue * Time.deltaTime);
            }

            return;
        }

        // Debug.Log($"target {e}");
        if (!MoveToGrydka)
        {
            grydka = TargetGardenBed();
            grydka.DestroyGrydka += () =>
            {
                StopCoroutine(myCoroutine);
                myCoroutine = null;
                MoveToGrydka = false;
                WePlant = false;
            };
            _target = TargetGardenBed().transform;
            MoveToGrydka = true;
        }

        if (MoveToGrydka)
        {
            MoveToTarget();
            if (Vector2.Distance(transform.position, _target.position) < 0.1f)
            {
                WePlant = true;
                myCoroutine = StartCoroutine(SowHarvesting());
            }
        }
    }

    IEnumerator SowHarvesting()
    {
        _gameModel.AnimationCollectorGnome.Value = eTypeAnimation.ActionCicle;
        finaljobTime = jobTime/(1 + JobCharacteristicsIndex * _gameModel.CollectorGnomeLevel.Value.lvlActions);
        yield return new WaitForSeconds(finaljobTime);
        if (WePlant)
        {
            _target.GetComponent<Grydka>().Harvesting();
            //TODO реализовать визуал отдыха
            _gameModel.AnimationCollectorGnome.Value = eTypeAnimation.Idle;
            finalRecreationTime = RecreationTime/(1 + RecreationCharacteristicsIndex * _gameModel.CollectorGnomeLevel.Value.lvlStartAction);
            yield return new WaitForSeconds(finalRecreationTime);
            WePlant = false;
            MoveToGrydka = false;
        }
    }

    private Grydka TargetGardenBed()
    {
        var allGrydka = GameManager.instance.currentGrydka.FindAll(c => c.ripe);
        if (allGrydka.Count == 0) return null;
        float minDist = 500;
        Grydka nearest = null;
        foreach (var gr in allGrydka)
        {
            float distance = Vector2.Distance(transform.position, gr.transform.position);
            if (distance < minDist)
            {
                minDist = distance;
                nearest = gr;
            }
        }

        return nearest;
    }

    public void MoveToTarget()
    {
        // Debug.Log($" _target {_target.position}");
        if (_gameModel.AnimationCollectorGnome.Value != eTypeAnimation.Walk)
        {
            _gameModel.AnimationCollectorGnome.Value = eTypeAnimation.Walk;
        }

        // Debug.Log($" _target {_target.position}");
        directAnim(_target.position);
        transform.position = Vector3.MoveTowards(transform.position, _target.position,  finalSpeedValue * Time.deltaTime);
        // Vector3 r = new Vector3(_target.position.x, _target.position.y, 0);
        // _agent.SetDestination(r);
    }

    private void Animation(eTypeAnimation typeAnimation)
    {
        // Debug.Log($"Anim {typeAnimation.ToString()}");
        switch (typeAnimation)
        {
            case eTypeAnimation.Idle:
                spineAnimationState.SetAnimation(0, "Idle", true);
                break;

            case eTypeAnimation.Walk:
                // Debug.Log($"Walk Anim");
                spineAnimationState.SetAnimation(0, "Walk", true);
                break;

            case eTypeAnimation.ActionCicle:
                spineAnimationState.SetAnimation(0, "Action_cycle", true);
                break;

            case eTypeAnimation.ActionEnd:
                spineAnimationState.SetAnimation(0, "Action_end", true);
                break;

            case eTypeAnimation.ActionStart:
                spineAnimationState.SetAnimation(0, "Action_start", true);
                break;
        }
    }

    private void directAnim(Vector3 target)
    {
        if (target.x > transform.position.x)
        {
            CollectorGnomeAnimation.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            CollectorGnomeAnimation.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}