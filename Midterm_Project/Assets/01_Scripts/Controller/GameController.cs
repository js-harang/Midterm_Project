using UnityEngine;

public class GameController : MonoBehaviour
{
    GameManager gameManager;
    ScreenManager screenManager;

    int gameSpeed = 1;
    [SerializeField] GameObject speed1X;
    [SerializeField] GameObject speed2X;

    private void Start()
    {
        gameManager = GameManager.instance;
        screenManager = ScreenManager.instance;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    switch (SceneManager.GetActiveScene().name)
        //    {
        //        case "01_Start":
        //            sm.popupUIActive = !sm.popupUIActive;
        //            break;
        //        case "02_Main":
        //            break;
        //    }
        //}
    }

    public void Speed2X()
    {
        if (gameSpeed == 1)
            gameSpeed++;
        else
            gameSpeed--;

        speed1X.SetActive(gameSpeed == 1);
        speed2X.SetActive(gameSpeed == 2);

        Time.timeScale = 1f * gameSpeed;
    }
}
