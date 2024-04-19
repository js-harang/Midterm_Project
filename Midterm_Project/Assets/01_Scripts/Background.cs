using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float speed;
    public int initialValue;
    private int count = 0;

    private void Update()
    {
        float positionX = initialValue - speed * 0.125f;
        transform.position = new Vector2 (positionX, 0);
        count++;
    }
}
