using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCharacterController : MonoBehaviour {
    //public variables
    public CharacterController controller;
    public Transform playerCam;
    public float crouchCamIncrement = 1f;
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float gravityConstant = -9.81f;
    public float jumpHeight = 2f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public string crouchKey = "left ctrl";
    public string jumpKey = "space";
    public string runKey = "left shift";
    public float crouchHeight = 1.5f;

    // private variables
    private Vector3 velocity;
    private bool onGround;
    private bool crouching;
    private bool running;
    private float originalHeight;
    private float moveSpeed = 12f;
    private Vector3 origCamPos;

    private void Start() {
        originalHeight = controller.height;
        origCamPos = playerCam.localPosition;
    }

    void Jump() {
        velocity.y = Mathf.Sqrt(-2f * jumpHeight * gravityConstant);
    }

    void StartCrouch() {
        controller.center = Vector3.down * (originalHeight - controller.height) / 2f;
        controller.height = crouchHeight;

        float newCamPos = crouching ? origCamPos.y - crouchCamIncrement : origCamPos.y;
        Vector3 newPos = new Vector3(playerCam.localPosition.x, newCamPos, playerCam.localPosition.z);

        playerCam.localPosition = Vector3.Lerp(playerCam.localPosition, newPos, Time.deltaTime * 3);

        crouching = true;
    }

    IEnumerator EndCrouch(float duration) {
        float elapsed = 0f;

        while (elapsed < duration) {
            controller.height = Mathf.Lerp(crouchHeight, originalHeight, elapsed / duration);
            elapsed += Time.deltaTime;
            controller.center = Vector3.down * (originalHeight - controller.height) / 2f;

            float newCamPos = crouching ? origCamPos.y + crouchCamIncrement : origCamPos.y;
            Vector3 newPos = new Vector3(playerCam.localPosition.x, newCamPos, playerCam.localPosition.z);

            playerCam.localPosition = Vector3.Lerp(playerCam.localPosition, newPos, Time.deltaTime * 3);
            yield return null;
        }
        controller.height = originalHeight;
        crouching = false;
    }

    void StartRun() {
        moveSpeed = runSpeed;
        running = true;
    }

    void StopRun() {
        moveSpeed = walkSpeed;
        running = false;
    }

    // Update is called once per frame
    void Update() {
        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (onGround && velocity.y < 0) {
            velocity.y = -2f;
        }

        if (onGround && Input.GetKeyDown(jumpKey)) {
            Jump();
        }

        if (!running && Input.GetKey(crouchKey)) {
            StartCrouch();
            moveSpeed = crouchSpeed;
        } else if (crouching && !Input.GetKey(crouchKey)) {
            StartCoroutine(EndCrouch(0.1f));
            moveSpeed = walkSpeed;
        }

        if (!crouching && onGround && Input.GetKey(runKey)) {
            StartRun();
        } else if (running && !Input.GetKey(runKey)) {
            StopRun();
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);
        velocity.y += gravityConstant * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

}