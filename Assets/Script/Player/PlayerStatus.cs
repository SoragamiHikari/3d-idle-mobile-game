using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int hp = 50;
    public int attackPower = 5;
    public int def = 5 ;

    public static int totalAttack;

    // Start is called before the first frame update
    void Start()
    {
        hp= 50;
        attackPower = 5;
        def = 5;
        totalAttack = attackPower + SwordScript.attackPower;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
