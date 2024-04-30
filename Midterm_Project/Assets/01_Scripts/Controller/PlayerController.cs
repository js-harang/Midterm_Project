using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Joystick stick;
    [SerializeField] private float speed;

    private void Start()
    {
        stick = GameObject.Find("Joystick").GetComponent<Joystick>();
    }

    void Update()
    {
        Vector3 dir = new Vector3(stick.Horizontal, stick.Vertical, 0);
        dir.Normalize();
        transform.position += dir * speed * Time.deltaTime;

        if (stick.Horizontal > 0)
            gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
        else if (stick.Horizontal < 0)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
