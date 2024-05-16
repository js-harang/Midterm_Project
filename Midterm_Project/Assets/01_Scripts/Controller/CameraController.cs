using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void Update()
    {
        gameObject.transform.position = new Vector3(
                                        target.transform.position.x,
                                        target.transform.position.y,
                                        target.transform.position.z - 10);
    }
}
