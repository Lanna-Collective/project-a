using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //public variables
    public CharacterController controller;
    public float speed = 12f;
    public float gravityConstant = -9.81f;
    public float jumpHeight = 2f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // private variables
    private Vector3 velocity;
    private bool onGround;

    // Update is called once per frame
    void Update() {

        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (onGround && velocity.y < 0) {
            velocity.y = -2f;
        }
        if (onGround && Input.GetButtonDown("Jump")) {
            velocity.y = Mathf.Sqrt(-2f * jumpHeight * gravityConstant);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravityConstant * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}