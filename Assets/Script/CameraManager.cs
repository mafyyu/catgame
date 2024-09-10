using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField, Header("振動する時間")]
    private float _shakeTime; //振動する時間
    [SerializeField, Header("振動する大きさ")]
    private float _shakeMagnitude; //振動する大きさ

    [SerializeField, Header("スクロール速度")]
    public float scrollSpeed; // カメラのスクロール速度

    private Player _player; //Playerクラスの変数を宣言
    private Vector3 _initPos; //カメラの初期位置を保持する変数
    private float _shakeCount;
    private int _currentPlayerHP; //playerのHPの値を保持する変数


    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>(); //Playerクラスのオブジェクトを探す
        _currentPlayerHP = _player.GetHP(); //playerのHPの値を取得
        _initPos = transform.position; //カメラの初期位置を取得
    }

    // Update is called once per frame
    void Update()
    {
        _ShakeCheck();
        // _FollowPlayer();
        CameraScroll();
    }

    private void _ShakeCheck()
    {
        if (_currentPlayerHP != _player.GetHP()) 
        {
            _currentPlayerHP = _player.GetHP(); //playerのHPの値を更新
            _shakeCount = 0.0f;
            StartCoroutine(_Shake());
        }
    }

    IEnumerator _Shake()
    {
        Vector3 initPos = transform.position; //カメラの初期位置を取得

        while (_shakeCount < _shakeTime)
        {
            float x = initPos.x + Random.Range(-_shakeMagnitude, _shakeMagnitude); //x軸の振動値をランダムに取得
            float y = initPos.y + Random.Range(-_shakeMagnitude, _shakeMagnitude);
            transform.position = new Vector3(x, y, initPos.z); //カメラの位置を振動させる

            _shakeCount += Time.deltaTime; //振動時間を更新

            yield return null;
        }
        transform.position = initPos; //カメラの位置を初期位置に戻す
    }

    // private void _FollowPlayer()
    // {
    //     if (_player == null) return; //playerがnullの時は処理を行わない
    //     float x = _player.transform.position.x; //playerのx座標を取得
    //     x =Mathf.Clamp(x, _initPos.x, Mathf.Infinity); //x座標の移動範囲を制限
    //     transform.position = new Vector3(x,transform.position.y, transform.position.z); //カメラの位置を更新
    // }

    public void CameraScroll()
    {
        // カメラをスクロールさせる
        transform.position += new Vector3(scrollSpeed * Time.deltaTime, 0, 0);

        // デバッグ用にカメラの位置を表示
        Debug.Log("Camera Position: " + transform.position + ", Scroll Speed: " + scrollSpeed);
    }

    

}
