using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 temp = transform.position;
        temp.y = _player.position.y + 1.5f;
        temp.x = _player.position.x;


        transform.position = temp;
    }
}