using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefabs;
    private GameObject player;
    private float spawnInterval = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Invoke("ChangePosition", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > 50)
        {
            SpawnEnemy();
        }
    }

    void ChangePosition()
    {
        float x = Random.Range(-110, 110);
        float z = Random.Range(-110, 110);
        transform.position = new Vector3(x, 2, z);

        Invoke("ChangePosition", 1);
    }

    void SpawnEnemy()
    {
        if(spawnInterval > 0)
        {
            spawnInterval -= Time.deltaTime;
        }
        else
        {
            Instantiate(EnemyPrefabs, transform.position, Quaternion.identity);
            spawnInterval = 3;
        }
    }
}
