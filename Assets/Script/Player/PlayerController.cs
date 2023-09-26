using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private PlayerAttack attack;

    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();
        attack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerMove();
        PlayerRotation();
    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void PlayerMove()
    {
        if(!attack.attackMode)
        {
            rb.velocity = new Vector3(horizontalInput, 0, verticalInput).normalized * moveSpeed * Time.deltaTime;
            if (horizontalInput != 0 || verticalInput != 0)
            {
                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("run", false);
            }
        }
        else
        {
            horizontalInput = 0;
            verticalInput = 0;
            anim.SetBool("run", false);
        }    
    }

    void PlayerRotation()
    {
        float playerRotation = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
        if(horizontalInput!= 0 || verticalInput != 0)
        {
            transform.rotation = Quaternion.Euler(0f, playerRotation, 0f);
        }
        else
        {
            transform.rotation = transform.rotation;
        }
    }

}
