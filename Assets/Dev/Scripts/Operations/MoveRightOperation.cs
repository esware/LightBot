using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev.Scripts.Commands
{
    public class MoveRightOperation : BaseOperation
    {
        #region Methods
        public override bool IsValid ()
        {
            return true;
        }

        public override IEnumerator Run ()
        {
            yield return Control.TurnRight();
        }
        #endregion
    }
}