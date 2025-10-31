using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Can use to load different levels

public class SceneLoader : MonoBehaviour
{

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void Load_MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel_01()
    {
        SceneManager.LoadScene(1);
    }
}
