
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dev.Scripts.UI
{
    public class MainMenuClickHandler : MonoBehaviour
    {
        [Header("Elements")]
        public int levelIndex;

        public GameObject lockedImg;
        public Button button;
        
        public void SetData(int level)
        {
            transform.GetComponentInChildren<Text>().text = level.ToString();
            if (PlayerPrefs.GetInt("PlayerLevel")+1<level)
            {
                button.interactable = false;
                lockedImg.SetActive(true);
            }
            else
            {
                button.interactable = true;
                lockedImg.SetActive(false);
            }
        }

        #region Methods
        public void OnLevelButton ()
        {
            PlayerPrefs.SetInt("CurrentLevel",levelIndex-1);
            SceneManager.LoadScene(1);
        }
        
        
        #endregion
    }
}