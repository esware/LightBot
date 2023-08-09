using System;
using System.Collections;
using System.Collections.Generic;
using Dev.Scripts.Commands;
using Game.UI;
using UnityEngine;

namespace Dev.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public readonly Procedure mainProcedure = new Procedure();
        public static GameManager Instance;
        #endregion

        private void Awake()
        {
            if (Instance!=null)
            {
                 Destroy(gameObject);
            }
            Instance = this;
            
            if (mainProcedure.Operations.Count>0)
            {
                mainProcedure.ClearOperations();
            }
        }

        public void AddOperation(BaseOperation operation) => mainProcedure.AddOperation(operation);
        public void RemoveOperation(BaseOperation operation) => mainProcedure.RemoveOperation(operation);

        public void ResetCode()
        {
            if (mainProcedure.Operations.Count>0)
            {
                mainProcedure.ClearOperations();
                Debug.Log("Clear");
            }
            

            foreach (Transform p in UIHandler.Instance.targetProc.transform)
            {
                if (p!=null)
                {
                    Destroy(p.gameObject);
                }
            }
            StartCoroutine(DoResetCode());
        }

        public void RunCode()
        {
            StartCoroutine(DoRunCode());
        }

        private IEnumerator DoRunCode()
        {
            foreach (var op in mainProcedure.Operations)
            {
                yield return op.Run();
            }
            yield return new WaitForFixedUpdate();
        }
        
        
        private IEnumerator DoResetCode()
        {
            ResetOperation resetOperation = new ResetOperation();
            resetOperation.Init();
            yield return resetOperation.Run();
            StopAllCoroutines();
        }
    }
}