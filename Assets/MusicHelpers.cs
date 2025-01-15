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
    public float speedMove;
    public SkeletonAnimation LeftGnomeAnimation;
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    private GameModel _gameModel;

    void Start()
    {
        spineAnimationState = LeftGnomeAnimation.AnimationState;
        skeleton = LeftGnomeAnimation.Skeleton;
        spineAnimationState.SetAnimation(0, "Idle", true);
        _gameModel = Reference.GameModel;
        _gameModel.AnimationMusicHelpers.Subscribe(Animation);
        // _agent = GetComponent<NavMeshAgent>();
        // _agent.updateRotation = false;
        // _agent.updateUpAxis = false;
    }

    void Update()
    {
        if (WePlant) return;
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
                    Vector3.MoveTowards(transform.position, idlePoint.position, speedMove * Time.deltaTime);
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
                // if (_target.GetComponent<Grydka>().needMusic)
                // {
                    WePlant = true;
                    StartCoroutine(SowHarvesting());
                // }
                // else
                // {
                //     WePlant = false;
                //     MoveToGrydka = false;
                // }
            }
        }
    }

    IEnumerator SowHarvesting()
    {
        _gameModel.AnimationMusicHelpers.Value = eTypeAnimation.ActionCicle;
        yield return new WaitForSeconds(2f);
        _target.GetComponent<Grydka>().PlayMusic();
        WePlant = false;
        MoveToGrydka = false;
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
        transform.position = Vector3.MoveTowards(transform.position, _target.position, speedMove * Time.deltaTime);
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