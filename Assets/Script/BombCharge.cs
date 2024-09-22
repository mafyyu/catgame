using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCharge : MonoBehaviour
{  
    [SerializeField, Header("爆弾が増える数")]
    private int charge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().count+= charge;
            Destroy(gameObject);
        }
    }
}
