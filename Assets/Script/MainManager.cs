using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<PlayerAttack>().enabled = true;

        player.GetComponent<PlayerAtHome>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
