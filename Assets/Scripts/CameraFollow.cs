using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Vector3 cameraOffset;
    [SerializeField]
    [Range(0.01f, 1.0f)]
    private float SmoothFactor;

    [SerializeField]
    private Camera camera;
    private Vector3 DragCamera;
    private Transform dummyTarget;
    public bool MovementSmoothing = true;
    public bool RotationSmoothing = false;
    private bool previousSmoothing;
    private float mouseX;
    private float mouseY;
    private Vector3 moveVector;
    void Start()
    {
        cameraOffset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!Joystick.isJoystick || !player) return;
        Vector3 Pos = player.position + cameraOffset;
        Pos.x = 0;
        transform.position = Vector3.Slerp(transform.position, Pos, SmoothFactor);
    }
    private void Update()
    {
        if (Joystick.isJoystick) return;
        DragCam();
    }

    private void DragCam()
    {
        if (Input.GetMouseButton(2) || Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            mouseY = Input.GetAxis("Mouse Y");
            mouseX = Input.GetAxis("Mouse X");

            moveVector = transform.TransformDirection(0, mouseY, 0);
            moveVector = new Vector3(moveVector.x, 0, moveVector.z);

            transform.Translate(-moveVector, Space.World);

            // This shit consumed like 7-10ms of processor time when was called each frame.
            //Debug.Log("Drag");
        }
    }
}
