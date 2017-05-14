using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth; // 腳色血量控制
    public GameObject enemy; // 敵人物件
    public float spawnTime = 3f; // 生成的時間
    public Transform[] spawnPoints; // 生成的位置


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime); // 每三秒呼叫一次Spawn函式
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f) // 腳色血量歸零
        {
            return;  // 跳出函式
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length); // 隨機數字

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation); // 生成敵人 (生成點的位置 生成點的旋轉值)
    }
}
