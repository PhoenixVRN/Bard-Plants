using System.Collections;
using Spine.Unity;
using UnityEngine.AI;
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
    public float speedMove;

    private GameModel _gameModel;

    void Start()
    {
        // _agent = GetComponent<NavMeshAgent>();
        // _agent.updateRotation = false;
        // _agent.updateUpAxis = false;

        spineAnimationState = CollectorGnomeAnimation.AnimationState;
        skeleton = CollectorGnomeAnimation.Skeleton;
        spineAnimationState.SetAnimation(0, "Idle", true);
        _gameModel = Reference.GameModel;
        _gameModel.AnimationCollectorGnome.Subscribe(Animation);
    }

    void Update()
    {
        if (WePlant) return;
        if (TargetGardenBed() == null)
        {
            // _agent.SetDestination(idlePoint.position);
            if (Vector2.Distance(transform.position, idlePoint.position) < 0.4)
            {
                _gameModel.AnimationCollectorGnome.Value = eTypeAnimation.Idle;
            }
            else
            {
                directAnim(idlePoint.position);
                transform.position =
                    Vector3.MoveTowards(transform.position, idlePoint.position, speedMove * Time.deltaTime);
            }

            return;
        }

        grydka = TargetGardenBed();
        var e = TargetGardenBed().transform;
        // Debug.Log($"target {e}");
        if (!MoveToGrydka && e != null)
        {
            _target = e;
            MoveToGrydka = true;
        }

        if (MoveToGrydka)
        {
            MoveToTarget();
            if (Vector2.Distance(transform.position, _target.position) < 0.4f)
            {
                if (_target.GetComponent<Grydka>().ripe)
                {
                    WePlant = true;
                    StartCoroutine(SowHarvesting());
                }

                else
                {
                    WePlant = false;
                    MoveToGrydka = false;
                }
            }
        }
    }

    IEnumerator SowHarvesting()
    {
        _gameModel.AnimationCollectorGnome.Value = eTypeAnimation.ActionCicle;
        yield return new WaitForSeconds(3f);
        _target.GetComponent<Grydka>().Harvesting();
        WePlant = false;
        MoveToGrydka = false;
    }

    private Grydka TargetGardenBed()
    {
        var allGrydka = GameManager.instance.currentGrydka.FindAll(c => c.ripe);
        if (allGrydka.Count == 0) return null;
        var randomGrydka = Random.Range(0, allGrydka.Count);
        return allGrydka[Random.Range(0, allGrydka.Count)];
        // return GameManager.instance.allGrydka.Find(c => c.ripe);
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
        transform.position = Vector3.MoveTowards(transform.position, _target.position, speedMove * Time.deltaTime);
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