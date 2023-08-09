using UnityEngine;

namespace Dev.Scripts
{
    [CreateAssetMenu(fileName = "GameData" ,menuName = "SerhatGames/ScriptableObject/Data/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] public GameObject levelObject;
    }
}