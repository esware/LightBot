using UnityEngine;

namespace Dev.Scripts
{
    [CreateAssetMenu(fileName = "GameData" ,menuName = "SerhatGames/ScriptableObject/Data/GameData")]
    public class GameData: ScriptableObject
    {
        [SerializeField] public LevelData[] levelDatas;
        
        private int _lastLevelIndex = 0;

        public int LastLevelIndex
        {
            get => _lastLevelIndex;
            set => _lastLevelIndex = value;
        }
    }
}