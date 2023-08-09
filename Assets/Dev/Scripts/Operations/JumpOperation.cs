using System.Collections;
using System.Collections.Generic;
using Dev.Scripts.Character;
using Dev.Scripts.Tiles;
using UnityEngine;

namespace Dev.Scripts.Commands
{
    public class JumpOperation:BaseOperation
    {
        #region Variables
        private Position _nextPosition;
        #endregion

        #region Methods
        public override bool IsValid ()
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

            return LevelManager.Instance.TileIsExist(_nextPosition);
        }

        public override IEnumerator Run ()
        {
            if (IsValid())
            {
                Tile currentPlatform = LevelManager.Instance.GetTile(Control.currentPosition);
                Tile nextPlatform = LevelManager.Instance.GetTile(_nextPosition);

                if ((nextPlatform.Height - currentPlatform.Height == 1) 
                    || (currentPlatform.Height - nextPlatform.Height > 0))
                    yield return Control.Jump(_nextPosition, nextPlatform.lastBlock.transform.position);
            }
        }
        #endregion
    }
}