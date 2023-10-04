using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMainManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject interectButton;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerAtHome>().enabled = true;

        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerAttack>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        interectButton.SetActive(true);
    }

    public void GoTOBattel()
    {
        SceneManager.LoadScene("Main_1");
    }
}
