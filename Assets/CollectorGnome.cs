using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class CollectorGnome : MonoBehaviour
{
    public Transform _target;
    public Transform idlePoint;
    private NavMeshAgent _agent;
    public bool MoveToGrydka;
    public Grydka grydka;
    public bool WePlant;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    void Update()
    {
        if (WePlant) return;
        if (TargetGardenBed() == null)
        {
            _agent.SetDestination(idlePoint.position);
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
            if (Vector2.Distance(transform.position, _target.position) < 1)
            {
                WePlant = true;
                StartCoroutine( SowHarvesting());
            }
        }
    }
    
    IEnumerator  SowHarvesting()
    {
        yield return new WaitForSeconds(2f);
        _target.GetComponent<Grydka>().Harvesting();
        WePlant = false;
        MoveToGrydka = false;
    }

    private Grydka TargetGardenBed()
    {
        return GameManager.instance.allGrydka.Find(c => c.ripe);
    }

    public void MoveToTarget()
    {
        // Debug.Log($" _target {_target.position}");
        Vector3 r = new Vector3(_target.position.x, _target.position.y, 0);
        _agent.SetDestination(r);
    }
}
