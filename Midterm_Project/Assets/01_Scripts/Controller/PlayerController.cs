using System.Collections;
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

    [SerializeField, Space(10)]
    Joystick stick;
    [SerializeField]
    float speed;

    [SerializeField, Space(10)]
    float attackDistance;
    [SerializeField, Space(10)]
    float attackDelay;

    Animator animator;

    private void Start()
    {
        gm = GameManager.gameManager;

        playerState = PlayerState.Idle;

        animator = transform.GetComponentInChildren<Animator>();

        hp = gm.maxHp;
        HpBarUpdate();
        mp = gm.maxMp;
        MpBarUpdate();

        StartCoroutine(AttackCoroutine());
    }

    private void Update()
    {
        if (gm.gameState == GameState.GameOver)
            return;

        switch (playerState)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Run:
                Run();
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

    public void DamageAction(int enemyDamage)
    {
        hp -= enemyDamage;
        HpBarUpdate();

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
        playerState = PlayerState.Death;
    }

    public IEnumerator AttackCoroutine()
    {
        while (playerState != PlayerState.Death)
        {
            GameObject target = GetTarget();
            if (target != null)
            {
                playerState = PlayerState.AttackDelay;
                animator.SetTrigger("ToAttackDelay");
                Attack(target);
            }
            else
            {
                playerState = PlayerState.Idle;
                animator.SetFloat("State", 0);
            }

            if (stick.Horizontal != 0 || stick.Vertical != 0)
            {
                playerState = PlayerState.Run;
                animator.SetFloat("State", 1);
            }

            yield return new WaitForSeconds(attackDelay);
        }
    }

    public GameObject GetTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject target in targets)
        {
            EnemyController ec = target.GetComponent<EnemyController>();
            if (Vector3.Distance(transform.position, target.transform.position) < attackDistance
                && ec.enemyState != EnemyState.Death)
                return target;
        }

        return null;
    }

    public void Attack(GameObject targetObject)
    {
        if (stick.Horizontal != 0 || stick.Vertical != 0)
        {
            playerState = PlayerState.Run;
            animator.SetFloat("State", 1);
            return;
        }

        playerState = PlayerState.Attack;
        animator.SetTrigger("StartAttack");
        StartCoroutine(Hit(targetObject));
    }

    private IEnumerator Hit(GameObject targetObject)
    {
        yield return new WaitForSeconds(0.5f);

        EnemyController ec = targetObject.GetComponent<EnemyController>();
        ec.DamageAction(gm.str);
    }

    public void HpBarUpdate()
    {
        hpSlider.value = (float)hp / gm.maxHp;
    }

    public void MpBarUpdate()
    {
        mpSlider.value = (float)hp / gm.maxMp;
    }
}
