using UnityEngine;

/// <summary>
/// Background 무한루프
/// </summary>
public class Background1 : MonoBehaviour
{
    [SerializeField] private Transform target;
    [Space]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float scrollAmount;

    private Vector3 moveDirection = Vector3.left;

    private void Update()
    {
        Vector3 x = new Vector3(20 - (Time.time % 20), 0, 0);

        transform.position = x;

        if (transform.position.x <= 0)
        {
            Vector3 a = new Vector3(10, 0, 0);

            transform.position = a;

        }

        Debug.Log(transform.position);
    }
}
