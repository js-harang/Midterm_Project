using UnityEngine;

public enum GameState
{
    Title,
    Playing,
    GameOver,
}

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;

    private void Awake()
    {
        if (gameManager != null)
            Destroy(this);
        else
        {
            gameManager = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    [Space(10)]
    public GameState gameState;

    [Space(10)]
    public int enemyMaxCount;

    [Space(10)]
    public int level;
    public int startLevel;
    public int endLevel;
    public int expRequired;

    [Space(10)]
    public int str;
    public int maxHp;
    public int maxMp;
    public int criticalChance;
    public int criticalDamage;

    [Space(10)]
    public int momey;
    public int stat;

    private void Start()
    {
        gameState = GameState.Title;
    }
}
