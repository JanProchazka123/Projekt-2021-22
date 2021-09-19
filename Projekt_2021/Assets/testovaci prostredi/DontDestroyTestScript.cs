using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyTestScript : MonoBehaviour
{


    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("TestScene1");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("HealthTestScene");
        }
    }
}
