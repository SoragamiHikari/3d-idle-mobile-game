using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAuto : MonoBehaviour
{
    // The target to follow
    public Transform target;

    // The speed of movement
    public float speed = 5f;

    // The distance to stop following
    public float stopDistance = 1f;

    // The tag of the potential targets
    public string targetTag = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerMoveToTarget();
    }

    void PlayerMoveToTarget()
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
                // Stop following if the distance is less than the stop distance
                if (Vector3.Distance(transform.position, target.position) > 2)
                {
                    Vector3 locationDirection = (target.position - transform.position).normalized;
                    float enemyRotation = Mathf.Atan2(locationDirection.x, locationDirection.z) * Mathf.Rad2Deg;

                    // Follow the target with a smooth movement
                    Vector3 direction = target.position - transform.position;
                    direction = direction.normalized;
                    transform.position += direction * speed * Time.deltaTime;
                }
            }
        }
    }

    // Find a target with the specified tag
    void FindTarget()
    {
        // Get all the game objects with the target tag
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

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
            target = closestTarget.transform;
        }
    }
}
