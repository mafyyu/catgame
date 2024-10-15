using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;

    private Rigidbody2D _rigid;
    private Camera _mainCamera;

    public float nowPosi;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main; // メインカメラの参照を取得
        nowPosi = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsVisibleFromCamera())
        {
            _Move();
        }
        transform.position = new Vector3(transform.position.x, nowPosi + Mathf.PingPong(Time.time, 1.0f), transform.position.z);
    }

    private void _Move()
    {
        _rigid.velocity = new Vector2(Vector2.left.x * _moveSpeed, _rigid.velocity.y);
        //敵が左方向へ動くようにする
    }

    private bool _IsVisibleFromCamera()
    {
        // 敵の位置をスクリーン座標に変換
        Vector3 screenPoint = _mainCamera.WorldToViewportPoint(transform.position);
        // スクリーン座標がカメラの視界内にあるかどうかをチェック
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}