using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField, Space(10)]
    private Material backgroundMaterial;

    [SerializeField, Space(10)]
    private float scrollSpeed;

    private void Update()
    {
        Vector2 dir = Vector2.right;
        backgroundMaterial.mainTextureOffset += dir * scrollSpeed * Time.deltaTime;
    }
}
