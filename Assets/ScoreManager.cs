using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProを使用するために追加

public class ScoreManager : MonoBehaviour {

    public GameObject score_object = null; // TextまたはTextMeshProオブジェクト
    public static int score_num = 0; // スコア変数

    // 初期化
    void Start () {
        if (score_object == null) {
            Debug.LogError("score_object is not assigned! Please assign it in the Inspector.");
        }
    }

    // 更新
    void Update () {
        if (score_object != null) {
            // TextMeshProUGUIコンポーネントを取得
            TextMeshProUGUI score_text = score_object.GetComponent<TextMeshProUGUI>();

            // TextMeshProUGUIがnullでないことを確認
            if (score_text != null) {
                // テキストの表示を入れ替える
                score_text.text = "Score: " + score_num;
            } else {
                Debug.LogError("TextMeshProUGUI component not found on the assigned GameObject.");
            }
        }
    }
}
