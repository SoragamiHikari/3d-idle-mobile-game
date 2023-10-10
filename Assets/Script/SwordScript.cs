using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public GameObject playerPosition;
    [SerializeField] private ParticleSystem hitFx;
    [SerializeField] private GameObject hitPoint;

    //attack power
    public static int attackPower = 5;

    // Start is called before the first frame update
    void Start()
    {
        attackPower = 5;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRB = other.gameObject.GetComponent<Rigidbody>();
            Instantiate(hitFx, hitPoint.transform.position, Quaternion.identity);
            Vector3 opositeDirection = other.transform.position - playerPosition.transform.position;

            enemyRB.AddForce(opositeDirection.normalized * 100, ForceMode.Impulse);
        }
    }
}
