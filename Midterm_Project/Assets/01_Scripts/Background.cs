using UnityEngine;

/// <summary>
/// Background 무한루프
/// </summary>
public class Background : MonoBehaviour
{
    [SerializeField] private int startPosition;

    private void Update()
    {
        transform.position = new Vector3(10 - ((Time.time + startPosition) % 20), 0, 0);

        if (transform.position.x <= -10)
            transform.position = new Vector3(10, 0, 0);
    }
}
