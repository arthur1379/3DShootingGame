using UnityEngine;
using UnityEngine.UI; // 呼叫UI函式庫
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score; // int score變數 設為靜態全域變數


    Text text; // 文字UI


    void Awake ()
    {
        text = GetComponent <Text> (); // 呼叫文字UI元件
        score = 0; // 初始分數設為0
    }


    void Update ()
    {
        text.text = "Score: " + score; // 不斷更新文字的值(字串) -> 字串 = 字串 + 整數;
    }
}
