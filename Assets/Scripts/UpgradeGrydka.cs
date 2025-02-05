using UnityEngine;

public class UpgradeGrydka : MonoBehaviour
{
    private Grydka grydka;

    private void Start()
    {
        grydka = GetComponentInParent<Grydka>();
    }

    private void OnMouseDown()
    {
        grydka.UpgradeGrydka();
    }
}