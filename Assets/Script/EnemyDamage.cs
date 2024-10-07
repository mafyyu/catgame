using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField, Header("攻撃力")]
    private int _attackPower; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDamage(Player player) //ダメージを与える処理
    {
        player.Damage(_attackPower);
    }
}
