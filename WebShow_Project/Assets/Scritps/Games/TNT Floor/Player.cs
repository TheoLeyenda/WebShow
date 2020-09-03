using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    public enum TypeMovement
    {
        Position,
        Physics,
    }
    public enum LookDirection
    {
        Left,
        Right,
        Up,
        Down,
    }
    //El salto ejecutara la animacion de salto y desactivara el boxCollider
    public int numberPlayer;
    private float auxSpeed;
    public float speed;
    public float speedJump;
    public bool stopMovementForCode;
    private float auxLinearDrag;
    public Rigidbody2D rigidbody2;
    public Animator animator;
    public BoxCollider2D boxCollider;
    public bool unidirectionalJump;
    public float delayJump;
    private float auxDelayJump;
    //public InventoryPlayer inventoryPlayer;
    public TypeMovement typeMovement;
    [Header("Valor entre el 0 y el 1")]
    public float sensibilityController = 0.1f;
    private bool isJumping;
    [HideInInspector]
    public bool invulnerhabilidad = false;
    bool right = false;
    bool left = false;
    bool up = false;
    bool down = false;

    private LookDirection lookDirection;

    Vector3 rightVector;
    Vector3 leftVector;
    Vector3 upVector;
    Vector3 downVector;

    InventoryPlayer inventoryPlayer;

    public static event Action<InventoryPlayer> OnTakePoint;
    void Start()
    {
        GameObject go = GameObject.Find("InventoryPlayer" + numberPlayer);
        if (go != null)
            inventoryPlayer = go.GetComponent<InventoryPlayer>(); 
        rigidbody2 = GetComponent<Rigidbody2D>();
        auxLinearDrag = rigidbody2.drag;
        rigidbody2.velocity = Vector2.zero;
        auxDelayJump = delayJump;
        rightVector = new Vector3(transform.right.x * speed * Time.deltaTime, 0, 0);
        leftVector = new Vector3(-transform.right.x * speed * Time.deltaTime, 0, 0);
        upVector = new Vector3(0, transform.up.y * speed * Time.deltaTime, 0);
        downVector = new Vector3(0, -transform.up.y * speed * Time.deltaTime, 0);
        auxSpeed = speed;

        if (OnTakePoint != null)
            OnTakePoint(inventoryPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (typeMovement)
        {
            case TypeMovement.Physics:
                MovementPhysics();
                break;
            case TypeMovement.Position:
                MovementPositions();
                break;
        }
        CheckAnimations();
        if (isJumping)
        {
            CheckJumpDelay();
           
        }
    }
    
    void CheckJumpDelay()
    {
        if (delayJump > 0)
        {
            //boxCollider.enabled = false;
            delayJump = delayJump - Time.deltaTime;
            speed = speedJump;
        }
        else
        {
            //boxCollider.enabled = true;
            delayJump = auxDelayJump;
            isJumping = false;
            invulnerhabilidad = false;
            speed = auxSpeed;
        }
    }
    void CheckAnimations()
    {
        if (!isJumping)
        {
            //Debug.Log("HOLA CAPO");

            if (right || left)
            {
                if (right)
                {
                    animator.Play("WalkRight");
                    lookDirection = LookDirection.Right;
                }
                if (left)
                {
                    animator.Play("WalkLeft");
                    lookDirection = LookDirection.Left;
                }
            }
            else if (up || down)
            {
                if (up)
                {
                    animator.Play("WalkUp");
                    lookDirection = LookDirection.Up;
                }
                if (down)
                {
                    animator.Play("WalkDown");
                    lookDirection = LookDirection.Down;
                }
            }
            else if(!right && !left && !down && !up)
            {
                switch (lookDirection)
                {
                    case LookDirection.Down:
                        animator.Play("IdleDown");
                        break;
                    case LookDirection.Up:
                        animator.Play("IdleUp");
                        break;
                    case LookDirection.Left:
                        animator.Play("IdleLeft");
                        break;
                    case LookDirection.Right:
                        animator.Play("IdleRight");
                        break;
                }
            }
        }
        else
        {
            if (right || left)
            {
                if (right)
                {
                    animator.Play("JumpRight");
                    lookDirection = LookDirection.Right;
                }
                if (left)
                {
                    animator.Play("JumpLeft");
                    lookDirection = LookDirection.Left;
                }
            }
            else if (up || down)
            {
                if (up)
                {
                    animator.Play("JumpUp");
                    lookDirection = LookDirection.Up;
                }
                if (down)
                {
                    animator.Play("JumpDown");
                    lookDirection = LookDirection.Down;
                }
            }
        }
    }
   
    void MovementPositions()
    {
        CheckInputMovement();

        rigidbody2.velocity = Vector2.zero;
        if (right)
        {
            transform.position = transform.position + rightVector;
        }
        if (left)
        {
            transform.position = transform.position + leftVector;
        }
        if (up)
        {
            transform.position = transform.position + upVector;
        }
        if (down)
        {
            transform.position = transform.position + downVector;
        }

        CheckInputJump();

    }
    void MovementPhysics()
    {
        CheckInputMovement();

        if (right)
        {
            rigidbody2.AddForce(transform.right * speed * Time.deltaTime, ForceMode2D.Force);
        }
        if (left)
        {
            rigidbody2.AddForce(-transform.right * speed * Time.deltaTime, ForceMode2D.Force);
        }
        if (up)
        {
            rigidbody2.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Force);
        }
        if (down)
        {
            rigidbody2.AddForce(-transform.up * speed * Time.deltaTime, ForceMode2D.Force);
        }
        if (!right && !left && !up && !down && stopMovementForCode)
        {
            rigidbody2.velocity = Vector2.zero;
            //rigidbody2.drag = 1000;
        }
        else if(stopMovementForCode)
        {
            rigidbody2.drag = auxLinearDrag;
        }

        CheckInputJump();
    }

    public void CheckInputMovement()
    {
        if (!isJumping || !unidirectionalJump)
        {
            right = Input.GetAxis("Horizontal") > sensibilityController;
            left = Input.GetAxis("Horizontal") < -sensibilityController;
            up = Input.GetAxis("Vertical") > sensibilityController;
            down = Input.GetAxis("Vertical") < -sensibilityController;
        }
    }
    public void CheckInputJump()
    {
        if (!isJumping && unidirectionalJump && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            invulnerhabilidad = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Coin")
        {
            if (inventoryPlayer != null)
            {
                inventoryPlayer.currentCoin++;
                if (OnTakePoint != null)
                {
                    OnTakePoint(inventoryPlayer);
                }
            }
            Destroy(collider.gameObject);
        }
    }
}
