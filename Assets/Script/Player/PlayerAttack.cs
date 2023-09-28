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
    private AudioSource attackVoiceSource;

    //Extension
    private int oneTime;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();
        attackVoiceSource= GetComponent<AudioSource>();
        capsuleColliderSword = sword.GetComponent<CapsuleCollider>();

        capsuleColliderSword.enabled = false;
        sword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SwichAttackOrNot();

        if(Input.GetKeyDown(KeyCode.Space) && attackDuration <= 0)
        {
            numberAttack++;
            ExcuteAttack();
        }
    }

    public void TouchAttackInptu()
    {
        if(attackDuration <= 0)
        {
            numberAttack++;
            ExcuteAttack();
        }
    }

    void SwichAttackOrNot()
    {
        if (attackDuration > -0.25f)
        {
            attackDuration -= Time.deltaTime;
            if (oneTime == 0)
            {
                swordStartFx.Play();
                backSwordStartFx.Play();
                oneTime = 1;
            }
            attackMode = true;
            backSword.SetActive(false);
            sword.SetActive(true);
        }
        else
        {
            sword.SetActive(false);
            if (oneTime == 1)
            {
                swordStartFx.Play();
                backSwordStartFx.Play();
                oneTime = 0;
            }
            backSword.SetActive(true);
            attackMode = false;
        }
    }

    void ExcuteAttack()
    {
        if(numberAttack == 1)
        {
            attackDuration = 1.4f;
            anim.SetTrigger("attack");
            StartCoroutine(AttackTime(0.3f));// delay addforce
        }
        else if(numberAttack == 2)
        {
            attackDuration = 1.3f;
            anim.SetTrigger("attack2");
            StartCoroutine(AttackTime(0.3f));// delay addforce
        }
        else if(numberAttack == 3)
        {
            attackDuration = 1.5f;
            anim.SetTrigger("attack3");
            StartCoroutine(AttackTime(0.55f));// delay addforce
            numberAttack = 0;
        }
    }

    IEnumerator AttackTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        attackVoiceSource.PlayOneShot(attackVoice[Random.Range(0, attackVoice.Length)]);
        rb.AddRelativeForce(Vector3.forward * 150, ForceMode.Impulse);
        capsuleColliderSword.enabled = true;

        yield return new WaitForSeconds(0.4f);// delay false collider sword
        capsuleColliderSword.enabled = false;
        StopCoroutine(AttackTime(delay));
    }
}
