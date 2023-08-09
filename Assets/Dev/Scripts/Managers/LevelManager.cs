using System;
using System.Collections.Generic;
using Dev.Scripts.Tiles;
using UnityEngine;

namespace Dev.Scripts
{
    public struct GameEvents
    {
        public static Action OnLevelCompleted;
        
    }
    public class LevelManager : MonoBehaviour
    {
        #region Variables
        [HideInInspector] public List<Tile> tileList;
        [HideInInspector] public List<TargetTile> targetTileList;
        public static LevelManager Instance;
        
        [SerializeField] private GameData gameData;
        
        
        private const int LevelResetIndex = 1;
        
        private int _currentLevel;

        private int _levelIndex;

        #endregion

        private void Awake()
        {
            if (Instance != null)
                Destroy(this.gameObject);
            Instance = this;
            
            InstantiateLevel();
            InitLevelData();
        }

        private void InstantiateLevel()
        {
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            if (_currentLevel >= gameData.levelDatas.Length-1)
            {
                PlayerPrefs.SetInt("CurrentLevel",0);
            }
            Instantiate(gameData.levelDatas[_currentLevel].levelObject);
        }
        private void InitLevelData()
        {
            Tile[] tiles = FindObjectsOfType<Tile>();

            foreach (var tile in tiles)
            {
                tileList.Add(tile);
                if (tile.GetComponent<TargetTile>())
                    targetTileList.Add(tile.GetComponent<TargetTile>());
            }
        }
        
        public bool TileIsExist (Position position)
        {
            foreach (var tile in tileList)
            {
                if (tile.Position == position)
                {
                    return true;
                }
            }

            return false;
        }

        public Tile GetTile(Position position)
        {
            if (TileIsExist(position))
            {
                foreach (var tile in tileList)
                {
                    if (tile.Position == position)
                    {
                        return tile;
                    }
                }
            }

            return null;
        }

        public TargetTile GetTargetTile(Position pos)
        {
            if (TileIsExist(pos))
            {
                var targetTile = GetTile(pos);
                if (targetTile.GetComponent<TargetTile>())
                {
                    return (TargetTile)targetTile;
                }
            }

            return null;
        }

        public void ResetAllTargetTiles()
        {
            foreach (var targetTile in targetTileList)
            {
                if (targetTile.GetComponent<TargetTile>())
                {
                    targetTile.GetComponent<TargetTile>().Reset();
                }
            }
        }
        public bool IsAllTargetsSwitchOn()
        {
            foreach (var target in targetTileList)
            {
                if (!target.GetSwitchStatus())
                    return false;
            }

            return true;
        }
    }
}