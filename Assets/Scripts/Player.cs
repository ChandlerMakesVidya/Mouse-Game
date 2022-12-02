using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Chandler Hummingbird
 * Date Created: Dec 07, 2020
 * Date Modified: Dec 07, 2020
 * Description: This script allows the player character to move and look around.
 */

public class Player : MonoBehaviour
{
    float moveInputX, moveInputY, lookInputX, lookInputY;
    bool sprintInput;
    Vector3 moveVector;
    CharacterController charController;
    Camera viewCam;
    float xAxisClamp;

    public float moveSpeed;
    public float sprintSpeed;
    public float smellCooldown;
    public bool dead;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        viewCam = Camera.main;
    }

    private void Update()
    {
        if (GameManager.GM.gameState == GameManager.GameState.Playing)
        {
            moveInputX = Input.GetAxisRaw("Horizontal");
            moveInputY = Input.GetAxisRaw("Vertical");
            lookInputX = Input.GetAxisRaw("Mouse X") * GameManager.GM.mouseSensitivity * Time.deltaTime;
            lookInputY = Input.GetAxisRaw("Mouse Y") * GameManager.GM.mouseSensitivity * Time.deltaTime;
            sprintInput = Input.GetButton("Sprint");

            Movement();
            CameraRotation();
        }
    }

    private void Movement()
    {
        moveVector = (transform.forward * moveInputY + transform.right * moveInputX).normalized;
        moveVector *= !sprintInput ? moveSpeed : sprintSpeed;

        charController.SimpleMove(moveVector);
    }

    private void CameraRotation()
    {
        xAxisClamp += lookInputY;

        if(xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            lookInputY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if(xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            lookInputY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        viewCam.transform.Rotate(Vector3.left * lookInputY);
        transform.Rotate(Vector3.up * lookInputX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = viewCam.transform.eulerAngles;
        eulerRotation.x = value;
        viewCam.transform.eulerAngles = eulerRotation;
    }
}
