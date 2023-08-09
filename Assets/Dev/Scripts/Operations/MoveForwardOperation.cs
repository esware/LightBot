using System;
using System.Collections;
using System.Collections.Generic;
using Dev.Scripts.Character;
using Dev.Scripts.Tiles;
using UnityEngine;

namespace Dev.Scripts.Commands
{
    public class MoveForwardOperation : BaseOperation
    {
        private Position _nextPosition;
        public override bool IsValid()
        {
            _nextPosition = Control.currentPosition;
            
            switch (Control.currentDirection)
            {
                case Direction.FORWARD:
                    _nextPosition += new Position(0, 1);
                    break;
                case Direction.BACKWARD:
                    _nextPosition += new Position(0, -1);
                    break;
                case Direction.LEFT:
                    _nextPosition += new Position(-1, 0);
                    break;
                case Direction.RIGHT:
                    _nextPosition += new Position(1, 0);
                    break;
                default:
                    break;
            }

            if (!LevelManager.Instance.TileIsExist(_nextPosition))
            {
                Debug.Log("Tile is not Exits");
            }
            
            return LevelManager.Instance.TileIsExist(_nextPosition);
        }

        public override IEnumerator Run()
        {
            if (IsValid())
            {
                Tile currentPlatform = LevelManager.Instance.GetTile(Control.currentPosition);
                Tile nextPlatform = LevelManager.Instance.GetTile(_nextPosition);
                
                if (currentPlatform.Height == nextPlatform.Height)
                {
                    yield return Control.Walk(_nextPosition, nextPlatform.lastBlock.transform.position);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
}