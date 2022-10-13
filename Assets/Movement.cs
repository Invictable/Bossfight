using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    KeyCode forwardMovement = KeyCode.W;
    KeyCode leftMovement = KeyCode.A;
    KeyCode rightMovement = KeyCode.D;
    KeyCode backwardMovement = KeyCode.S;
    KeyCode jump = KeyCode.Space;

    public CharacterController controller;
    public float speed = 1;
    public float turnSmooth = .1f;
    float turnSmoothVelocity;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float jumpButtonGracePeriod;

    private float ySpeed;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private float originalStepOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(hori, 0f, vert).normalized;

        if(dir.magnitude >= .1)
        {
            float cangle = Mathf.Atan2(dir.x, dir.z) *Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cangle,ref turnSmoothVelocity,turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(dir * speed * Time.deltaTime);
        }

        if (controller.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetKey(jump))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            controller.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            controller.stepOffset = 0;
        }


        Vector3 velocity = dir * speed;
        velocity.y = ySpeed;

        controller.Move(velocity * Time.deltaTime);

        if (dir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSmooth * Time.deltaTime);
        }

        controller.SimpleMove(Vector3.forward * 0);

    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
