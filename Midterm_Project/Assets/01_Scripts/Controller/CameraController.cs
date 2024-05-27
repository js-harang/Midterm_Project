using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject background;

    [SerializeField]
    GameObject target;
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
        CameraUpdate();
    }

    private void CameraUpdate()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position,
                                                     target.transform.position + cameraPosition,
                                                     Time.deltaTime * cameraMoveSpeed);

        float limitX = background.transform.localScale.x / 2 - width;
        float clampX = Mathf.Clamp(transform.position.x,
                                  background.transform.position.x - limitX,
                                  background.transform.position.x + limitX);
        float limitY = background.transform.localScale.y / 2 - height;
        float clampY = Mathf.Clamp(transform.position.y,
                                  background.transform.position.y - limitY,
                                  background.transform.position.y + limitY);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
