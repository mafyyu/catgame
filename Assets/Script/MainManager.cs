using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using unityroom.Api;

public class MainManager : MonoBehaviour
{
    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject _gameOverUI; // ゲームオーバーの表示をするための変数
    [SerializeField, Header("ゲームクリアUI")]
    private GameObject _gameClearUI; // ゲームクリアの表示をするための変数

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
            _ShowGameOverUI();
        }

        // UIが表示された後、スペースキーが押されたらタイトルに戻る
        if (_bShowUI && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Title"); // タイトルシーンに戻る
        }
    }

    private void _ShowGameOverUI() // ゲームオーバーのUIを表示する処理
    {
        _gameOverUI.SetActive(true); // ゲームオーバーのUIを表示
        _bShowUI = true;
    }
    
    public void _ShowGameClearUI() // ゲームクリアのUIを表示する処理
    {
        _gameClearUI.SetActive(true); // ゲームクリアのUIを表示
        _bShowUI = true;
        UnityroomApiClient.Instance.SendScore(1, ScoreManager.score_num, ScoreboardWriteMode.HighScoreDesc);
    }
    

    public void OnReStart(InputAction.CallbackContext context) // ゲームを再スタートする処理
    {
        if (!_bShowUI || !context.performed) return;
        SceneManager.LoadScene("Title");
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title"); // タイトルシーンに戻る
    }
}
