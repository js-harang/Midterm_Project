using UnityEngine;

public enum EnemyState
{
    Run,
    Attack,
    Dead,
}

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    EnemyState enemyState;

    [SerializeField, Space(10)]
    GameObject player;
    [SerializeField]
    float attackDistance;

    Animator animator;

    private void Start()
    {
        enemyState = EnemyState.Run;

        animator = transform.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Run:
                Run();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Dead:
                Dead();
                break;
        }
    }

    private void Run()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < attackDistance)
        {
            enemyState = EnemyState.Attack;
            return;
        }

        Vector3 dir = (player.transform.position - transform.position).normalized;



        //if (Vector3.Distance(transform.position, originPos) > moveDistance)
        //{
        //    enemyState = EnemyState.Return;
        //    print("상태 전환 : Move -> Return");
        //}
        //else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        //{
        //    Vector3 dir = (player.position - transform.position).normalized;

        //    cc.Move(dir * moveSpeed * Time.deltaTime);

        //    transform.forward = dir;
        //}
        //else
        //{
        //    enemyState = EnemyState.Attack;
        //    print("상태 전환 : Move -> Attack");

        //    currentTime = attackDelay;

        //    anim.SetTrigger("MoveToAttackDelay");
        //}
    }

    private void Attack()
    {

    }

    private void Dead()
    {

    }
}
