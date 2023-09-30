using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemyPrefabs;
    public GameObject[] enemyInArena;
    private GameObject player;
    private MainManager mainManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        mainManager = GetComponent<MainManager>();

        InvokeRepeating("ChangePositionAndCountEnemy", 1, 1);
        Invoke("SpawnEnemy", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangePositionAndCountEnemy()
    {
        float x = Random.Range(-85, 85);
        float z = Random.Range(-85, 85);
        transform.position = new Vector3(x, 2, z);

        enemyInArena = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void SpawnEnemy()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > 40 && enemyInArena.Length < 20)
        {
            Instantiate(EnemyPrefabs[0], transform.position, Quaternion.identity);
        }
        Invoke("SpawnEnemy", 3);
    }
}
