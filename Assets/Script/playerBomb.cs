using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBomb: MonoBehaviour
{
    [SerializeField, Header("爆弾アイコン")]
    private GameObject _BombIcon; //アイコンを設定する用の変数

    private Player _player; 
    private int _beforeBomb; //playerのHPが変動する前の値を保持する変数


    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>(); //<>内に指定したクラスのオブジェクトを探す
        _beforeBomb = _player.GetBomb();  
        _CreateBombIcon();
    }
    
    private void _CreateBombIcon()
    {
        for (int i = 0; i < _player.GetBomb(); i++)
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

    private void _ShowBombIcon()
    {
        if(_beforeBomb == _player.GetBomb()) return;

        Image[] icons = transform.GetComponentsInChildren<Image>();
        for( int i = 0; i< icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < _player.GetBomb());
        }
        _beforeBomb = _player.GetBomb();
    }
}