using TMPro;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    GameManager gm;
    PlayerController pc;

    [SerializeField, Space(10)]
    GameObject speed1X;
    [SerializeField]
    GameObject speed2X;

    [SerializeField, Space(10)]
    GameObject exitPopup;
    bool isActive = false;
    int currentSpeed;

    [SerializeField, Space(10)]
    GameObject statPopup;
    bool statIsActive = false;

    [SerializeField, Space(10)]
    StatTxt statTxt;
    [System.Serializable]
    public struct StatTxt
    {
        public TextMeshProUGUI moneyTxt;
        [Space(10)]
        public TextMeshProUGUI strTxt;
        public TextMeshProUGUI maxHpTxt;
        public TextMeshProUGUI criticalChangeTxt;
        public TextMeshProUGUI criticalDamageTxt;
        [Space(10)]
        public TextMeshProUGUI strCostTxt;
        public TextMeshProUGUI maxHpCostTxt;
        public TextMeshProUGUI criticalChangeCostTxt;
        public TextMeshProUGUI criticalDamageCostTxt;
    }

    int strCost = 1;
    int maxHpCost = 1;
    int criticalChangeCost = 1;
    int criticalDamageCost = 1;

    private void Start()
    {
        gm = GameManager.gameManager;
        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        DamageUp();
        MaxHpUp();
        CriticalChanceUp();
        CriticalDamageUp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = !isActive;
            exitPopup.SetActive(isActive);

            if (isActive == true)
                currentSpeed = gm.gameSpeed;

            if (gm.gameSpeed == 1 || gm.gameSpeed == 2)
                gm.gameSpeed = 0;
            else if (gm.gameSpeed == 0)
                gm.gameSpeed = currentSpeed;
            PlaySpeed();
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Continue()
    {
        isActive = !isActive;
        exitPopup.SetActive(isActive);

        gm.gameSpeed = currentSpeed;
        PlaySpeed();
    }

    public void Speed2X()
    {
        if (gm.gameSpeed == 1)
            gm.gameSpeed++;
        else
            gm.gameSpeed--;

        speed1X.SetActive(gm.gameSpeed == 1);
        speed2X.SetActive(gm.gameSpeed == 2);

        PlaySpeed();
    }

    private void PlaySpeed()
    {
        Time.timeScale = 1 * gm.gameSpeed;
    }

    public void OpenCloseStat()
    {
        statIsActive = !statIsActive;
        statPopup.SetActive(statIsActive);
    }

    public void MoneyUpdate()
    {
        statTxt.moneyTxt.text = gm.money.ToString();
    }

    public void DamageUp()
    {
        if (gm.money < strCost)
            return;

        strCost = gm.str - 5 + 1;
        gm.money -= strCost;
        gm.str += 1;
        CostUpdate(statTxt.strTxt, statTxt.strCostTxt, strCost);
    }

    public void MaxHpUp()
    {
        if (gm.money < maxHpCost)
            return;

        maxHpCost = gm.maxHp - 100 + 1;
        gm.money -= maxHpCost;
        gm.maxHp += 1;
        CostUpdate(statTxt.maxHpTxt, statTxt.maxHpCostTxt, maxHpCost);
        pc.MpBarUpdate();
    }

    public void CriticalChanceUp()
    {
        if (gm.money < criticalChangeCost)
            return;

        criticalChangeCost = gm.criticalChance + 1;
        gm.money -= criticalChangeCost;
        gm.criticalChance += 1;
        CostUpdate(statTxt.criticalChangeTxt, statTxt.criticalChangeCostTxt, criticalChangeCost);
    }

    public void CriticalDamageUp()
    {
        if (gm.money < criticalDamageCost)
            return;

        criticalDamageCost = gm.criticalDamage + 1;
        gm.money -= criticalDamageCost;
        gm.criticalDamage += 1;
        CostUpdate(statTxt.criticalDamageTxt, statTxt.criticalDamageCostTxt, criticalDamageCost);
    }

    private void CostUpdate(TextMeshProUGUI txt, TextMeshProUGUI costTxt, int cost)
    {
        txt.text = (cost - 1).ToString();
        costTxt.text = "Money\n" + cost;
    }
}
