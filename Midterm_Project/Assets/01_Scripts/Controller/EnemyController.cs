using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState
{
    Idle,
    Run,
    Attack,
    Death,
}

public class EnemyController : MonoBehaviour
{
    PlayerController pc;

    [Space(10)]
    public EnemyState enemyState;

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

    [SerializeField, Space(10)]
    int money;
    [SerializeField]
    float exp;

    Animator animator;

    private void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        enemyState = EnemyState.Run;

        animator = transform.GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;

        hp = maxHp;
        hpSlider.value = (float)hp / maxHp;

        currentTime = attackDelay;
    }

    private void Update()
    {
        if (pc.playerState == PlayerState.Death)
        {
            Idle();
            return;
        }

        hpSlider.value = (float)hp / maxHp;

        switch (enemyState)
        {
            case EnemyState.Run:
                Run();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    private void Idle()
    {
        enemyState = EnemyState.Idle;
        animator.SetTrigger("ToIdle");
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

    public void AttackAction()
    {
        pc.DamageAction(damage);
    }

    public void DamageAction(int playerDamage)
    {
        hp -= playerDamage;
        hpSlider.value = (float)hp / maxHp;

        // AnyState -> Death
        if (hp <= 0)
        {
            enemyState = EnemyState.Death;
            animator.SetTrigger("ToDeath");
            Dead();
        }
    }
}
