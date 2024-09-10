using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChecker : MonoBehaviour
{
    private enum Mode // enum型(列挙型)でModeを宣言 名前がついた定数の集まり
    {
        None,
        Render,
        RenderOut,
    }

    private Mode _mode;

    // Start is called before the first frame update
    void Start()
    {
        _mode = Mode.None; //ModeをNoneに設定
    }

    // Update is called once per frame
    void Update()
    {
        _Dead();
    }

    private void OnWillRenderObject() //~ Renderとついているものが写っている間によばれる
    {
        if(Camera.current.name == "Main Camera") //カメラのタグがMainCameraの場合
        {
            _mode = Mode.Render; //ModeをRenderに設定
        }
    }

    private void _Dead()
    {
        Vector3 cameraMinPos = Camera.main.ScreenToWorldPoint(Vector3.zero);

        if(_mode == Mode.RenderOut && transform.position.x < cameraMinPos.x) //ModeがRenderOutの場合
        {
            Destroy(gameObject); //オブジェクトを削除
        }
        if(_mode == Mode.Render) //ModeがRenderの場合
        {
            _mode = Mode.RenderOut; //ModeをRenderOutに設定
        }
    }

    
}
