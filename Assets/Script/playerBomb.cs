using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBomb: MonoBehaviour
{
    [SerializeField, Header("爆弾アイコン")]
    private GameObject _BombIcon; //アイコンを設定する用の変数

    private Player _player; 
    private int _beforeBomb; //playerの爆弾数が変動する前の値を保持する変数

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>(); //<>内に指定したクラスのオブジェクトを探す
        _beforeBomb = _player.GetBomb();  
        _CreateBombIcon(_player.GetBomb());
    }

    // 爆弾アイコンを生成する処理
    private void _CreateBombIcon(int bombCount)
    {
        for (int i = 0; i < bombCount; i++)
        {
            GameObject _playerBombObj = Instantiate(_BombIcon);
            _playerBombObj.transform.SetParent(transform, false); // SetParent を使用し、配置を調整
        }
    }

    // Update is called once per frame
    void Update()
    {
        _ShowBombIcon();
    }

    // アイコンの表示/非表示、および必要に応じてアイコンを追加
    private void _ShowBombIcon()
    {
        // 爆弾の数が変わっていない場合は処理を終了
        if(_beforeBomb == _player.GetBomb()) return;

        // 爆弾の数が増えた場合、新しいアイコンを追加
        if (_beforeBomb < _player.GetBomb())
        {
            int newBombCount = _player.GetBomb() - _beforeBomb;
            _CreateBombIcon(newBombCount);  // 差分のアイコンを追加
        }

        // 全てのアイコンを取得し、現在の爆弾数に応じて表示/非表示を設定
        Image[] icons = transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < _player.GetBomb());
        }

        // 現在の爆弾数を更新
        _beforeBomb = _player.GetBomb();
    }
}