using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProを使用するために追加

public class ScoreManager : MonoBehaviour {

    public GameObject score_object = null; // TextまたはTextMeshProオブジェクト
    public GameObject score_over = null; // TextまたはTextMeshProオブジェクト
    public GameObject score_clear = null; // TextまたはTextMeshProオブジェクト



    public static int score_num = 0; // スコア変数


    public static Dictionary<string,int> dic = new Dictionary<string,int>()
    {
        {"Block",0},
        {"Enemy",0},

    };
    public void LogDictionaryContents() // デバッグ用
    {
        // BlockとEnemyのスコアを取得
        int blockScore = dic["Block"];
        int enemyScore = dic["Enemy"];

        // 個別にDebug.Logで出力
        Debug.Log($"Block Score: {blockScore}");
        Debug.Log($"Enemy Score: {enemyScore}");
    }

    // 初期化
    void Start () {
        if (score_object == null) {
            Debug.LogError("score_object is not assigned! Please assign it in the Inspector.");
        }
        score_num = 0;
        dic["Block"] = 0;
        dic["Enemy"] = 0;
    }

    // 更新
    void Update () {
        LogDictionaryContents();
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
            TextMeshProUGUI score_over_text = score_over.GetComponent<TextMeshProUGUI>();
            score_over_text.text = "Score: " + score_num;

            TextMeshProUGUI score_clear_text = score_clear.GetComponent<TextMeshProUGUI>();
            score_clear_text.text = "Score: " + score_num;
        }
    }


}