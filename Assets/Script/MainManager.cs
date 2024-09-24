using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainManager : MonoBehaviour
{
    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject _gameOverUI; // ゲームオーバーの表示をするための変数
    [SerializeField, Header("ゲームクリアUI")]
    private GameObject _gameClearUI; // ゲームクリアの表示をするための変数

    [SerializeField, Header("Title画面に行くまでの時間")]
    public float _delay=3.0f; // タイトル画面に行くまでの時間を設定する変数

    private GameObject _player; // playerのオブジェクトを保持する変数
    private bool _bShowUI; // UIを表示するかどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>().gameObject; 
        _bShowUI = false;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーが存在しない場合にゲームオーバー処理を開始
        if (_player == null && !_bShowUI)
        {
            StartCoroutine(_ShowGameOverUI());
        }
    }

    private IEnumerator _ShowGameOverUI() // ゲームオーバーのUIを表示する処理
    {
        _gameOverUI.SetActive(true); // ゲームオーバーのUIを表示
        _bShowUI = true;

        yield return StartCoroutine(ReturnToTitleAfterDelay()); // タイトルシーンに戻る
    }
    
    public IEnumerator _ShowGameClearUI() // ゲームクリアのUIを表示する処理
    {
        _gameClearUI.SetActive(true); // ゲームクリアのUIを表示
        _bShowUI = true;
    
        yield return StartCoroutine(ReturnToTitleAfterDelay()); // タイトルシーンに戻る
    }

    public void OnReStart(InputAction.CallbackContext context) // ゲームを再スタートする処理
    {
        if (!_bShowUI || !context.performed) return;
        SceneManager.LoadScene("Title");
    }
    
    public IEnumerator ReturnToTitleAfterDelay()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            yield return new WaitForSeconds(_delay); // 指定した秒数待機
            SceneManager.LoadScene("Title"); // タイトルシーンに戻る
        }
        // Debug.Log("Waiting for " + _delay + " seconds.");
        // yield return new WaitForSeconds(_delay); // 指定した秒数待機
        // SceneManager.LoadScene("Title"); // タイトルシーンに戻る
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title"); // タイトルシーンに戻る
    }
    
}
