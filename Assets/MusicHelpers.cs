using System.Collections;
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

    void Start()
    {
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
            if (Vector2.Distance(transform.position, idlePoint.position) < 0.4)
            {
                // _gameModel.AnimationGardenGnome.Value = eTypeAnimation.Idle;
            }
            else
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, idlePoint.position, speedMove * Time.deltaTime);
            }

            // _agent.SetDestination(idlePoint.position);
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
            if (Vector2.Distance(transform.position, _target.position) < 0.4)
            {
                if (_target.GetComponent<Grydka>().needMusic)
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
        yield return new WaitForSeconds(2f);
        _target.GetComponent<Grydka>().PlayMusic();
        WePlant = false;
        MoveToGrydka = false;
    }

    private Grydka TargetGardenBed()
    {
        var allGrydka = GameManager.instance.currentGrydka.FindAll(c => c.needMusic);
        if (allGrydka.Count == 0) return null;
        var randomGrydka = Random.Range(0, allGrydka.Count);
        return allGrydka[Random.Range(0, allGrydka.Count)];
    }

    public void MoveToTarget()
    {
        // Debug.Log($" _target {_target.position}");
        // Vector3 r = new Vector3(_target.position.x, _target.position.y, 0);
        transform.position = Vector3.MoveTowards(transform.position, _target.position, speedMove * Time.deltaTime);
        // _agent.SetDestination(r);
    }

    // private void DirectAnim(Vector3 target)
    // {
    //     if (target.x > transform.position.x)
    //     {
    //         LeftGnomeAnimation.gameObject.transform.localScale = new Vector3(-1, 1, 1);
    //     }
    //     else
    //     {
    //         LeftGnomeAnimation.gameObject.transform.localScale = new Vector3(1, 1, 1);
    //     }
    // }
}