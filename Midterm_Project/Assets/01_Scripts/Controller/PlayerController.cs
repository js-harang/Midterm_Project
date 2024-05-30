using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    Idle,
    Run,
    AttackDelay,
    Attack,
    Death,
}

public class PlayerController : MonoBehaviour
{
    GameManager gm;

    [Space(10)]
    public PlayerState playerState;

    [HideInInspector]
    public int hp;
    [SerializeField, Space(10)]
    Slider hpSlider;
    [HideInInspector]
    public int mp;
    [SerializeField]
    Slider mpSlider;
    [SerializeField]
    int damage;

    [SerializeField, Space(10)]
    Joystick stick;
    [SerializeField]
    float speed;

    [SerializeField, Space(10)]
    float attackDistance;
    float currentTime;
    [SerializeField, Space(10)]
    float attackDelay;

    GameObject[] enemys;

    Animator animator;

    private void Start()
    {
        gm = GameManager.gameManager;

        playerState = PlayerState.Idle;

        animator = transform.GetComponentInChildren<Animator>();

        hp = gm.maxHp;
        hpSlider.value = (float)hp / gm.maxHp;
        mp = gm.maxMp;
        mpSlider.value = (float)hp / gm.maxMp;

        currentTime = attackDelay;

        enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Update()
    {
        if (gm.gameState == GameState.GameOver)
            return;

        FindTarget();

        switch (playerState)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Run:
                Run();
                break;
            case PlayerState.AttackDelay:
                FindTarget();
                break;
            case PlayerState.Death:
                Death();
                break;
        }
    }

    private void Idle()
    {
        // Idle -> Run
        if (stick.Horizontal != 0 || stick.Vertical != 0)
        {
            playerState = PlayerState.Run;
            animator.SetFloat("State", 1);
            return;
        }

        // Idle -> Attack
        foreach (GameObject target in enemys)
        {
            EnemyController ec = target.GetComponent<EnemyController>();
            if (Vector3.Distance(transform.position, target.transform.position) < attackDistance)
            {
                playerState = PlayerState.AttackDelay;
                animator.SetTrigger("ToAttackDelay");
            }
        }
    }

    private void Run()
    {
        // Run -> Idle
        if (stick.Horizontal == 0 && stick.Vertical == 0)
        {
            playerState = PlayerState.Idle;
            animator.SetFloat("State", 0);
            return;
        }

        // Run -> Attack
        foreach (GameObject target in enemys)
        {
            EnemyController ec = target.GetComponent<EnemyController>();
            if (Vector3.Distance(transform.position, target.transform.position) < attackDistance)
            {
                playerState = PlayerState.AttackDelay;
                animator.SetTrigger("ToAttackDelay");
            }
        }

        Vector3 dir = new Vector3(stick.Horizontal, stick.Vertical, 0);
        dir.Normalize();
        transform.position += dir * speed * Time.deltaTime;

        // 좌우 반전
        if (stick.Horizontal > 0)
            gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
        else if (stick.Horizontal < 0)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        // 플레이어가 카메라 밖으로 나가는 현상 방지
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0.05f) pos.x = 0.05f;
        if (pos.x > 0.95f) pos.x = 0.95f;
        if (pos.y < 0.15f) pos.y = 0.15f;
        if (pos.y > 0.85f) pos.y = 0.85f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private void FindTarget()
    {
        List<GameObject> targets = new List<GameObject>();

        foreach (GameObject target in enemys)
        {
            EnemyController ec = target.GetComponent<EnemyController>();
            if (Vector3.Distance(transform.position, target.transform.position) < attackDistance
                && ec.enemyState != EnemyState.Death)
                targets.Add(target);
        }

        Debug.Log(targets.Count);

        if (targets.Count <= 0)
        {
            if (playerState == PlayerState.Attack || playerState == PlayerState.AttackDelay)
            {
                playerState = PlayerState.Idle;
                animator.SetFloat("State", 0);
            }
        }
        else
        {
            animator.SetTrigger("ToAttackDelay");
            playerState = PlayerState.AttackDelay;
            AttackDelay(targets);
        }
    }

    private void AttackDelay(List<GameObject> targets)
    {
        currentTime += Time.deltaTime;
        if (currentTime > attackDelay)
        {
            currentTime = 0;
            animator.SetTrigger("StartAttack");
            playerState = PlayerState.Attack;
            Attack(targets);
        }
    }

    private void Attack(List<GameObject> targets)
    {
        StartCoroutine(AttackCoroutine(targets));
        playerState = PlayerState.AttackDelay;
    }

    private IEnumerator AttackCoroutine(List<GameObject> targets)
    {
        yield return new WaitForSeconds(0.5f);

        foreach (GameObject target in targets)
        {
            EnemyController ec = target.GetComponent<EnemyController>();
            ec.DamageAction(damage);
        }
    }

    public void DamageAction(int enemyDamage)
    {
        hp -= enemyDamage;
        hpSlider.value = (float)hp / gm.maxHp;

        // AnyState -> Death
        if (hp <= 0)
        {
            playerState = PlayerState.Death;
            animator.SetTrigger("ToDeath");
        }
    }

    private void Death()
    {
        animator.SetTrigger("ToDeath");
        gm.gameState = GameState.GameOver;
    }









    //private void Attack()
    //{
    //    if (stick.Horizontal != 0 || stick.Vertical != 0)
    //    {
    //        playerState = PlayerState.Run;
    //        animator.SetFloat("State", 1);
    //        return;
    //    }

    //    animator.SetTrigger("StartAttack");

    //    StartCoroutine(AttackAction());
    //    playerState = PlayerState.AttackDelay;
    //}


    //private IEnumerator AttackAction()
    //{
    //    yield return new WaitForSeconds(0.5f);

    //    foreach (GameObject target in attackableEnemy)
    //    {
    //        EnemyController ec = target.GetComponent<EnemyController>();
    //        if (ec.enemyState != EnemyState.Death)
    //            ec.DamageAction(damage);
    //    }
    //}


    //private IEnumerator AttackCoroutine()
    //{
    //    while (true)
    //    {
    //        attackableEnemy = Target();

    //        // Attack -> Idle
    //        if (attackableEnemy.Count != 0)
    //        {
    //            playerState = PlayerState.Attack;
    //            animator.SetTrigger("ToAttackDelay");
    //            Attack();
    //        }
    //        else
    //            playerState = PlayerState.Idle;

    //        yield return new WaitForSeconds(attackDelay);
    //    }
    //}

    //public List<GameObject> Target()
    //{
    //    GameObject[] Enemys = GameObject.FindGameObjectsWithTag("Enemy");
    //    List<GameObject> targets = new List<GameObject>();

    //    foreach (GameObject target in Enemys)
    //    {
    //        EnemyController ec = target.GetComponent<EnemyController>();
    //        if (target.gameObject.layer == LayerMask.NameToLayer("Enemy")
    //            && Vector3.Distance(transform.position, target.transform.position) < attackDistance
    //            && ec.enemyState != EnemyState.Death)
    //            targets.Add(target);
    //    }

    //    if (targets.Count > 0)
    //    {
    //        StartCoroutine
    //    }

    //    return targets;
    //}
}
