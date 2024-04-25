using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameManager gm;
    private ScreenManager sm;

    private int gameSpeed = 1;

    private void Start()
    {
        gm = GameManager.instance;
        sm = ScreenManager.instance;
    }

    private void Update()
    {
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

    public void GameStartBtn() => sm.ChangeScene("02_Main");

    public void Speed2X()
    {
        if (gameSpeed == 1)
            gameSpeed++;
        else
            gameSpeed--;

        Time.timeScale = 1f * gameSpeed;
    }
}
