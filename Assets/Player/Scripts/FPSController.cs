using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed;
    public float runSpeed;
    public float jumpPower;
    public float gravity;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    //camera effect
    public float bobbingAmount = 0.1f;
    public float bobbingSpeed = 5.0f;
    private Vector3 cameraOrigin;
    private float bobbingOffset = 0.0f;

    CharacterController characterController;

    public AudioClip music;
    public AudioSource player;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.clip = music;
        player.Play();
    }

    void Update()
    {

        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        ApplyBobbing(curSpeedX);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);           
        }

        #endregion
    }

    void ApplyBobbing(float verticalInput)
    {
        float targetBobbingOffset = 0f;

        if (Mathf.Abs(verticalInput) > 0.01f && characterController.isGrounded)
        {
            targetBobbingOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;
        }

        bobbingOffset = Mathf.Lerp(bobbingOffset, targetBobbingOffset, Time.deltaTime * 10f);

        Camera.main.transform.localPosition = cameraOrigin + Vector3.up * bobbingOffset;
    }
 
    public float GetCurrentSpeed()
    {
        return Math.Abs(characterController.velocity.magnitude);
    }
}
