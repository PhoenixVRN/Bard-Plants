using DG.Tweening;
using UnityEngine;

public class Fetus : MonoBehaviour
{
    public ETypePlant typePlant;
    public bool NonInteractive;
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (!NonInteractive) return;
    //     if (other.name.Contains("Player") || other.name.Contains("CollectorGnome"))
    //     {
    //         Debug.Log($"NAme {other.name}");
    //        transform.DOMove(other.transform.position + new Vector3(0, 0.5f, 0f), 0.2f).OnComplete(() =>
    //        {
    //            Bag.instance.AddPlants(typePlant, 1);
    //            Debug.Log($"Destructed {other.name}");
    //            Destroy(gameObject);
    //        });
    //     }
    // }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!NonInteractive) return;
        if (other.name.Contains("Player") || other.name.Contains("CollectorGnome"))
        {
            Debug.Log($"NAme {other.name}");
            transform.DOMove(other.transform.position + new Vector3(0, 0.5f, 0f), 0.2f).OnComplete(() =>
            {
                Bag.instance.AddPlants(typePlant, 1);
                Debug.Log($"Destructed {other.name}");
                Destroy(gameObject);
            });
        }
    }
}
