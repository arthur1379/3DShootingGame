using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player; // 呼叫敵人角色的Transform
    //PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;  // 呼叫敵人角色上的NavMeshAgent


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform; // 指定Player進來
        //playerHealth = player.GetComponent <PlayerHealth> ();
        //enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> (); // 指定敵人物件的NavMeshAgent元件
    }


    void Update ()
    {
        //if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        //{
            nav.SetDestination (player.position); // 讓
        //}
        //else
        //{
        //    nav.enabled = false;
        //}
    }
}
