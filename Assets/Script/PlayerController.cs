using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    public float jumpForce = 10f; // ジャンプ力
    private bool isGrounded; // 接地判定
    private Rigidbody2D rb; // Rigidbody2Dの参照

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    // 移動処理
    void Move()
    {
        // インプットのスムージングを除去
        float moveInput = Input.GetAxisRaw("Horizontal"); 
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // 水平方向の速度を設定
    }

    // ジャンプ処理
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // ジャンプ力を設定
        }
    }

    // 接地判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }
}
