using System.Collections;
using System.Collections.Generic;
using Dev.Scripts.Character;
using Dev.Scripts.Tiles;
using UnityEngine;

namespace Dev.Scripts.Commands
{
    public class MoveLeftOperation : BaseOperation
    {
        #region Methods
        public override bool IsValid ()
        {
            return true;
        }

        public override IEnumerator Run ()
        {
            yield return Control.TurnLeft();
        }
        #endregion
    }
}