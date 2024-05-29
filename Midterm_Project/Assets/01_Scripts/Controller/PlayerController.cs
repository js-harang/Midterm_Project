using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    Attack,
    Dead,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerState playerState;

    [SerializeField, Space(10)]
    Joystick stick;
    [SerializeField]
    float speed;

    [SerializeField, Space(10)]
    float attackDistance;

    [SerializeField, Space(10)]
    int damage;
    float currentTime;
    [SerializeField]
    float attackDelay;

    Animator animator;

    private void Start()
    {
        playerState = PlayerState.Idle;

        animator = transform.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (playerState == PlayerState.Dead)
            return;

        switch (playerState)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Run:
                Run();
                break;
            case PlayerState.Attack:
                Attack();
                break;
            case PlayerState.Dead:
                Dead();
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
        }

        // Idle -> Attack
        if (Vector3.Distance(transform.position, transform.position) > attackDistance)
            ToAttackDelay();
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
        if (Vector3.Distance(transform.position, transform.position) > attackDistance)
            ToAttackDelay();

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

    private void Attack()
    {
        currentTime += Time.deltaTime;
        if (currentTime > attackDelay)
        {
            currentTime = 0;

            animator.SetTrigger("StartAttack");
        }
    }

    private void Dead()
    {

    }

    private void ToAttackDelay()
    {
        playerState = PlayerState.Attack;

        currentTime = attackDelay;
        animator.SetTrigger("ToAttackDelay");
    }
}
