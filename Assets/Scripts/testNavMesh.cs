using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testNavMesh : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        _navMeshAgent.SetDestination(target.position);
    }
}
