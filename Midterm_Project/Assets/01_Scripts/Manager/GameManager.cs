using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    [HideInInspector] public int enemyCount;
}
