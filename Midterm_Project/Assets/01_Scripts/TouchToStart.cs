using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToStart : MonoBehaviour
{
    private GameController gm;

    private void Start()
    {
        gm = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            gm.GameStartBtn();
    }
}
