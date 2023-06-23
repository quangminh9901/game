using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunction : MonoBehaviour
{
    
    public void Play()
    {
        SceneManager.LoadScene(1); 
    }

    public void Replay()
    {
        SceneManager.LoadScene(1); 
    }

    
    public void Return()
    {
        SceneManager.LoadScene(0);    
    }
    
    public void Exit()
    {
        Application.Quit();    
    }
}
