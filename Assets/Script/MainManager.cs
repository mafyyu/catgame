using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class MainManager : MonoBehaviour
{

    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject _gameOverUI; //ゲームオーバーの表示をするための変数
    [SerializeField, Header("ゲームクリアUI")]
    private GameObject _gameClearUI; //ゲームクリアの表示をするための変数

    [SerializeField, Header("Title画面に行くまでの時間")]
    private float _delay;

    private GameObject _player; //playerのオブジェクトを保持する変数
    private bool _bShowUI; //UIを表示するかどうかのフラグ


    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>().gameObject; 
        _bShowUI = false;
    }


    // Update is called once per frame
    void Update()
    {
        _ShowGameOverUI();
    }

    private void _ShowGameOverUI() //ゲームオーバーのUIを表示する処理
    {
        if(_player !=null) return; //_playerに何かしらのオブジェクトが入っている場合は処理を終了する
        {
            _gameOverUI.SetActive(true); //ゲームオーバーのUIを表示
            _bShowUI = true;
            StartCoroutine(ReturnToTitleAfterDelay());
        }
    }

    public void _ShowGameClearUI() //ゲームクリアのUIを表示する処理
    {
        _gameClearUI.SetActive(true); //ゲームクリアのUIを表示
        _bShowUI = true;
    }

    public void OnReStart(InputAction.CallbackContext context) //ゲームを再スタートする処理
    {
        if(!_bShowUI || !context.performed) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //現在のシーンを再読み込み
    }
    
    private IEnumerator ReturnToTitleAfterDelay()
    {
        yield return new WaitForSeconds(_delay); // 指定した秒数待機
        SceneManager.LoadScene("Title"); // タイトルシーンに戻る
    }
}
