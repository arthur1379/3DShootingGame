using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100; // 敵人起始生命值
    public int currentHealth; // 敵人目前生命值
    public float sinkSpeed = 2.5f; // 死亡時墜落的速度
    public int scoreValue = 10; // 打死敵人獲得的分數
    public AudioClip deathClip; // 死亡音效


    Animator anim; 
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead; // 判斷是否死亡
    bool isSinking; // 但對是否墜落


    void Awake ()
    {
        // 把元件指定進來各自的變數
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth; // 目前血量 = 起始血量
    }


    void Update ()
    {
        if(isSinking) // 若撞落狀態 = T
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime); // 物件往下 (-y) 掉落 掉落速度為sinkSpeed
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint) // 受到傷害的函式(扣血, 子彈打到的位置)
    {
        if(isDead) // 若死亡則跳出函式
            return;

        enemyAudio.Play (); // 撥放受傷的音效

        currentHealth -= amount; // 扣血
            
        hitParticles.transform.position = hitPoint; // 扣血的粒子特效位置 = 子彈打到的位置
        hitParticles.Play(); // 粒子特效撥放

        if(currentHealth <= 0) // 目前血量 <= 0
        {
            Death (); // 死亡函式
        }
    }


    void Death () // 死亡函式
    {
        isDead = true; // 死亡狀態 = T

        capsuleCollider.isTrigger = true; // 碰撞體 實體碰撞功能關閉

        anim.SetTrigger ("Dead"); // 觸發敵人Animator中的Dead變數

        enemyAudio.clip = deathClip; // 敵人音效切換成死亡音效
        enemyAudio.Play (); // 死亡音效撥放
    }


    public void StartSinking () // 掉落函式
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false; // 敵人追蹤關閉
        GetComponent <Rigidbody> ().isKinematic = true; // 物理碰撞功能關閉
        isSinking = true; // 掉落狀態 = T 
        ScoreManager.score += scoreValue; // 敵人死掉就 加10分
        Destroy (gameObject, 2f); // 兩秒後敵人刪除
    }
}
