using System.Collections.Generic;

namespace Dev.Scripts
{
    public class Procedure
    {
        #region Variables

        private List<BaseOperation> _operations;
        #endregion

        #region Properties
        public List<BaseOperation> Operations => _operations;
        #endregion

        #region Methods

        public Procedure()
        {
            _operations = new List<BaseOperation>();
        }

        public void AddOperation(BaseOperation operation) => _operations.Add(operation);
        public void RemoveOperation(BaseOperation operation) => _operations.Remove(operation);

        public void ClearOperations()
        {
            _operations.Clear();
        }

        #endregion
    }
}