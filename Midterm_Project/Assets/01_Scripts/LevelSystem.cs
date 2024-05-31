using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    GameManager gm;
    PlayerController pc;

    [SerializeField, Space(10)]
    Slider levelSlider;

    int currentExp = 0;

    private void Start()
    {
        gm = GameManager.gameManager;
        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        LevelBarUpdate();
    }

    public void IncreaseExp(int exp)
    {
        if (gm.level >= gm.endLevel)
        {
            EndLevel();
            return;
        }

        currentExp += exp;

        if (currentExp >= gm.expRequired)
        {
            gm.level++;
            pc.hp = gm.maxHp;
            currentExp -= gm.expRequired;
        }

        LevelBarUpdate();
    }

    private void LevelBarUpdate()
    {
        levelSlider.value = (float)currentExp / gm.expRequired;
    }

    private void EndLevel()
    {
        levelSlider.value = 1;
    }
}
