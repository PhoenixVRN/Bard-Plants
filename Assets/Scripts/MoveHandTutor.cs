using UnityEngine;

public class MoveHandTutor : MonoBehaviour
{
    public float speed = 2f; // Скорость движения
    public float width = 3f; // Ширина восьмерки
    public float height = 2f; // Высота восьмерки
    private float time;
    private float X;
    private float Y;

    private void OnEnable()
    {
        X = transform.position.x;
        Y = transform.position.y;
    }

    void Update()
    {
        time += Time.deltaTime * speed;
        float x = Mathf.Sin(time) * width;
        float y = Mathf.Sin(time * 2) * height / 2; // Вторая частота вдвое больше

        transform.position = new Vector3(X + x, Y + y, transform.position.z);
    }
}