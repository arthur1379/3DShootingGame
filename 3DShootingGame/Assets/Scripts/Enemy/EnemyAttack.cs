using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f; // 攻擊的時間間隔 0.5
    public int attackDamage = 10; // 攻擊的傷害值


    Animator anim; // 敵人的Animator
    GameObject player; // 主角的物件
    PlayerHealth playerHealth; // 主角物件上的 playerHealth.cs
    EnemyHealth enemyHealth; 
    bool playerInRange; // 是否再攻擊範圍裡面
    float timer; // 時間計算


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player"); // 找到主角物件
        playerHealth = player.GetComponent <PlayerHealth> (); // 指定主角物件上的PlayerHealth
        print(player);
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> (); // 敵人自己的Animator
    }


    void OnTriggerEnter (Collider other) // 球形Collider偵測到有物體進入
    {
        if(other.gameObject == player) // 如果碰到的是主角
        {
            playerInRange = true; // 可以攻擊
        }
    }


    void OnTriggerExit (Collider other) // 球形Collider偵測到有物體離開
    {
        if(other.gameObject == player) // 如果碰到的是主角
        {
            playerInRange = false; // 不可以攻擊
        }
    }


    void Update ()
    {
        timer += Time.deltaTime; // 時間累加

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0) // 時間 >= 攻擊攻擊時間間隔 && 可以攻擊
        {
            Attack (); // 觸發攻擊的函式
        }

        if(playerHealth.currentHealth <= 0) // 若角色的血量歸零 或小於0
        {
            anim.SetTrigger ("PlayerDead"); // 敵人動畫 PlayerDead Trigger被觸發 變成閒置動畫
        }
    }


    void Attack () // 攻擊函式
    {
        timer = 0f; // 時間變為0

        if(playerHealth.currentHealth > 0) // 若角色血量未歸零
        {
            playerHealth.TakeDamage (attackDamage); // 呼叫PlayerHealth.cs中的TakeDamage 攻擊數值為attackDamage
        }
    }
}
