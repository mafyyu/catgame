using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
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

    void OnWillRenderObject()
    {
        if(Camera.current.name != "SceneCamera"  && Camera.current.name != "Preview Camera")
        {
            _cameraManager.scrollSpeed = 0.0f;
            Debug.Log("Stop");
        }
        
        


        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     _cameraManager.scrollSpeed = 3.0f;
        // }
    }
}
