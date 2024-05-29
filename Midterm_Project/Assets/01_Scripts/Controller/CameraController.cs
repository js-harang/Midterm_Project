using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Space(10)]
    Transform background;

    [SerializeField]
    Transform target;
    private Vector3 cameraPosition = new Vector3(0, 0, -10);
    [SerializeField, Space(10)]
    float cameraMoveSpeed;

    float width;
    float height;

    private void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void Update()
    {
        CameraPositionUpdate();
    }

    private void CameraPositionUpdate()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position,
                                                     target.position + cameraPosition,
                                                     Time.deltaTime * cameraMoveSpeed);

        float limitX = background.localScale.x / 2 - width;
        float clampX = Mathf.Clamp(transform.position.x,
                                  background.position.x - limitX,
                                  background.position.x + limitX);
        float limitY = background.localScale.y / 2 - height;
        float clampY = Mathf.Clamp(transform.position.y,
                                  background.position.y - limitY,
                                  background.position.y + limitY);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
