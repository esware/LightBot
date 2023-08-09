using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLoader : MonoBehaviour
{
    public void LoadNextLevel()
    {
        var level = PlayerPrefs.GetInt("PlayerLevel");
        PlayerPrefs.SetInt("PlayerLevel",level+1);
        
        SceneManager.LoadScene(0);
    }
}
