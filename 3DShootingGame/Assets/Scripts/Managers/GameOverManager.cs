using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth; // 腳色血量
    public float restartDelay = 5f; // 重新開始的延遲時間

    Animator anim; //  UI動畫
    float restartTimer; //  重新開始的時間

    void Awake()
    {
        anim = GetComponent<Animator>(); 
    }


    void Update()
    { 
        if (playerHealth.currentHealth <= 0) // 血量為0
        {
            anim.SetTrigger("GameOver"); // 觸發Trigger

            restartTimer += Time.deltaTime; // 時間計算

            if (restartTimer >= restartDelay) // 時間大於延遲時間
            {
                Application.LoadLevel(Application.loadedLevel); // 呼叫場景
            }
        }
    }
}
