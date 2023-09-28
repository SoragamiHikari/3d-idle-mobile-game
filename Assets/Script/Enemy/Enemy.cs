using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    private Animator anim;

    public bool readyState = true;
    private EnemyAttack enemyAttack;

    [SerializeField] private float maxDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int hp = 3;

    //Extension
    private int oneTime = 0;
    private float moveDelay = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();
        enemyAttack = GetComponent<EnemyAttack>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveBehavier();
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
            if (Vector3.Distance(transform.position, player.transform.position) > 2 && moveDelay <= 0)
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
        if(other.gameObject.CompareTag("PlayerSword"))
        {
            hp--;
            StartCoroutine(ToRedyState(1));
            if(hp <= 0)
            {
                Destroy(gameObject);
            }
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
}
