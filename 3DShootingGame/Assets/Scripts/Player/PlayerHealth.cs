using UnityEngine;
using UnityEngine.UI; // UI函式庫
using System.Collections;
using UnityEngine.SceneManagement; // 場景管理函式庫


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100; // 起始的生命值
    public int currentHealth; // 目前的生命值
    public Slider healthSlider; // 場景的HealthSlider
    public Image damageImage; // 場景的DamageImage
    public AudioClip deathClip; // 死亡音效
    public float flashSpeed = 5f; // 受到傷害的閃爍後回覆的時間
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f); // 閃爍的顏色 紅色 Alpha = 0.1


    Animator anim; // 角色動畫
    AudioSource playerAudio; // 角色音效
    PlayerMovement playerMovement; // 角色控制的程式
    PlayerShooting playerShooting; // 角色射擊的程式
    bool isDead; // 判斷是否死亡
    bool damaged; // 判斷是否受到傷害


    void Awake ()
    {
        anim = GetComponent <Animator> (); 
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth; // 起始血量 = 現在血量
    }


    void Update ()
    {
        if(damaged) // 受到傷害 = T
        {
            damageImage.color = flashColour; // 傷害畫面
        }
        else // 未受到傷害
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime); // 將顏色漸變回 原本的完全透明
        }
        damaged = false; //受到傷害關閉
    }


    public void TakeDamage (int amount) // 受到傷害 需要在別的程式呼叫
    { 
        damaged = true; // 受到傷害 = T

        currentHealth -= amount; // 目前的傷害 - 受到的傷害

        healthSlider.value = currentHealth; // 將目前血量值指定給healthSlider.value

        playerAudio.Play (); // 角色受到傷害的音效撥放一次

        if(currentHealth <= 0 && !isDead) // 目前血量歸零 且 沒死時
        {
            Death (); // 死亡函式被觸發
        }
    }


    void Death () // 死亡函式
    {
        isDead = true; // 布林值 isDead = true

        playerShooting.DisableEffects (); // 關閉射擊特效

        anim.SetTrigger ("Die"); // 觸發Animator控制器的 Die Trigger

        playerAudio.clip = deathClip; // 角色撥放的音效改為死亡的音效
        playerAudio.Play ();  // 撥放一次

        playerMovement.enabled = false; // 角色移動的程式 關閉
        playerShooting.enabled = false; // 射擊功能關閉
    }


    public void RestartLevel () // 復活的函式 在其他的程式被觸發
    {
        SceneManager.LoadScene (0); // 回去場景編號0的場景
    }
}
