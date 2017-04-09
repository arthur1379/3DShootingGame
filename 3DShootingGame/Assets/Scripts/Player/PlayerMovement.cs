using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;            // 角色移動的速度

    Vector3 movement;                   // 角色移動的方向
    Animator anim;                      // 呼叫Animator元件
    Rigidbody playerRigidbody;          // 呼叫Rigidbody元件
    int floorMask;                      // Floor Layer 圖層的編號
    float camRayLength = 100f;          // 射線的長度

    void Awake()
    {
        // 指定Floor Layer的編號給floorMask
        floorMask = LayerMask.GetMask("Floor");

        // 找到元件
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        // 讀取鍵盤硬體控制按鈕
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 移動函式
        Move(h, v);

        // 旋轉函式
        Turning();

        // 動畫函式
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        // 設定移動的方向
        movement.Set(h, 0f, v);

        // 設定移動的速度 方向 * 速度 * 每秒
        movement = movement.normalized * speed * Time.deltaTime;

        // 將移動方向 * 速度 * 每秒 指定到Rigidbody元件上 讓角色可以移動
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        // 從相機打出一個射線
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 設定被射線打到的碰撞體
        RaycastHit floorHit;

        // 當射線碰到 (射線 設限碰到的東西 射線長度 射線可以碰撞的圖層)
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // 計算 角色 到 射線的 Hit Point的角度
            Vector3 playerToMouse = floorHit.point - transform.position;

            // 地面高度 y 為 0  確保 playerToMouse.y = 0 
            playerToMouse.y = 0f;

            // 設定新的旋轉值
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // 將新的旋轉值指定給主角
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        // 只要 h 或 v 其中有一個不為0就回傳 true
        bool walking = h != 0f || v != 0f;

        // 觸發 IsWalking
        anim.SetBool("IsWalking", walking);
    }
}