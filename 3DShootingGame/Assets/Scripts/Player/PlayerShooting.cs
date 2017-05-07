using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20; // 子彈的傷害
    public float timeBetweenBullets = 0.15f; // 發射子彈的時間間隔
    public float range = 100f; // 攻擊距離


    float timer; // 計算時間
    Ray shootRay = new Ray(); // 射線
    RaycastHit shootHit; // 射到的物體
    int shootableMask; // 可射擊的圖層
    ParticleSystem gunParticles; // 發射子彈粒子效果
    LineRenderer gunLine; // 子彈軌跡
    AudioSource gunAudio; // 子彈射擊音效
    Light gunLight; // 光線
    float effectsDisplayTime = 0.2f; // 效果呈現的時間


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable"); // 敵人的Shootable圖層

        // 指定元件進來
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime; // 時間計算

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0) // 按下滑鼠左鍵 + 時間計算 > 時間射擊間隔 + 時間規模不等於0
        {
            Shoot (); // 射擊函式
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime) // 時間計算 >= 時間射擊間隔 * 效果呈現的時間
        {
            DisableEffects (); // 關閉效果函式
        }
    }


    public void DisableEffects () // 關閉效果函式
    {
        gunLine.enabled = false; // 子彈軌跡關閉
        gunLight.enabled = false; // 子彈光線關閉
    }


    void Shoot () // 射擊函式
    {
        timer = 0f; // 時間歸零

        gunAudio.Play (); // 音效撥放

        gunLight.enabled = true; // 開啟光線

        gunParticles.Stop (); // 粒子效果關閉
        gunParticles.Play (); // 粒子效果開啟

        gunLine.enabled = true; // 子彈軌跡開啟
        gunLine.SetPosition (0, transform.position); // 設定子彈軌跡位置 (第一個點)

        shootRay.origin = transform.position; // 設定射線起始位置
        shootRay.direction = transform.forward; // 設定射線方向 (Z)

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask)) // 設定射線 (只有射到Shootable圖層才有作用)
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> (); // 去抓被射到的物件上的EnemyHealth
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point); // 若抓到就呼叫物件程式上的TakeDamage( 傷害, 射線打到的位置 )
            }
            gunLine.SetPosition (1, shootHit.point); // 設定子彈軌跡的點 (第二個點)
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range); //如果沒有射到物件(敵人) 軌跡第二點的距離為 物件位置+射線方向*射擊範圍
        }
    }
}
