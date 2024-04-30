using DG.Tweening;
using TMPro;
using UnityEngine;

public class BlinkTxt : MonoBehaviour
{
    [SerializeField] private LoopType loopType;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.DOFade(0, 1.5f).SetLoops(-1, loopType);
    }
}
