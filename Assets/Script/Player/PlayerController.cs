using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private PlayerAttack attack;
    public bool takeDemage;

    public Text switchButtonText;
    public bool mobileInput;
    private float horizontalInput;
    private float verticalInput;
    public VariableJoystick joystick;
    private float horizontalJoystickInput;
    private float verticalJoystickInput;
    [SerializeField] private float moveSpeed;

    [SerializeField] private AudioClip[] hitVoice;
    [SerializeField] private AudioClip takeHit;
    private AudioSource hitVoiceSouce;

    public GameObject target;
    public bool autoMove;
    public Text autoMoveText;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();
        attack = GetComponent<PlayerAttack>();
        hitVoiceSouce= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        JoystickInput();
    }

    private void FixedUpdate()
    {
        if(mobileInput && !autoMove)
        {
            PlayerMoveJoystick();
            PlayerRotationWithJoystick();
        }
        else if(!mobileInput && !autoMove)
        {
            PlayerMove();
            PlayerRotation();
        }
        else
        {
            PlayerAutoMove();
        }
    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void JoystickInput()
    {
        horizontalJoystickInput = joystick.Horizontal;
        verticalJoystickInput = joystick.Vertical;
    }

    void PlayerMove()
    {
        if(!attack.attackMode && !takeDemage)
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

    void PlayerMoveJoystick()
    {
        if (!attack.attackMode && !takeDemage)
        {
            rb.velocity = new Vector3(horizontalJoystickInput, 0, verticalJoystickInput).normalized * moveSpeed * Time.deltaTime;
            if (horizontalJoystickInput != 0 || verticalJoystickInput != 0)
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
            horizontalJoystickInput = 0;
            verticalJoystickInput = 0;
            anim.SetBool("run", false);
        }
    }

    void PlayerRotation()
    {
        float playerRotation = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
        if (horizontalInput != 0 || verticalInput != 0)
        {
            transform.rotation = Quaternion.Euler(0f, playerRotation, 0f);
        }
        else
        {
            transform.rotation = transform.rotation;
        }
    }

    void PlayerRotationWithJoystick()
    {
        float playerRotation = Mathf.Atan2(horizontalJoystickInput, verticalJoystickInput) * Mathf.Rad2Deg;
        if (horizontalJoystickInput != 0 || verticalJoystickInput != 0)
        {
            transform.rotation = Quaternion.Euler(0f, playerRotation, 0f);
        }
        else
        {
            transform.rotation = transform.rotation;
        }
    }

    public void SwitchInput()
    {
        if(mobileInput)
        {
            mobileInput= false;
            switchButtonText.text = "Keyboard Input";
        }
        else
        {
            mobileInput= true;
            switchButtonText.text = "Mobile Input";
        }
    }

    public void AutoMoveSwitch()
    {
        if(autoMove)
        {
            autoMove = false;
            autoMoveText.text = "Manual";
        }
        else
        {
            autoMove = true;
            autoMoveText.text = "Auto Move";
        }
    }

    void PlayerAutoMove()
    {
        // If there is no target, find one
        if (target == null)
        {
            FindTarget();
        }
        else
        {
            // If the target is destroyed, set it to null
            if (target.gameObject.activeSelf == false)
            {
                target = null;
            }
            else
            {
                Vector3 direction = target.transform.position - transform.position;
                float playerRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, playerRotation, 0);
                if (!attack.attackMode && !takeDemage)
                {
                    if (Vector3.Distance(transform.position, target.transform.position) > 2.5f)
                    {
                        anim.SetBool("run", true);
                        rb.velocity = direction.normalized * moveSpeed * Time.deltaTime;
                    }
                    else
                    {
                        anim.SetBool("run", false);
                        attack.TouchAttackInptu();
                    }
                    
                }
                else
                {
                    anim.SetBool("run", false);
                }
            }
        }
    }

    // Find a target with the specified tag
    void FindTarget()
    {
        // Get all the game objects with the target tag
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        // If there are any targets, choose the closest one
        if (targets.Length > 0)
        {
            float minDistance = Mathf.Infinity;
            GameObject closestTarget = null;

            // Loop through all the targets and find the closest one
            foreach (GameObject t in targets)
            {
                float distance = Vector3.Distance(transform.position, t.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = t;
                }
            }

            // Set the target to the closest one
            target = closestTarget;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemySword"))
        {
            StartCoroutine(TakeDemage());
        }
    }

    IEnumerator TakeDemage()
    {
        anim.SetBool("run", false);
        hitVoiceSouce.PlayOneShot(hitVoice[Random.Range(0, hitVoice.Length)]);
        hitVoiceSouce.PlayOneShot(takeHit);
        anim.SetTrigger("takeDemage");
        takeDemage= true;

        yield return new WaitForSeconds(1.3f);
        takeDemage= false;
        StopCoroutine(TakeDemage());
    }
}
