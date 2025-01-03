using System.Collections;
using Spine.Unity;
using UnityEngine;
using UnityEngine.AI;

public class GardenGnome : MonoBehaviour
{
    public Transform _target;

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

    void Start()
    {
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
        if (WePlant) return;
        if (EmptyGardenBed() == null)
        {
            // _agent.SetDestination(idlePoint.position);
            if (Vector2.Distance(transform.position, idlePoint.position) < 0.4)
            {
                _gameModel.AnimationGardenGnome.Value = eTypeAnimation.Idle;
            }
            else
            {
                DirectAnim(idlePoint.position);
                transform.position =
                    Vector3.MoveTowards(transform.position, idlePoint.position, speedMove * Time.deltaTime);
            }

            return;
        }

        var e = EmptyGardenBed().transform;
        // Debug.Log($"target {e}");
        if (!MoveToGrydka && e != null)
        {
            _target = e;
            MoveToGrydka = true;
        }

        if (MoveToGrydka)
        {
            MoveToTarget();
            if (Vector2.Distance(transform.position, _target.position) < 0.4)
            {
                if (!_target.GetComponent<Grydka>().empty)
                {
                    WePlant = true;
                    StartCoroutine(WePlantPlant());
                }
                else
                {
                    WePlant = false;
                    MoveToGrydka = false;
                }
            }
        }
    }

    IEnumerator WePlantPlant()
    {
        _gameModel.AnimationGardenGnome.Value = eTypeAnimation.ActionCicle;
        yield return new WaitForSeconds(3f);
        _target.GetComponent<Grydka>().PlantaPlant();
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

        // Debug.Log($" _target {_target.position}");
        DirectAnim(_target.position);
        // Vector3 r = new Vector3(_target.position.x, _target.position.y, 0);
        // Vector3 direction = _target.position - transform.position;
        // direction.Normalize();
        // transform.position += direction * speedMove * Time.deltaTime;
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