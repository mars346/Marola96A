using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded = false;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 5f;

    private Vector2 direction = Vector2.zero;

    void Start()
    {
        rb = GetComponent <Rigidbody>();
    }

    void OnMove(InputValue value)
    {
        Vector2 direction = value.Get<Vector2>();
        Debug.Log(direction);
        this.direction = direction;

        float movementX = this.direction.x;
        float movementY = this.direction.y;
    }

    void Update()
    {
        Move(direction.x, direction.y);
    }

    private void Move(float x, float z)
    {
        rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed);
    }

    void OnJump()
    {
        if(isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    void OnCollisionStay (Collision collision)
    {
        if(Vector3.Angle(collision.GetContact(0).normal, Vector3.up) < 45f)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}