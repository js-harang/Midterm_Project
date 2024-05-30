using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    GameManager gm;

    [SerializeField, Space(10)]
    Slider levelSlider;

    int currentExp;

    private void Start()
    {
        gm = GameManager.gameManager;
    }

    private void Update()
    {

    }

    public void IncreaseExp(int exp)
    {
        currentExp += exp;

        if (currentExp > gm.expRequired)
        {
            gm.level++;
            currentExp -= gm.expRequired;
        }

        levelSlider.value = (float)currentExp / gm.expRequired;
    }
}
