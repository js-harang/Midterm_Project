using UnityEngine;

public class EnemyHitEvent : MonoBehaviour
{
    [SerializeField, Space(10)]
    EnemyController enemyController;

    private void EnemyHit()
    {
        if (this.name == "Enemy")
            enemyController.AttackAction();
    }
}
