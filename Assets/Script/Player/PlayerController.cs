using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private PlayerAttack attack;
    private bool takeDemage;

    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float moveSpeed;

    [SerializeField] private AudioClip[] hitVoice;
    private AudioSource hitVoiceSouce;

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
        anim.SetTrigger("takeDemage");
        takeDemage= true;

        yield return new WaitForSeconds(1);
        takeDemage= false;
        StopCoroutine(TakeDemage());
    }
}
