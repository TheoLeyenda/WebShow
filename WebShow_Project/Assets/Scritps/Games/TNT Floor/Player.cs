using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{

    public enum TypeMovement
    {
        Position,
        Physics,
    }
    //El salto ejecutara la animacion de salto y desactivara el boxCollider
    public int numberPlayer;
    public float speed;
    public float speedJump;
    public bool stopMovementForCode;
    private float auxLinearDrag;
    public Rigidbody2D rigidbody2;
    public Animator animator;
    //public InventoryPlayer inventoryPlayer;
    public TypeMovement typeMovement;
    [Header("Valor entre el 0 y el 1")]
    public float sensibilityController = 0.1f;
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        auxLinearDrag = rigidbody2.drag;
        rigidbody2.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimations();
        switch (typeMovement)
        {
            case TypeMovement.Physics:
                MovementPhysics();
                break;
            case TypeMovement.Position:
                MovementPositions();
                break;
        }
            
    }
    void CheckAnimations()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > sensibilityController)
            {
                animator.Play("WalkLeft");
            }
            if (Input.GetAxis("Horizontal") < -sensibilityController)
            {
                animator.Play("WalkRight");
            }
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Vertical") > sensibilityController)
            {
                animator.Play("WalkUp");
            }
            if (Input.GetAxis("Vertical") < -sensibilityController)
            {
                animator.Play("WalkDown");
            }
        }
    }
    void MovementPositions()
    {
        bool right = Input.GetAxis("Horizontal") > sensibilityController;
        bool left = Input.GetAxis("Horizontal") < -sensibilityController;
        bool up = Input.GetAxis("Vertical") > sensibilityController;
        bool down = Input.GetAxis("Vertical") < -sensibilityController;
        rigidbody2.velocity = Vector2.zero;
        if (right)
        {
            transform.position = transform.position + new Vector3(transform.right.x * speed * Time.deltaTime,0, 0);
        }
        if (left)
        {
            transform.position = transform.position + new Vector3(-transform.right.x * speed * Time.deltaTime, 0, 0);
        }
        if (up)
        {
            transform.position = transform.position + new Vector3(0, transform.up.y * speed * Time.deltaTime, 0);
        }
        if (down)
        {
            transform.position = transform.position + new Vector3(0, -transform.up.y * speed * Time.deltaTime, 0);
        }

    }
    void MovementPhysics()
    {
        bool right = Input.GetAxis("Horizontal") > sensibilityController;
        bool left = Input.GetAxis("Horizontal") < -sensibilityController;
        bool up = Input.GetAxis("Vertical") > sensibilityController;
        bool down = Input.GetAxis("Vertical") < -sensibilityController;
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
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Coin")
        {
            GameObject go = GameObject.Find("InventoryPlayer" + numberPlayer);
            if (go != null)
            {
                InventoryPlayer inventoryPlayer = go.GetComponent<InventoryPlayer>();
                if (inventoryPlayer != null)
                {
                    inventoryPlayer.currentCoin++;
                }
            }
            Destroy(collider.gameObject);
        }
    }
}
