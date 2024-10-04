using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flog : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float _moveSpeed; 

    [SerializeField, Header("ジャンプ速度")]
    private float _jumpSpeed;

    private Rigidbody2D _rigid;
    private Camera _mainCamera;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main; // メインカメラの参照を取得
        _anim = GetComponent<Animator>();
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Block")
        {
                _rigid.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                _anim.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Block")
        {
            _anim.SetBool("Jump", true);
        }
    }
}