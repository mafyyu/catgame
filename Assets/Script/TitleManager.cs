using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void _titleStart()
    {
    }



    public void OnSpaceClick(InputAction.CallbackContext context) //スペースキーを押した時の処理
    {
        if (context.performed)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
