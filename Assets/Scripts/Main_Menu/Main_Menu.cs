using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public void LoadGame()
    {
        //Index of the 'Game' Scene
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        
        EditorApplication.isPlaying = false;

#endif
        Application.Quit();
    }
}
