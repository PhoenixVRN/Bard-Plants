using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public Transform target;
    public float speedMove;
    void Start()
    {
        
    }

  
    void Update()
    {
        // transform.position = Vector3.Lerp(transform.position, target.position, speedMove * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speedMove*Time.deltaTime);
    }
}
