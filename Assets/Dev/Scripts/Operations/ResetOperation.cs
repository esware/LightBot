using System.Collections;
using System.Collections.Generic;
using Dev.Scripts.Character;
using UnityEngine;

namespace Dev.Scripts.Commands
{
    public class ResetOperation : BaseOperation
    {
        public override bool IsValid()
        {
            return true;
        }

        public override IEnumerator Run()
        {
            LevelManager.Instance.ResetAllTargetTiles();
            Control.Reset();
            return null;
        }
    }
}