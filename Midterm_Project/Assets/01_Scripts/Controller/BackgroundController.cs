using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Background bm;

    [SerializeField] private GameObject background;
    private GameObject backgroundObj;
    private List<GameObject> currentBackground;
    private float backgroundWidth;

    private void Start()
    {
        bm = background.GetComponent<Background>();

        currentBackground = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            bm.initialValue = i * 10;
            Vector2 position = new Vector2(bm.initialValue, 0);
            backgroundObj = Instantiate(background, position, Quaternion.identity);
            currentBackground.Add(backgroundObj);
        }

        backgroundWidth = background.transform.Find("X").localScale.x;
    }

    private void Background()
    {
        backgroundObj = currentBackground[0];
        float removePositionX = Camera.main.transform.position.x - backgroundWidth / 2 * 3;
        float creationPositionX = currentBackground[currentBackground.Count - 1].transform.position.x + backgroundWidth;

        if (currentBackground[0].transform.position.x <= removePositionX)
        {
            backgroundObj.transform.position = new Vector2(creationPositionX, 0);
            currentBackground.Remove(backgroundObj);
            currentBackground.Add(backgroundObj);
        }
    }
}
