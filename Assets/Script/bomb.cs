using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; // Tilemap関連のクラスを使用

public class bomb : MonoBehaviour
{
    [SerializeField, Header("爆発エフェクト")]
    public GameObject explosionEffect;

    // 爆発の半径を指定する変数
    [SerializeField, Header("爆発の半径")]
    private float explosionRadius = 5.0f; // ここで大きめの半径を設定

    void OnCollisionEnter2D(Collision2D col)
    {
        // 爆発範囲内の敵とタイルを削除
        Explode();
        
        // 爆発エフェクトを生成して、爆弾自身を削除
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // 爆発範囲内の敵とタイルを削除する処理
    void Explode()
    {
        // 1. 爆発範囲内の敵を削除
        DestroyEnemiesInRadius();

        // 2. 爆発範囲内のタイルを削除
        DestroyTilesInRadius();
    }

    // 爆発範囲内の敵を削除する処理
    void DestroyEnemiesInRadius()
    {
        // Physics2D.OverlapCircleAllを使って、爆発範囲内の全てのCollider2Dを取得
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        // 取得したCollider2Dの中でEnemyタグを持つオブジェクトを削除
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Bear"))
            {
                // デバッグ: 敵が範囲内で検知されたことを確認
                Debug.Log($"爆発範囲内の敵を削除: {hitCollider.gameObject.name}");
                ScoreManager.score_num += 1000;//スコアを10加算0
                Destroy(hitCollider.gameObject);
            }
        }
    }

    // 爆発範囲内のタイルを削除する処理
    void DestroyTilesInRadius()
    {
        // TilemapのBlockタイルの削除処理
        Collider2D[] tileColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hitCollider in tileColliders)
        {
            if (hitCollider.CompareTag("Block"))
            {
                Tilemap tilemap = hitCollider.GetComponent<Tilemap>();
                if (tilemap != null)
                {
                    Vector3 explosionCenter = transform.position;
                    Vector3Int centerTilePos = tilemap.WorldToCell(explosionCenter);

                    for (int x = -Mathf.CeilToInt(explosionRadius); x <= Mathf.CeilToInt(explosionRadius); x++)
                    {
                        for (int y = -Mathf.CeilToInt(explosionRadius); y <= Mathf.CeilToInt(explosionRadius); y++)
                        {
                            Vector3Int tilePos = new Vector3Int(centerTilePos.x + x, centerTilePos.y + y, centerTilePos.z);

                            if (Vector3.Distance(tilemap.CellToWorld(tilePos), explosionCenter) <= explosionRadius)
                            {
                                if (tilemap.GetTile(tilePos) != null)
                                {
                                    tilemap.SetTile(tilePos, null);
                                    ScoreManager.score_num += 1500;
                                    ScoreManager.dic["Block"] += 1500; //追加
                                }
                            }
                        }
                    }

                    // TilemapCollider2D の再有効化（当たり判定の再構築）
                    TilemapCollider2D tileCol = tilemap.GetComponent<TilemapCollider2D>();
                    if (tileCol != null)
                    {
                        tileCol.enabled = false;
                        tileCol.enabled = true;
                    }
                }
            }
        }
    }

    // 爆発の範囲を視覚的に表示する（デバッグ用）
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // 爆発範囲を赤い円で表示
    }
}
