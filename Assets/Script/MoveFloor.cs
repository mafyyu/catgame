using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveFloor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D ot)
    {
        // 衝突したオブジェクトが "MoveFloor" タグを持っているか確認
        if (ot.gameObject.CompareTag("MoveFloor"))
        {
            Debug.Log("hit");
            
            // 衝突点を取得
            Vector3 hitPos = Vector3.zero;
            foreach (ContactPoint2D point in ot.contacts)
            {
                hitPos = point.point;
            }

            // Tilemapコンポーネントの取得
            Tilemap tilemap = ot.gameObject.GetComponent<Tilemap>();
            if (tilemap == null) return;  // null チェック

            // タイルの全位置を取得し、タイルが存在する座標をリストに追加
            BoundsInt.PositionEnumerator positions = tilemap.cellBounds.allPositionsWithin;
            var allPositions = new List<Vector3Int>();
            foreach (var pos in positions)
            {
                if (tilemap.GetTile(pos) != null)
                {
                    allPositions.Add(pos);
                }
            }

            // 最も近いタイルの位置を探す
            int minPositionNum = 0;
            for (int i = 1; i < allPositions.Count; i++)
            {
                if ((hitPos - tilemap.CellToWorld(allPositions[i])).magnitude <
                    (hitPos - tilemap.CellToWorld(allPositions[minPositionNum])).magnitude)
                {
                    minPositionNum = i;
                }
            }

            // 最も近いタイルの位置を取得
            Vector3Int finalPosition = allPositions[minPositionNum];

            // 最も近いタイルを取得
            TileBase tile = tilemap.GetTile(finalPosition);
            if (tile != null)
            {
                // タイルを削除
                tilemap.SetTile(finalPosition, null);

                // TilemapCollider2D の更新
                TilemapCollider2D tileCol = ot.gameObject.GetComponent<TilemapCollider2D>();
                if (tileCol != null)
                {
                    tileCol.enabled = false;  // コライダーを一度無効にする
                    tileCol.enabled = true;   // 再度有効にする
                }
            }
        }
    }
}
