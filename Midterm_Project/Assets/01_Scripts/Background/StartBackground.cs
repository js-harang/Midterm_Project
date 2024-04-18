using System.Collections.Generic;
using UnityEngine;

public class StartBackground : MonoBehaviour
{
    [SerializeField] GameObject background;
    private List<GameObject> currentBackground;

    private float screenWidth;

    private void Start()
    {
        currentBackground = new List<GameObject>();

        for (int i = 0; i < 3; i++)
        {
            Vector2 position = new Vector2(10 * i, 0);
            GameObject obj = Instantiate(background, position, Quaternion.identity);
            BackgroundMove back = obj.GetComponent<BackgroundMove>();
            currentBackground.Add(obj);
            back.initialValue = 10 * i;
        }

        screenWidth = currentBackground[0].transform.Find("X").localScale.x;
    }

    //private void Update()
    //{
    //    BackgroundPosition();
    //}

    private void BackgroundPosition()
    {
        GameObject obj = currentBackground[0];
        float moveStageX = Camera.main.transform.position.x + screenWidth;
        float startX = currentBackground[currentBackground.Count - 1].transform.position.x + screenWidth;

        if (currentBackground[currentBackground.Count - 1].transform.position.x <= moveStageX)
        {
            obj.transform.position = new Vector2(startX, 0);
            currentBackground.Remove(obj);
            currentBackground.Add(obj);
        }
    }

    private void MoveBackground()
    {

    }

    //[SerializeField] GameObject background;
    //[SerializeField] List<GameObject> currentBackground;

    //private void Update()
    //{
    //    create
    //}

    //private GameManager gm;

    //[SerializeField] private int startPosition;

    //private void Start()
    //{
    //    gm = GameManager.instance;
    //}

    //private void Update()
    //{
    //    transform.position = new Vector3(10 - ((Time.time + startPosition) % 20), 1, 0);

    //    if (transform.position.x <= -10)
    //        transform.position = new Vector3(10, 1, 0);
    //}
}
