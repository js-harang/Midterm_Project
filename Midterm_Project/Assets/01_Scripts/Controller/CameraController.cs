using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //private void Update()
    //{
    //    Vector2 newVelocity = rb.velocity;
    //    newVelocity.x = speed;
    //    rb.velocity = newVelocity;
    //}
}
