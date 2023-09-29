using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    public bool attackMode = false;
    private int numberAttack = 0;
    private float attackDuration;

    public GameObject sword;
    private CapsuleCollider capsuleColliderSword;
    public GameObject backSword;
    public ParticleSystem swordStartFx;
    public ParticleSystem backSwordStartFx;

    [SerializeField] private AudioClip[] attackVoice;
    [SerializeField] private AudioClip attackSoundFx;
    private AudioSource attackSource;

    //Extension
    private float delaySwordTransform = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();
        attackSource= GetComponent<AudioSource>();
        capsuleColliderSword = sword.GetComponent<CapsuleCollider>();

        capsuleColliderSword.enabled = false;
        sword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SwichAttackOrNot();

        if (Input.GetKeyDown(KeyCode.Space) && attackDuration <= 0.2f)
        {
            numberAttack++;
            ExcuteAttack();
        }
    }

    public void TouchAttackInptu()
    {
        if(attackDuration <= 0.2f)
        {
            numberAttack++;
            ExcuteAttack();
        }
    }

    void SwichAttackOrNot()
    {
        if (attackDuration > -0.2f)
        {
            attackDuration -= Time.deltaTime;

            attackMode = true;
            if(backSword.activeSelf == true && sword.activeSelf == false)
            {

                swordStartFx.Play();
                backSwordStartFx.Play();
                backSword.SetActive(false);
                sword.SetActive(true);
            }
            delaySwordTransform = 2;
        }
        else
        {
            attackMode = false;
            if (delaySwordTransform > 0)
            {
                delaySwordTransform -= Time.deltaTime;
            }
            else
            {
                if(sword.activeSelf== true && backSword.activeSelf == false)
                {
                    swordStartFx.Play();
                    backSwordStartFx.Play();
                    sword.SetActive(false);
                    backSword.SetActive(true);
                }
                
            }
        }
    }

    void ExcuteAttack()
    {
        if(numberAttack == 1)
        {
            attackDuration = 1.2f;
            anim.SetTrigger("attack");
            StartCoroutine(AttackTime(0.3f));// delay addforce
        }
        else if(numberAttack == 2)
        {
            attackDuration = 1.1f;
            anim.SetTrigger("attack2");
            StartCoroutine(AttackTime(0.3f));// delay addforce
        }
        else if(numberAttack == 3)
        {
            attackDuration = 1.15f;
            anim.SetTrigger("attack3");
            StartCoroutine(AttackTime(0.55f));// delay addforce
            numberAttack = 0;
        }
    }

    IEnumerator AttackTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        attackSource.PlayOneShot(attackVoice[Random.Range(0, attackVoice.Length)]);
        attackSource.PlayOneShot(attackSoundFx, 0.5f);
        rb.AddRelativeForce(Vector3.forward * 180, ForceMode.Impulse);
        capsuleColliderSword.enabled = true;

        yield return new WaitForSeconds(0.4f);// delay false collider sword
        capsuleColliderSword.enabled = false;
        StopCoroutine(AttackTime(delay));
    }
}
