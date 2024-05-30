using UnityEngine;

public class EnemyHitEvent : MonoBehaviour
{
    [SerializeField, Space(10)]
    EnemyController enemyController;

    private void EnemyHit()
    {
        enemyController.AttackAction();
    }
}
