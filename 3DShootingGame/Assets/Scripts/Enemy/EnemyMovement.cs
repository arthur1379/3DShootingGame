using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player; // 呼叫敵人角色的Transform
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;  // 呼叫敵人角色上的NavMeshAgent


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform; // 指定Player進來
        playerHealth = player.GetComponent <PlayerHealth> (); // 主角物件 PlayerHealth程式
        enemyHealth = GetComponent <EnemyHealth> (); // 敵人物件 EnemyHealth程式
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> (); // 指定敵人物件的NavMeshAgent元件
    }


    void Update ()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) // 如果敵人血量未歸零 和 主角血量未歸零
        {
            nav.SetDestination (player.position); // 追蹤功能開啟
        }
        else
        {
            nav.enabled = false; // 敵人死亡後 追蹤功能關閉
        }
    }
}
