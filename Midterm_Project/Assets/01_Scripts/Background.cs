using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Material backgroundMaterial;
    [Space(10f)]
    [SerializeField] private float scrollSpeed;

    private void Update()
    {
        Vector2 dir = Vector2.right;
        backgroundMaterial.mainTextureOffset += dir * scrollSpeed * Time.deltaTime;
    }
}
