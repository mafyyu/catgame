using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField, Header("移動速度")] //SerializeFieldを使うことで、Unityで変数を編集できる
    private float _moveSpeed; //float型の変数_moveSpeedを宣言
    [SerializeField, Header("ジャンプ速度")]
    private float _jumpSpeed; //floatは小数点の値を持つ変数
    [SerializeField, Header("体力")]
    private int _hp; //intは整数の値を持つ変数
    [SerializeField, Header("無敵時間")]
    private float _damageTime; //floatは小数点の値を持つ変数
    [SerializeField, Header("点滅時間")]
    private float _flashTime; //floatは小数点の値を持つ変数

    private Vector2 _inputDirection; //(x,y)の値が入るのがvector2型
    private Rigidbody2D _rigid; 
    private SpriteRenderer _spriteRenderer; //SpriteRendererの変数を宣言
    private Animator _anim; //Animatorの変数を宣言
    private bool _bJump; //何回でもジャンプできるのを制御するための変数

    // 追加した変数
    private Camera _mainCamera; // メインカメラを格納する変数
    private float _leftEdge; // 画面左端の位置を格納する変数

    private CameraManager _cameraManager; // CameraManagerのインスタンスを格納する変数

    [SerializeField, Header("爆弾の個数")]

    public int count;

    // Start is called before the first frame update
    void Start() //ゲームスタート時に１度だけ実行される
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>(); //Animatorを取得
        _spriteRenderer = GetComponent<SpriteRenderer>(); //SpriteRendererを取得
        _bJump = false;

        _cameraManager = FindObjectOfType<CameraManager>();

        // 追加した初期化処理
        _mainCamera = Camera.main; // メインカメラを取得
        if (_mainCamera != null)
        {
            _leftEdge = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x; // 画面左端の位置を計算
        }
    }

    // Update is called once per frame
    void Update() //ゲームが動いている間ずっと実行される
    {
        _Move();
        Debug.Log(_hp); //Unityのコンソールに_hpの値を表示

        // 常に左端の位置を再計算する
        _leftEdge = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        if (transform.position.x < _leftEdge)
        {
            _cameraManager.scrollSpeed = 0.0f; 
            Destroy(gameObject); //playerを消す
        }

        float currentScrollSpeed = _cameraManager.scrollSpeed;


        // プレイヤーがカメラの下にいるかをチェック
        if (transform.position.y < _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, 0)).y - 1.0f) // 画面下端を基準に調整
        {
            _cameraManager.scrollSpeed = 0.0f; 
            Destroy(gameObject);
        }
        
        Throw();
    }


    private void _Move() //privateは宣言したクラス内でしか使用しないときに先頭につける
    {
        if (_bJump) return; //_bJumpがtrueの時はreturnより後の処理は行わない
        _rigid.velocity = new Vector2(_inputDirection.x * _moveSpeed, _rigid.velocity.y); //playerの速度 この状態だと上下には動くことはない
        _anim.SetBool("walk", _inputDirection.x != 0.0f); //playerが左右に動いている時にwalkをtrueにする
    }

    private void OnCollisionEnter2D(Collision2D collision) //playerが何かにぶつかった時に実行される
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Block") //playerが床にぶつかった時
        {
            _bJump = false; //ジャンプしていない状態にする
        }

        if (collision.gameObject.tag == "Enemy") //playerが敵にぶつかった時
        {
            _HitEnemy(collision.gameObject); //敵にぶつかった時の処理を実行
        }
        else if (collision.gameObject.tag == "Goal") //playerがゴールにぶつかった時
        {
            FindObjectOfType<MainManager>()._ShowGameClearUI(); //MainManagerの_ShowGameClearUIメソッドを実行
            enabled = false; //playerのスクリプトを無効にする
            GetComponent<PlayerInput>().enabled = false; //PlayerInputのスクリプトを無効にする
            _cameraManager.scrollSpeed = 0.0f; 
            SceneManager.LoadScene("Title"); // タイトルシーンに戻る
        }
    }

    private void _HitEnemy(GameObject enemy) //敵にぶつかった時の処理
    {
        float halfScaleY = transform.lossyScale.y / 2.0f;
        float enemyHalfScaleY = enemy.transform.lossyScale.y / 2.0f;
        if (transform.position.y - (halfScaleY - 0.1f) >= enemy.transform.position.y + (enemyHalfScaleY - 0.1f))
        {
            Destroy(enemy); //敵を消す
            _rigid.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse); //敵を踏んだ時にジャンプする
        }
        else
        {
            enemy.GetComponent<Enemy>().PlayerDamage(this); //敵の頭上以外にplayerがぶつかった時ダメージを受ける
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage"); //playerが敵にぶつかった時のレイヤーをPlayerDamageに変更
            StartCoroutine(_Damage()); //コルーチンを実行
        }
    }

    public void attackEnemy(GameObject enemy)
    {

    }

    IEnumerator _Damage() //IEnumeratorはコルーチンを使うときに使う
    {
        Color color = _spriteRenderer.color; //spriteRendererの色を取得
        for (int i = 0; i < _damageTime; i++)
        {
            yield return new WaitForSeconds(_flashTime);
            _spriteRenderer.color = new Color(color.r, color.g, color.b, 0.0f);

            yield return new WaitForSeconds(_flashTime);
            _spriteRenderer.color = new Color(color.r, color.g, color.b, 1.0f);
        }
        _spriteRenderer.color = color; //spriteRendererの色を元に戻す
        gameObject.layer = LayerMask.NameToLayer("Player"); //playerのレイヤーをPlayerに変更
    }

    private void _Dead()
    {
        if (_hp <= 0) //hpが0以下になった時
        {
            _cameraManager.scrollSpeed = 0.0f; 
            Destroy(gameObject); //playerを消す
        }
    }


    public void _OnMove(InputAction.CallbackContext context) //メソッドの中に引数を入れることで、引数の値を使うことができる
    {
        _inputDirection = context.ReadValue<Vector2>(); //playerの入力方向を_inputDirectionに代入
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || _bJump) return; //_ONJumpが押されていないときか_bJumpがtrueの時はreturnより後の処理は行わない

        _rigid.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse); //オブジェクトの加速度を与える ()の中に書いた文を加速させる
        _bJump = true; //ジャンプしたらtrueになる
    }

    public void Damage(int damage) //引数damageを受け取る
    {
        _hp = Mathf.Max(_hp - damage, 0); //hpからdamageを引いた値と0の大きい方を_hpに代入
        _Dead(); //_Deadメソッドを実行
    }

    public int GetHP()
    {
        return _hp; //_hpの値を返す
    }

    public GameObject bombPrefab; // プレハブを保持する公開フィールド


    private void Throw()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (count > 0)
            {
            // プレイヤーの位置に投げるオブジェクトを生成
                GameObject instantiatedBomb = Instantiate(bombPrefab);
                instantiatedBomb.transform.position = transform.position;
                Rigidbody2D bombRigid = instantiatedBomb.GetComponent<Rigidbody2D>();
                Vector2 force = new Vector2(5.0f, 5.0f);
                bombRigid.AddForce(force, ForceMode2D.Impulse);
                count--;
            }
       }
    }
    public int GetBomb()
    {
        return count;
    }
}
