using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager screenManager = null;

    private void Awake()
    {
        if (screenManager != null)
            Destroy(this);
        else
        {
            screenManager = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    [SerializeField, Space(10)]
    CanvasGroup fadeImg;

    [SerializeField, Space(10)]
    GameObject loadingUI;
    [SerializeField]
    Slider loadingBar;
    [SerializeField]
    TMP_Text loadingText;

    float fadeDuration = 1.0f;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ChangeScene(string sceneName)
        => fadeImg.DOFade(1, fadeDuration)
                  .OnStart(() => { fadeImg.blocksRaycasts = true; })
                  .OnComplete(() => { StartCoroutine(LoadScene(sceneName)); });

    IEnumerator LoadScene(string sceneName)
    {
        loadingUI.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        float timer = 0;

        while (!(async.isDone))
        {
            yield return null;

            if (async.progress < 0.9f)
                loadingBar.value = async.progress;
            else
            {
                timer += Time.unscaledDeltaTime;
                loadingBar.value = Mathf.Lerp(0.9f, 1f, timer);

                if (loadingBar.value >= 1f)
                {
                    async.allowSceneActivation = true;
                    yield break;
                }
            }

            loadingText.text = $"{loadingBar.value * 100:F0} %";
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        => fadeImg.DOFade(0, fadeDuration)
                  .OnStart(() => { loadingUI.SetActive(false); })
                  .OnComplete(() => { fadeImg.blocksRaycasts = false; });
}
