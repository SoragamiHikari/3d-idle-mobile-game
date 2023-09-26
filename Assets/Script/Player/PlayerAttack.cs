using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    private int numberAttack = 0;
    private float attackDuration;
    public bool attackMode = false;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attackDuration > -0.2f)
        {
            attackDuration -= Time.deltaTime;
            attackMode = true;
        }
        else
        {
            attackMode= false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && attackDuration <= 0)
        {
            numberAttack++;
            ExcuteAttack();
        }
    }

    void ExcuteAttack()
    {
        if(numberAttack == 1)
        {
            attackDuration = 1f;
            anim.SetTrigger("attack");
            StartCoroutine(AttackTime(0.2f));
            //rb.AddRelativeForce(Vector3.forward * 200, ForceMode.Impulse);
        }
        else if(numberAttack == 2)
        {
            attackDuration = 1f;
            anim.SetTrigger("attack2");
            StartCoroutine(AttackTime(0.2f));
            //rb.AddRelativeForce(Vector3.forward * 200, ForceMode.Impulse);
        }
        else if(numberAttack == 3)
        {
            attackDuration = 1.3f;
            anim.SetTrigger("attack3");
            StartCoroutine(AttackTime(0.5f));
            //rb.AddRelativeForce(Vector3.forward * 200, ForceMode.Impulse);
            numberAttack = 0;
        }
    }

    IEnumerator AttackTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.AddRelativeForce(Vector3.forward * 200, ForceMode.Impulse);
        StopCoroutine(AttackTime(delay));
    }
}
