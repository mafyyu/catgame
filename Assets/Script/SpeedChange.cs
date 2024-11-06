using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChange : MonoBehaviour
{
    private CameraManager _cameraManager;
    [SerializeField, Header("カメラのスピード変更")]
    private float cameraChangeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _cameraManager = FindObjectOfType<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       
         if (col.CompareTag("Player"))
        {
           _cameraManager.scrollSpeed = 3.0f;
           Debug.Log("SpeedChange");
        } 
    }
}
