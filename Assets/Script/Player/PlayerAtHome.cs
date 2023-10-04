using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtHome : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    public VariableJoystick joystick;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float moveSpeed;

    private float yRotation;
    private Vector3 direction;
    public Transform cameraPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerRotation();
        PlayerMove();
    }

    void PlayerInput()
    {
        horizontalInput = joystick.Horizontal;
        verticalInput = joystick.Vertical;
    }

    void PlayerMove()
    {
        //rb.velocity = new Vector3(horizontalInput, 0, verticalInput).normalized * moveSpeed * Time.deltaTime;
        if (horizontalInput != 0 || verticalInput != 0)
        {
            Vector3 moveForward = Quaternion.Euler(0f, yRotation, 0f) * Vector3.forward;
            rb.velocity = moveForward * moveSpeed * Time.deltaTime;
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
    }

    void PlayerRotation()
    {
        if(horizontalInput!= 0 || verticalInput != 0)
        {
            direction = new Vector3(horizontalInput, 0, verticalInput);
            yRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraPlayer.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        else
        {
            transform.rotation = transform.rotation;
        }
    }
}
