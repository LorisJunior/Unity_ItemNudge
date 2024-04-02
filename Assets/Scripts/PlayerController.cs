using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction moveAction;
    public float speed = 5f;

    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private Vector2 move;
    private Vector2 lookDirection = new Vector2(0, -1);

    // Start is called before the first frame update
    void Start()
    {
        moveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void FixedUpdate()
    {
        rigidbody2d.MovePosition(rigidbody2d.position + move * speed * Time.deltaTime);
    }

    private void PlayerMovement()
    {
        move = moveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0f) || !Mathf.Approximately(move.y, 0f))
        {
            lookDirection = move;
            lookDirection.Normalize();
        }

        animator.SetFloat(Animations.moveX, lookDirection.x);
        animator.SetFloat(Animations.moveY, lookDirection.y);
        animator.SetFloat(Animations.speed, move.magnitude);
    }
}
