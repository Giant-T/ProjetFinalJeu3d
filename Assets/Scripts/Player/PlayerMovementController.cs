using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravity = -10f;
    [SerializeField] private float decelaration = 0.95f;

    [Header("Ground Checking")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundLayer;

    private Vector3 velocity = Vector3.zero;
    private bool isOnGround = false;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isOnGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        GetInputs();

        if (isOnGround && velocity.y < 0)
            velocity.y = -1f;
        
        CalcGravity();

        Move();

        velocity *= decelaration * Time.deltaTime;
    }

    private void GetInputs()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move *= speed * Time.deltaTime;

        velocity += move;
    }

    private void CalcGravity()
    {
        velocity.y += gravity * Time.deltaTime;
    }

    private void Move()
    {
        controller.Move(velocity);
    }
}
