using System;
using Dev.Scripts;
using Dev.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIHandler : MonoBehaviour
    {
        #region Variables
            [SerializeField]
            public RectTransform targetProc;
            
            [SerializeField]
            private GameObject buttonRun;

            [SerializeField]
            private GameObject buttonReset;

            [SerializeField]
            private GameObject panelFinish;

            [SerializeField] private GameObject procPanel;
            [SerializeField] private Text levelCounterText;

            public static UIHandler Instance;
            #endregion

        #region Methods
            private void Awake ()
            {
                if (Instance != null)
                    Destroy(this.gameObject);
                Instance = this;
                
                Init();
                SignUpEvents();
                
                
            }

            private void Init()
            {
                procPanel.SetActive(true);
                panelFinish.SetActive(false);
                levelCounterText.transform.parent.gameObject.SetActive(true);
                var level = PlayerPrefs.GetInt("CurrentLevel") + 1;
                levelCounterText.text = "LEVEL " + level;
            }

            private void SignUpEvents()
            {
                GameEvents.OnLevelCompleted += ShowFinish;
            }

            private void OnDisable()
            {
                GameEvents.OnLevelCompleted -= ShowFinish;
            }

            internal void AddOperation (DraggableObject draggableObject)
            {
                GameManager.Instance.AddOperation(draggableObject.Operation);
            }

            internal void RemoveOperation (DraggableObject draggableObject)
            {
                GameManager.Instance.RemoveOperation(draggableObject.Operation);
            }

            public void Run ()
            {
                GameManager.Instance.RunCode();
                ShowHideRunButton(false);
            }
            public void Reset ()
            {
                GameManager.Instance.ResetCode();
                ShowHideRunButton(true);
            }

            private void ShowHideRunButton (bool isShow)
            {
                buttonRun.SetActive(isShow);
                buttonReset.SetActive(!isShow);
            }
            
            private void ShowFinish ()
            {
                PlayerPrefs.SetInt("CurrentLevel",PlayerPrefs.GetInt("CurrentLevel")+1);
                if (PlayerPrefs.GetInt("PlayerLevel")<=PlayerPrefs.GetInt("CurrentLevel") )
                {
                    PlayerPrefs.SetInt("PlayerLevel",PlayerPrefs.GetInt("PlayerLevel")+1);
                } 
                
                panelFinish.SetActive(true);
                procPanel.SetActive(false);
                
                levelCounterText.transform.parent.gameObject.SetActive(false);
            }

            public void NextLevel ()
            {
                SceneManager.LoadScene(1);
            }

            public void BackToMain ()
            {
                SceneManager.LoadScene(0);
            }
        #endregion
    }
}