using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // 跟隨的目標
    public float smoothing = 5f;        // 相機緩衝的程度

    Vector3 offset;                     // 相機和角色的距離差

    void Start()
    {
        // 計算相機和角色的距離差
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        // 設定相機的位置座標
        Vector3 targetCamPos = target.position + offset;

        //  設定相機位置 利用Vector3.Lerp漸漸移動過去
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
