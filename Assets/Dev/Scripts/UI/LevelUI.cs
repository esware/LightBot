using System;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.Scripts.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private Transform levelContainer;
        [SerializeField] private GameObject levelObject;
        [SerializeField] private GameData gameData;

        private int _currentLevelIndex;

        private void Awake()
        {
            InitData();
        }

        private void InitData()
        {
            for (int i = 0; i < gameData.levelDatas.Length; i++)
            {
                var levelData= Instantiate(levelObject, levelContainer);
                levelData.GetComponent<MainMenuClickHandler>().levelIndex = i+1;
                levelData.GetComponent<MainMenuClickHandler>().SetData(i+1);
            }
        }
    }
}