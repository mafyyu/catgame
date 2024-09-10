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

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
    }

    private void _Move()
    {
        _rigid.velocity = new Vector2(Vector2.left.x*_moveSpeed , _rigid.velocity.y);
        //敵が左方向へ動くようにする
    }

    public void PlayerDamage(Player player) //ダメージを与える処理
    {
        player.Damage(_attackPower);
    }
}
