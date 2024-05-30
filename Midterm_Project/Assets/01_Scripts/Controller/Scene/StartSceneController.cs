using DG.Tweening;
using TMPro;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    GameManager gm;
    ScreenManager sm;

    TextMeshProUGUI text;

    private void Start()
    {
        gm = GameManager.gameManager;
        sm = ScreenManager.screenManager;

        text = GameObject.Find("TouchToStart").GetComponent<TextMeshProUGUI>();
        text.DOFade(0, 1.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            GameStartBtn();
    }

    private void GameStartBtn()
    {
        gm.gameState = GameState.Playing;
        sm.ChangeScene("02_Main");
    }
}
