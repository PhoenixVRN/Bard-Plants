using UnityEngine;

public class DetectPlant : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Grydka>())
        {
            Debug.Log($"Player {other.name} entered");
        }
    }
}
