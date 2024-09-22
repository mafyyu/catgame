using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;

    [SerializeField, Header("攻撃力")]
    private int _attackPower; 

    private Rigidbody2D _rigid;
    private Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main; // メインカメラの参照を取得
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsVisibleFromCamera())
        {
            _Move();
        }
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

    public void PlayerDamage(Player player) //ダメージを与える処理
    {
        player.Damage(_attackPower);
    }
}