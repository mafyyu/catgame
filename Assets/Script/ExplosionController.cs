using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionControler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyExplosion), 0.4f);
    }

    void DestroyExplosion()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
