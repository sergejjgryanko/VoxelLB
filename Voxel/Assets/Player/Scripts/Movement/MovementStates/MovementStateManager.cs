using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    #region Movement
    public float jumpHeight = 1.0f;
    public RunningAnimation runningAnimation;
    public Transform Ass;
    public float currentMoveSpeed;
    public float walkSpeed = 4;

    [HideInInspector] public Vector3 dir;
    private Vector3 dirr;
    [HideInInspector] public float hzInput, vInput;
    CharacterController controller;
    #endregion

    #region GroundCheck
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;
    #endregion

    #region Gravity
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;
    #endregion

    MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();

    [HideInInspector] public Animator anim;

    public int level = 1;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);

    }

    void Update()
    {
        GetDirectionAndMove();

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // —брасываем вертикальную скорость, когда персонаж находитс€ на земле
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            Jump();
        }
        // ѕримен€ем гравитацию
        velocity.y += gravity * Time.deltaTime;

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;
        if (Input.GetKeyDown(KeyCode.LeftShift))
            currentMoveSpeed = 250;
        else
            currentMoveSpeed = walkSpeed;

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            currentMoveSpeed = 0f;
        }

        if (controller.isGrounded)
            dirr = dir;

        controller.Move(dirr.normalized * currentMoveSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

        //разворот таза в зависимости от направлни€
        if (vInput == 0)
        {
            if (hzInput < 0)
            {
                Ass.rotation = Quaternion.Euler(0, -65, 0) * transform.rotation;
                runningAnimation.direction = 0;
            }
            else if (hzInput > 0)
            {
                Ass.rotation = Quaternion.Euler(0, 65, 0) * transform.rotation;
                runningAnimation.direction = 4;
            }
            else
            {
                Ass.rotation = Quaternion.Euler(0, 0, 0) * transform.rotation;
                runningAnimation.direction = 8;
            }
        }
        else if(vInput > 0)
        {
            if (hzInput < 0)
            {
                Ass.rotation = Quaternion.Euler(0, -40, 0) * transform.rotation;
                runningAnimation.direction = 1;
            }
            else if (hzInput > 0)
            {
                Ass.rotation = Quaternion.Euler(0, 40, 0) * transform.rotation;
                runningAnimation.direction = 3;
            }
            else
            {
                Ass.rotation = Quaternion.Euler(0, 0, 0) * transform.rotation;
                runningAnimation.direction = 2;
            }
        }
        else if(vInput < 0)
        {
            if (hzInput < 0)
            {
                Ass.rotation = Quaternion.Euler(0, 40, 0) * transform.rotation;
                runningAnimation.direction = 7;
            }
            else if (hzInput > 0)
            {
                Ass.rotation = Quaternion.Euler(0, -40, 0) * transform.rotation;
                runningAnimation.direction = 5;
            }
            else
            {
                Ass.rotation = Quaternion.Euler(0, 0, 0) * transform.rotation;
                runningAnimation.direction = 6;
            }
        }
    }
    void Jump()
    {
        // –ассчитываем вертикальную скорость дл€ прыжка
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void IncreaseLevel()
    {
        level++;
    }

}
