using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState
{
    Run,
    Attack,
    Death,
}

public class EnemyController : MonoBehaviour
{
    [SerializeField, Space(10)]
    PlayerController playerController;

    [SerializeField, Space(10)]
    EnemyState enemyState;

    int hp;
    [SerializeField, Space(10)]
    int maxHp;
    [SerializeField]
    Slider hpSlider;
    [SerializeField]
    int damage;

    [SerializeField, Space(10)]
    float moveSpeed;
    Transform player;

    [SerializeField, Space(10)]
    float attackDistance;
    [SerializeField]
    float attackDelay;
    float currentTime;

    Animator animator;

    private void Start()
    {
        enemyState = EnemyState.Run;

        animator = transform.GetComponentInChildren<Animator>();

        hp = maxHp;

        player = GameObject.Find("Player").transform;

        currentTime = attackDelay;
    }

    private void Update()
    {
        if (playerController.playerState == PlayerState.Death)
            return;

        hpSlider.value = (float)hp / (float)maxHp;

        switch (enemyState)
        {
            case EnemyState.Run:
                Run();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Death:
                Dead();
                break;
        }
    }

    private void Run()
    {
        // Run -> Attack
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            enemyState = EnemyState.Attack;
            animator.SetTrigger("RunToAttack");
            return;
        }
        // Run -> Death
        else if (hp <= 0)
        {
            enemyState = EnemyState.Death;
            animator.SetTrigger("ToDeath");
            return;
        }

        Vector3 dir = player.position - transform.position;
        dir.Normalize();
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        // Attack -> Run
        if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            enemyState = EnemyState.Run;
            animator.SetTrigger("AttackToRun");
            return;
        }
        // Attack -> Death
        else if (hp <= 0)
        {
            enemyState = EnemyState.Death;
            animator.SetTrigger("ToDeath");
            return;
        }

        currentTime += Time.deltaTime;
        if (currentTime > attackDelay)
        {
            currentTime = 0;

            animator.SetTrigger("StartAttack");
        }
    }

    private void Dead()
    {
        StartCoroutine(DeathProcess());
    }

    IEnumerator DeathProcess()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this);
    }
}
