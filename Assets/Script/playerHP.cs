using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHP: MonoBehaviour
{
    [SerializeField, Header("HPアイコン")]
    private GameObject _playerIcon; //アイコンを設定する用の変数

    private Player _player; 
    private int _beforeHP; //playerのHPが変動する前の値を保持する変数


    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>(); //<>内に指定したクラスのオブジェクトを探す
        _beforeHP = _player.GetHP();  
        _CreateHPIcon();
    }
    
    private void _CreateHPIcon()
    {
        for (int i = 0; i < _player.GetHP(); i++)
        {
            GameObject _playerHPObj = Instantiate(_playerIcon);
            _playerHPObj.transform.SetParent(transform, false); // SetParent を使用し、配置を調整
        }
    }

    // Update is called once per frame
    void Update()
    {
        _ShowHPIcon();
    }

    private void _ShowHPIcon()
    {
        if(_beforeHP == _player.GetHP()) return;

        Image[] icons = transform.GetComponentsInChildren<Image>();
        for( int i = 0; i< icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < _player.GetHP());
        }
        _beforeHP = _player.GetHP();
    }
}