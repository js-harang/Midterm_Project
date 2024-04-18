using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameManager gm;
    private ScreenManager sm;

    [SerializeField] private GameObject background;
    private GameObject backgroundObj;
    private List<GameObject> currentBackground;
    private float backgroundWidth;

    private void Start()
    {
        gm = GameManager.instance;
        sm = ScreenManager.instance;

        currentBackground = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            Vector2 position = new Vector2(10 * i, 0);
            backgroundObj = Instantiate(background, position, Quaternion.identity);
            currentBackground.Add(backgroundObj);
        }

        backgroundWidth = background.transform.Find("X").localScale.x;
    }

    private void Update()
    {
        Background();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "01_Start":
                    sm.popupUIActive = !sm.popupUIActive;
                    break;
                case "02_Main":
                    break;
            }
        }
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

    public void GameStartBtn() => sm.ChangeScene("02_Main");
}
