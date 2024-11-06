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
        _titleStart();
    }




    public void _titleStart() 
    {
        // if (Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     SceneManager.LoadScene("Easy");
        // }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Normal");
        }

        // else if (Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //     SceneManager.LoadScene("Hard");
        // }
    }
}
