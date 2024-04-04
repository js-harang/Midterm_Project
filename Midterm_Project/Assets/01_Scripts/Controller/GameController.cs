using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    GameManager gm = GameManager.instance;
    ScreenManager sm;

    public void GameStartBtn() => sm.ChangeScene("02_Main");

    private void Start()
    {
        sm = ScreenManager.instance;

        Screen.SetResolution(1080, 1920, true);

        Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "01_Start":
                    sm.popupUIActive = !sm.popupUIActive;
                    //sm.popupUI.SetActive(sm.popupUIActive);
                    break;
                case "02_Main":
                    break;
            }
        }
    }
}
