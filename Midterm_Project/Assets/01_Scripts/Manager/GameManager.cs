using UnityEngine;

public enum GameState
{
    Title,
    Playing,
    GameOver,
}

public class GameManager : MonoBehaviour
{
    [SerializeField, Space(10)]
    GameState gameState;

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

    public int enemyMaxCount;

    private void Start()
    {
        gameState = GameState.Title;
    }
}
