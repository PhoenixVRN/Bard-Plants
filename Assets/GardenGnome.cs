using System.Collections;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UnityEngine.AI;

public class GardenGnome : MonoBehaviour
{
    public Vector2 _target;

    public Transform idlePoint;

    // private NavMeshAgent _agent;
    public bool MoveToGrydka;
    public Grydka grydka;
    public bool WePlant;
    public SkeletonAnimation LeftGnomeAnimation;
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    public float speedMove;

    private GameModel _gameModel;

    private Vector3 oldPos;

    void Start()
    {
        oldPos = transform.position;
        // _agent = GetComponent<NavMeshAgent>();
        // _agent.updateRotation = false;
        // _agent.updateUpAxis = false;
        spineAnimationState = LeftGnomeAnimation.AnimationState;
        skeleton = LeftGnomeAnimation.Skeleton;
        spineAnimationState.SetAnimation(0, "Idle", true);
        _gameModel = Reference.GameModel;
        _gameModel.AnimationGardenGnome.Subscribe(Animation);
    }

    void Update()
    {
        // Debug.Log($"delta {Vector3.Distance(transform.position, oldPos)}");
        oldPos = transform.position;
        if (WePlant) return;
        // if (EmptyGardenBed() == null)
        if (GameManager.instance.currentGrydka.Count >= Reference.GameModel.MaxNumberPlants.Value)
        {
            // _agent.SetDestination(idlePoint.position);
            if (Vector2.Distance(transform.position, idlePoint.position) < 0.4)
            {
                _gameModel.AnimationGardenGnome.Value = eTypeAnimation.Idle;
            }
            else
            {
                // Debug.Log($"GoTo IdlePos");
                DirectAnim(idlePoint.position);
                transform.position =
                    Vector3.MoveTowards(transform.position, idlePoint.position, speedMove * Time.deltaTime);
            }

            return;
        }

        if (!MoveToGrydka)
        {
            _target = GameManager.instance.SpawnPositionPlant();
            MoveToGrydka = true;
        }
        // var e = EmptyGardenBed().transform;
        // Debug.Log($"target {e}");
        // if (!MoveToGrydka && e != null)
        // {
        // _target = e;
        //     MoveToGrydka = true;
        // }


        if (MoveToGrydka)
        {
            if (Vector2.Distance(transform.position, _target) < 0.4)
            {
                    WePlant = true;
                    StartCoroutine(WePlantPlant());
            }
            else
            {
                MoveToTarget();
            }
        }
    }

    IEnumerator WePlantPlant()
    {
        _gameModel.AnimationGardenGnome.Value = eTypeAnimation.ActionCicle;
        yield return new WaitForSeconds(3f);
       
        GameManager.instance.PlantAplant(_target);
        WePlant = false;
        MoveToGrydka = false;
    }

    private Grydka EmptyGardenBed()
    {
        var allGrydka = GameManager.instance.currentGrydka.FindAll(c => c.empty == false);
        if (allGrydka.Count == 0) return null;
        var randomGrydka = Random.Range(0, allGrydka.Count);
        return allGrydka[Random.Range(0, allGrydka.Count)];
        // return GameManager.instance.allGrydka.Find(c => c.empty == false);
    }

    public void MoveToTarget()
    {
        if (_gameModel.AnimationGardenGnome.Value != eTypeAnimation.Walk)
        {
            _gameModel.AnimationGardenGnome.Value = eTypeAnimation.Walk;
        }

        DirectAnim(_target);
        transform.position = Vector3.MoveTowards(transform.position, _target, speedMove * Time.deltaTime);
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