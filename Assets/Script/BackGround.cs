using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField, Header("視差効果"),Range(0,1)]
    private float _parallaxEffect; //視差効果の値を設定する変数

    private GameObject _camera; //カメラのオブジェクトを保持する変数
    private float _length; //背景の長さを保持する変数
    private float _StartPosX; //背景の初期位置を保持する変数

    // Start is called before the first frame update
    void Start()
    {
        _StartPosX = transform.position.x; //背景の初期位置を取得
        _length = GetComponent<SpriteRenderer>().bounds.size.x; //背景の長さを取得
        _camera = Camera.main.gameObject; //メインカメラのオブジェクトを取得
    }

    // Update is called once per frame
    void Update()  //フレームごとに処理を行う
    {
        
    }

    private void FixedUpdate() //一定時間ごとに処理を行う
    {
        _Parallax();
    }

    private void _Parallax()
    {
        float temp = _camera.transform.position.x * (1 - _parallaxEffect); //カメラの移動量を取得
        float dist = _camera.transform.position.x * _parallaxEffect; //背景の移動量を取得
        transform.position = new Vector3(_StartPosX + dist, transform.position.y, transform.position.z); //背景の位置を更新

        if(temp > _StartPosX + _length)
        {
            _StartPosX += _length; //背景の位置を更新
        }
        else if(temp < _StartPosX - _length)
        {
            _StartPosX -= _length; //背景の位置を更新
        }
    
    }
}
