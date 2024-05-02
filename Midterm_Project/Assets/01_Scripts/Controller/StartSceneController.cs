using DG.Tweening;
using TMPro;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    private ScreenManager sm;

    [Space(10f)]
    [SerializeField] private LoopType loopType;
    private TextMeshProUGUI text;

    private void Start()
    {
        sm = ScreenManager.instance;

        text = GameObject.Find("TouchToStart").GetComponent<TextMeshProUGUI>();
        text.DOFade(0, 1.5f).SetLoops(-1, loopType);
    }

    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            GameStartBtn();
    }

    private void GameStartBtn()
    {
        sm.ChangeScene("02_Main");
    }
}
