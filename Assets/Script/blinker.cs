using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshProを使用するための名前空間

public class Blinker : MonoBehaviour
{
    public string targetTag = "BlinkingText";  // 点滅させる対象のタグ
    public float speed = 1.0f;

    private TextMeshProUGUI text;  // TextMeshProUGUIコンポーネント用の変数
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        // タグで指定されたオブジェクトを取得
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

        if (targetObject != null)
        {
            text = targetObject.GetComponent<TextMeshProUGUI>();  // TextMeshProUGUIコンポーネントを取得

            if (text == null)
            {
                Debug.LogError("TextMeshProUGUIコンポーネントが見つかりません！");
            }
        }
        else
        {
            Debug.LogError("指定されたタグを持つオブジェクトが見つかりません！");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // textがnullでない場合のみ処理を行う
        if (text != null)
        {
            text.color = GetAlphaColor(text.color);
        }
    }

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Abs(Mathf.Sin(time));  // アルファ値を絶対値にして、0から1の間で変化させる
        return color;
    }
}
