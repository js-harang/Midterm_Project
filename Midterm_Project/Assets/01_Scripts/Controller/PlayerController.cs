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
    [SerializeField] private Joystick stick;
    [SerializeField] private float speed;

    [Space(10)]
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Animator animator;

    private void Update()
    {
        if (playerState != PlayerState.Dead)
        {
            PlayerMove();
        }

        AnimationUpdtae();
    }

    private void PlayerMove()
    {
        if (stick.Horizontal == 0 && stick.Vertical == 0)
        {
            playerState = PlayerState.Idle;
            return;
        }

        playerState = PlayerState.Run;
        Vector3 dir = new Vector3(stick.Horizontal, stick.Vertical, 0);
        dir.Normalize();
        transform.position += dir * speed * Time.deltaTime;
        if (stick.Horizontal > 0)
            gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
        else if (stick.Horizontal < 0)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void AnimationUpdtae()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                animator.SetFloat("RunState", 0);
                break;
            case PlayerState.Run:
                animator.SetFloat("RunState", 0.5f);
                break;
            case PlayerState.Attack:
                break;
            case PlayerState.Dead:
                break;
        }
    }
}
