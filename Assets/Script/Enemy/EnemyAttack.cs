using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool attaking;
    public GameObject enemySword;
    private CapsuleCollider swordCollider;

    private Animator anim;
    private Enemy enemyBe;
    public float attakDelay = 2;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        swordCollider = enemySword.GetComponent<CapsuleCollider>();

        enemyBe= GetComponent<Enemy>();
        swordCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyBe.readyState && !enemyBe.isLose)
        {
            attakDelay -= Time.deltaTime;
            if(attakDelay <= 0)
            {
                StartCoroutine(ExcuteAttack());
            }
        }
    }

    IEnumerator ExcuteAttack()
    {
        attakDelay = 2.5f;
        attaking = true;
        enemyBe.readyState= false;
        anim.SetTrigger("attack");

        yield return new WaitForSeconds(0.2f);
        swordCollider.enabled = true;

        yield return new WaitForSeconds(0.3f);
        swordCollider.enabled = false;

        yield return new WaitForSeconds(1);
        attaking= false;
        enemyBe.readyState = true;
        StopCoroutine(ExcuteAttack());
    }
}
