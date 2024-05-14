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
    }

    private void PlayerMove()
    {
        if (Input.touchCount != 0 || Input.GetMouseButtonDown(0) == true)
        {
            playerState = PlayerState.Run;
            if (playerState == PlayerState.Run)
                animator.SetFloat("RunState", 0.5f);

        }

        Vector3 dir = new Vector3(stick.Horizontal, stick.Vertical, 0);
        dir.Normalize();
        transform.position += dir * speed * Time.deltaTime;
        if (stick.Horizontal > 0)
            gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
        else if (stick.Horizontal < 0)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
