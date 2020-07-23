using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TouchControlsKit;

public class Movement : MonoBehaviour
{
    public bool running = false;
    public float runningForce = 3.75f;
    private float runningSpeed = 0.0f;
    public bool turningLeft = false;
    public bool turningRight = false;

    public bool jumping = false;
    public bool jump = false;
    public float jumpForce = 7.0f;
    public float verticalSpeed = 0.0f;
    public bool grounded = false;

    public float gravity = 9.0f;

    public float idleTimer = 0.0f;
    Animator m_Animator;
    CharacterController m_CharacterController;
    GameObject BoundingBox;

    private float rotationY = 90.0f; // Default player rotation is +90

    public bool jumpOnPlace = false;
    public int jumpDelay = 0;
    public int jumpDelayDef = 15; // Default value for jump delay.

    public bool attack = false; 

    private Vector3 moveVector = Vector3.zero;

    private bool locked = false;

    private GameObject Game;
    private Logger logger;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_CharacterController = gameObject.GetComponent<CharacterController>();
        BoundingBox = GameObject.Find("BoundingBox");

        Game = GameObject.Find("Game");
        logger = GameObject.Find("Telemetry").GetComponent<Logger>();
    }

    // Quickly turns player if movement direction is suddenly changed.
    void Turning()
    {
        if (turningLeft)
            rotationY -= 30.0f;
        if (rotationY <= -90.0f)
            turningLeft = false;

        if (turningRight)
            rotationY += 30.0f;
        if (rotationY >= 90.0f)
            turningRight = false;

        return;
    }

    void LockZAxis(float zvalue)
    {
        Vector3 playerPosition = transform.position;
        playerPosition.z = zvalue;

        transform.position = playerPosition;
    }

    public void LockMovement()
    {
        m_Animator.SetBool("locked", true);
        locked = true;
    }

    public void UnlockMovement()
    {
        m_Animator.SetBool("locked", false);
        locked = false;
    }

    bool checkIfGrounded()
    {
        // isGrounded property of CharacterController is unreliable, so test also collisions with bounding box embeded inside player
        bool test1 = m_CharacterController.isGrounded;
        bool test2 = BoundingBox.GetComponent<IsGrounded>().grounded;
        if (jumpOnPlace)
            test2 = false;

        return test1 || test2;
    }

    float checkMovementButtons()
    {
        float check1 = Input.GetAxis("Horizontal");
        float check2 = 0.0f;

        string currentController = Game.GetComponent<AssignController>().controllerString;
        int currentControllerNum = Game.GetComponent<AssignController>().controllerNum;

        if (currentControllerNum != 3)
            check2 = TCKInput.GetAxis(currentController, EAxisType.Horizontal);
        else
        {
            ControllerStickyScript cSticky = GameObject.Find("Controller-STICKY").GetComponent<ControllerStickyScript>();
            if (cSticky.left)
                check2 = -1.0f;
            if (cSticky.right)
                check2 = +1.0f;
        }

        if (check1 != 0.0f)
            return check1;
        else if (check2 != 0.0f)
            return check2;
        else
            return 0.0f;
    }

    void FixedUpdate()
    {
        if (locked)
            return;

        grounded = checkIfGrounded();

        running = false;

        idleTimer += Time.fixedDeltaTime;

        Turning();

        if (checkMovementButtons() < 0.0f)
        {
            if (rotationY > -90.0f)
                turningLeft = true;
            else
            {
                rotationY = -90.0f;
                running = true;
            }
        }

        else if (checkMovementButtons() > 0.0f)
        {
            if (rotationY < 90.0f)
                turningRight = true;
            else
            {
                rotationY = +90.0f;
                running = true;
            }
        }
        else
        {
            moveVector = Vector3.zero;
            running = false;
        }

        transform.rotation = Quaternion.Euler(0, rotationY, 0);

        // If player is grounded there should be no vertical speed
        if(grounded && !jumping)
            verticalSpeed = 0.0f;

        if ((grounded || jumping) && running)
        {
            idleTimer = 0.0f;

            moveVector.x = checkMovementButtons();
            moveVector.y = verticalSpeed;
            moveVector.z = 0.0f;
            moveVector *= runningForce;

            runningSpeed = moveVector.x;

            if(grounded && !jumping) // Player is moving on the ground
                m_Animator.SetBool("run", true);
        }
        else
        {
            m_Animator.SetBool("run", false);
        }

        if(jump && !grounded)
            jump = false;


        // Player has landed on the ground
        if (jumping && grounded && !jump)
        {
            m_Animator.SetBool("jump", false);
            jumping = false;
            jumpOnPlace = false;

            m_CharacterController.detectCollisions = true;
        }

        // Player has pressed the jump button
        if ((Input.GetKey(KeyCode.UpArrow) || TCKInput.GetAction("ButtonJump", EActionEvent.Down)) && grounded)
            jumping = true;

        // Player has pressed the jump button and is grounded -> start jump
        if (grounded && jumping)
        {
            idleTimer = 0.0f;

            m_Animator.SetBool("jump", true);
            logger.LogPlayerJump(transform.position, Game.GetComponent<AssignController>().controllerNum);

            if (running) // Player will jump while running, set speed without delay
            {
                m_CharacterController.detectCollisions = false;
                verticalSpeed = jumpForce;
                jump = true;
            }
            else // Player will jump on place, initialize "jump delay"
            {
                //verticalSpeed = jumpForce;
                //jumping = true;
                jumpDelay = jumpDelayDef;
                jumpOnPlace = true;
                jumping = false;
                jump = true;
            }
        }

        // Player has not pressed any button in more than five seconds -> start "bored" animation
        if (idleTimer > 5.0f)
            m_Animator.SetBool("idle", true);
        else
            m_Animator.SetBool("idle", false);

        if (!jumping && jumpOnPlace && jumpDelay <= 0)
        {
            jumping = true;
            verticalSpeed = jumpForce;
            m_CharacterController.detectCollisions = false;
        }
        else if(!jumping && jumpOnPlace && jumpDelay > 0)
        {
            jumpDelay = jumpDelay - 1;
        }

        if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("AnimationPlayerAttack"))
        {
            m_Animator.SetBool("attack", false);
            attack = false;
        }
        else
        {
            verticalSpeed = 0.0f;
            moveVector.x += 0.175f;
        }

        // Player has pressed an action button -> start "attack" animation
        if ((Input.GetKey(KeyCode.DownArrow) || TCKInput.GetAction("ButtonAttack", EActionEvent.Down)) && grounded && !jumping && !jumpOnPlace && !attack)
        {
            logger.LogPlayerAttack(transform.position, Game.GetComponent<AssignController>().controllerNum);

            m_Animator.Play("AnimationPlayerAttack");

            m_Animator.SetBool("attack", true);
            attack = true;
        }

        verticalSpeed -= gravity * Time.fixedDeltaTime;

        moveVector.y = verticalSpeed;
        m_CharacterController.Move(moveVector * Time.fixedDeltaTime);

        // Because some animations move the player along all axes, reset Z axis.
        LockZAxis(10.0f);
    }
}
