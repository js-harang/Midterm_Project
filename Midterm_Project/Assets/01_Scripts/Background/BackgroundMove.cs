using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    int count = 0;
    public int initialValue;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //initialValue = transform.position.x;
    }

    private void Update()
    {
        Vector2 position = transform.position;
        float x = initialValue - speed * 0.125f* count;
        transform.position = new Vector2(x, 0);
        count++;
        if(x <= -10)
        {
            initialValue += 20;
            count = 0;
        }
        //Vector2 newVelocity = rb.velocity;
        //newVelocity.x = -speed;
        //rb.velocity = newVelocity;
    }
}
