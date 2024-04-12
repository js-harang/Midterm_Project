using UnityEngine;

/// <summary>
/// Background 무한루프
/// </summary>
public class StartBackground : MonoBehaviour
{
    private GameManager gm;

    [SerializeField] private int startPosition;

    private void Start()
    {
        gm = GameManager.instance;
    }

    private void Update()
    {
        transform.position = new Vector3(10 - ((Time.time + startPosition) % 20), 1, 0);

        if (transform.position.x <= -10)
            transform.position = new Vector3(10, 1, 0);
    }
}
