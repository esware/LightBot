using System.Collections;
using System.Collections.Generic;
using Dev.Scripts.Tiles;

namespace Dev.Scripts.Commands
{
    public class PressButtonOperation:BaseOperation
    {
        #region Variables
        TargetTile targetTile;
        #endregion

        #region Methods
        public override bool IsValid ()
        {
            targetTile = LevelManager.Instance.GetTargetTile(Control.currentPosition);

            if (targetTile)
                return true;
            return false;
        }

        public override IEnumerator Run ()
        {
            if (IsValid())
                yield return Control.Switch(targetTile);
        }
        #endregion
    }
}