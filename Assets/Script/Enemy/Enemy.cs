using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    private Animator anim;

    public bool readyState = false;
    private EnemyAttack enemyAttack;

    [SerializeField] private AudioClip takeHit;
    private AudioSource audioSource;

    [SerializeField] private float maxDistance;
    [SerializeField] private float moveSpeed;
    public bool isLose = false;

    //status
    [SerializeField] private int hp = 50;
    [SerializeField] private int attackPower = 5;
    [SerializeField] private int def = 5;


    //Extension
    private int oneTime = 0;
    private float moveDelay = 2;

    // Start is called before the first frame update
    void Start()
    {
        hp= 50;
        attackPower= 5;
        def = 5;

        rb= GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();
        enemyAttack = GetComponent<EnemyAttack>();
        audioSource= GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        if(!isLose)
        {
            MoveBehavier();
        }
    }

    void MoveBehavier()
    {
        Vector3 locationDirection = (player.transform.position - rb.position).normalized;
        float enemyRotation = Mathf.Atan2(locationDirection.x, locationDirection.z) * Mathf.Rad2Deg;

        if(moveDelay >= 0)
        {
            moveDelay -= Time.deltaTime;
        }

        if(!enemyAttack.attaking)
        {
            transform.rotation = Quaternion.Euler(0, enemyRotation, 0);
            if (Vector3.Distance(transform.position, player.transform.position) > 2 && Vector3.Distance(transform.position, player.transform.position) < 40 && moveDelay <= 0)
            {
                rb.velocity = locationDirection * moveSpeed * Time.deltaTime;
                anim.SetBool("run", true);
                readyState = false;
                oneTime= 0;
            }
            else if (oneTime == 0)
            {
                moveDelay = 2;
                anim.SetBool("run", false);
                readyState = true;
                oneTime= 1;
            }
        }
        else if(enemyAttack.attaking)
        {
            readyState= false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerSword") && !isLose)
        {
            int demage = PlayerStatus.totalAttack - def;
            hp -= demage;
            audioSource.PlayOneShot(takeHit, 0.5f);
            StartCoroutine(ToRedyState(1));
            if (hp <= 0)
            {
                StartCoroutine(BeforeDestroy());
            }
            Debug.Log(demage);
        }
    }

    IEnumerator ToRedyState(float timeTakeHit)
    {
        readyState= false;
        anim.SetTrigger("takeHit");

        yield return new WaitForSeconds(timeTakeHit);
        readyState=true;
        enemyAttack.attakDelay = 2;
        StopCoroutine(ToRedyState(timeTakeHit));
    }

    IEnumerator BeforeDestroy()
    {
        isLose = true;
        gameObject.tag = "Untagged";
        gameObject.layer = 0;
        Destroy(rb);
        GetComponent<BoxCollider>().enabled = false;
        anim.SetTrigger("lose");
        player.GetComponent<PlayerController>().target= null;

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
