using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; // Tilemap関連のクラスを使用

public class bomb : MonoBehaviour
{
    [SerializeField, Header("爆発エフェクト")]
    public GameObject explosionEffect;

    void OnCollisionEnter2D(Collision2D col)
    {
        // 衝突対象が Enemy か Block タグを持つか確認
        if (col.gameObject.tag == "Enemy")
        {
            // 敵を削除
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Block")
        {
            // Block タグの Tilemap を破壊する処理
            HandleTilemapCollision(col);
        }

        // 爆発エフェクトを生成して、爆弾自身を削除
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Tilemap の Block タイルを破壊する処理
    void HandleTilemapCollision(Collision2D ot)
    {
        Tilemap tilemap = ot.gameObject.GetComponent<Tilemap>();
        if (tilemap == null) return; // Tilemapがない場合は処理終了

        Vector3 hitPos = Vector3.zero;
        foreach (ContactPoint2D point in ot.contacts)
        {
            hitPos = point.point; // 衝突点を取得
        }

        var position = tilemap.cellBounds.allPositionsWithin;
        var allPosition = new List<Vector3Int>();
        int minPositionNum = 0;

        foreach (var variable in position)
        {
            if (tilemap.GetTile(variable) != null)
            {
                allPosition.Add(variable);
            }
        }

        if (allPosition.Count == 0) return;

        // 衝突点に最も近いタイルを探す
        for (int i = 1; i < allPosition.Count; i++)
        {
            if ((hitPos - allPosition[i]).magnitude <
                (hitPos - allPosition[minPositionNum]).magnitude)
            {
                minPositionNum = i;
            }
        }

        // 最終的に近いタイルの位置
        Vector3Int finalPosition = allPosition[minPositionNum];

        // タイルが存在する場合は削除
        if (tilemap.GetTile(finalPosition) != null)
        {
            tilemap.SetTile(finalPosition, null); // タイルを削除

            // TilemapCollider2D の再有効化（当たり判定の再構築）
            TilemapCollider2D tileCol = ot.gameObject.GetComponent<TilemapCollider2D>();
            if (tileCol != null)
            {
                tileCol.enabled = false;
                tileCol.enabled = true;
            }
        }
    }
}
