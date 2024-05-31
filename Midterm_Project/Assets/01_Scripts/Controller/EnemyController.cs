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
    GameManager gm;
    MainSceneController gc;
    PlayerController pc;
    CreateEnemy ce;
    LevelSystem ls;

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
    int exp;

    Animator animator;

    private void OnEnable()
    {
        animator = transform.GetComponentInChildren<Animator>();
        animator.SetTrigger("ReCreate");
        enemyState = EnemyState.Run;
        hp = maxHp;
    }

    private void Start()
    {
        gm = GameManager.gameManager;
        gc = GameObject.Find("GameController").GetComponent<MainSceneController>();
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        ce = GameObject.Find("EnemyManager").GetComponent<CreateEnemy>();
        ls = GameObject.Find("GameController").GetComponent<LevelSystem>();

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

        if (dir.x > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
            Vector3 newDir = new Vector3(-dir.x, dir.y, dir.z);
            transform.Translate(newDir * moveSpeed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(dir * moveSpeed * Time.deltaTime);
        }
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
        gm.money += money;
        gc.MoneyUpdate();
        ls.IncreaseExp(exp);

        yield return new WaitForSeconds(3f);

        if (pc.playerState != PlayerState.Death)
        {
            gameObject.SetActive(false);
            ce.enemyObjectPool.Add(gameObject);
        }
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
