using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    GameManager gameManager;
    ScreenManager screenManager;

    int gameSpeed = 1;
    [SerializeField, Space(10)]
    GameObject speed1X;
    [SerializeField]
    GameObject speed2X;

    private void Start()
    {
        gameManager = GameManager.gameManager;
        screenManager = ScreenManager.screenManager;
    }

    private void Update()
    {

    }

    public void Speed2X()
    {
        if (gameSpeed == 1)
            gameSpeed++;
        else
            gameSpeed--;

        speed1X.SetActive(gameSpeed == 1);
        speed2X.SetActive(gameSpeed == 2);

        Time.timeScale = 1 * gameSpeed;
    }
}
