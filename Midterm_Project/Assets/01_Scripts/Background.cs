using UnityEngine;

public class Background : MonoBehaviour
{
    private BackgroundController bc;

    [SerializeField] private float speed;
    public int initialValue;

    private void Start()
    {
        bc = GameObject.Find("GameController").GetComponent<BackgroundController>();
    }

    private void Update()
    {
        float positionX = transform.position.x - speed * 0.001f;
        gameObject.transform.position = new Vector2(positionX, 0);
    }
}
