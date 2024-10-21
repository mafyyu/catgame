using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//playerの攻撃に関する処理
public class PlayerAttack : MonoBehaviour
{
    public BoxCollider2D attackCollider; // AttackAreaのBoxColliderをここにドラッグ&ドロップで設定
    public float attackCooldown = 0.5f; // 攻撃のクールダウン時間
    public Vector2 attackOffset = new Vector2(1, 0); // プレイヤーから見た攻撃範囲のオフセット
    private float lastAttackTime;

    private void Start()
    {
        // 最初は攻撃範囲を無効に
        attackCollider.enabled = false;

        // コライダーの位置をプレイヤーの目の前に設定
        attackCollider.transform.localPosition = attackOffset;
    }

    private void Update()
    {
        // Fキーが押された時に攻撃を行う
        if (Input.GetKeyDown(KeyCode.F) && Time.time > lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time; // 最後の攻撃時間を更新
        }
    }

    private void Attack()
    {
        // 攻撃範囲のコライダーを有効にし、正しい位置に配置
        attackCollider.transform.localPosition = attackOffset;
        attackCollider.enabled = true; // 攻撃範囲を有効に
        Invoke("DisableAttackCollider", 0.1f); // 0.1秒後に攻撃範囲を無効にする
    }

    private void DisableAttackCollider()
    {
        attackCollider.enabled = false; // 攻撃範囲を無効に
    }

    // 攻撃範囲に敵が入ったときに敵を削除する
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // 敵を削除する
            ScoreManager.score_num += 10;
            ScoreManager.dic["Enemy"]+= 10; //追加
            Debug.Log("敵を削除しました"); // デバッグメッセージ
        }
    }
}
