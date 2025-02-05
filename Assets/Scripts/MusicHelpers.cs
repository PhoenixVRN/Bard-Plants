using System.Collections;
using Spine.Unity;
using UnityEngine.AI;
using UnityEngine;

public class MusicHelpers : MonoBehaviour
{
    public Transform _target;
    public Transform idlePoint;
    private NavMeshAgent _agent;
    public bool MoveToGrydka;
    public Grydka grydka;
    public bool WePlant;
    public SkeletonAnimation LeftGnomeAnimation;
    public Spine.AnimationState spineAnimationState;
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
        spineAnimationState = LeftGnomeAnimation.AnimationState;
        spineAnimationState.SetAnimation(0, "Idle", true);
        _gameModel = Reference.GameModel;
        _gameModel.AnimationMusicHelpers.Subscribe(Animation);
    }

    void Update()
    {
        if (WePlant) return;
        finalSpeedValue = speedMove * (1 + _gameModel.MusicHelpersLevel.Value.lvlSpeed * SpeedCharacteristicsIndex);
        if (TargetGardenBed() == null)
        {
            // DirectAnim(idlePoint.position);
            if (Vector2.Distance(transform.position, idlePoint.position) < 0.1)
            {
                _gameModel.AnimationMusicHelpers.Value = eTypeAnimation.Idle;
            }
            else
            {
                DirectAnim(idlePoint.position);
                _gameModel.AnimationMusicHelpers.Value = eTypeAnimation.Walk;
                transform.position =
                    Vector3.MoveTowards(transform.position, idlePoint.position, finalSpeedValue * Time.deltaTime);
            }

            // _agent.SetDestination(idlePoint.position);
            return;
        }

        grydka = TargetGardenBed();
        var e = TargetGardenBed().transform;
        // Debug.Log($"target {e}");
        if (!MoveToGrydka)
        {
            grydka = TargetGardenBed();
            grydka.MusicOff += () =>
            {
                if (myCoroutine != null)
                {
                    StopCoroutine(myCoroutine);
                }

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
            if (Vector2.Distance(transform.position, _target.position) < 0.1)
            {
                WePlant = true;
                myCoroutine = StartCoroutine(SowHarvesting());
            }
        }
    }

    IEnumerator SowHarvesting()
    {
        _gameModel.AnimationMusicHelpers.Value = eTypeAnimation.ActionCicle;
        finaljobTime = jobTime / (1 + JobCharacteristicsIndex * _gameModel.MusicHelpersLevel.Value.lvlActions);
        yield return new WaitForSeconds(finaljobTime);
        if (WePlant)
        {
            _target.GetComponent<Grydka>().PlayMusic();
            //TODO реализовать визуал отдыха
            _gameModel.AnimationMusicHelpers.Value = eTypeAnimation.Idle;
            finalRecreationTime = RecreationTime /
                                  (1 + RecreationCharacteristicsIndex *
                                      _gameModel.MusicHelpersLevel.Value.lvlStartAction);
            yield return new WaitForSeconds(finalRecreationTime);
            WePlant = false;
            MoveToGrydka = false;
        }
    }

    private Grydka TargetGardenBed()
    {
        var allGrydka = GameManager.instance.currentGrydka.FindAll(c => c.needMusic);
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
        if (_gameModel.AnimationMusicHelpers.Value != eTypeAnimation.Walk)
        {
            _gameModel.AnimationMusicHelpers.Value = eTypeAnimation.Walk;
        }

        DirectAnim(_target.position);
        transform.position =
            Vector3.MoveTowards(transform.position, _target.position, finalSpeedValue * Time.deltaTime);
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

    private void DirectAnim(Vector3 target)
    {
        if (target.x > transform.position.x)
        {
            LeftGnomeAnimation.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            LeftGnomeAnimation.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}