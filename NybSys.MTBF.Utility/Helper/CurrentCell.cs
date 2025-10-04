using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NybSys.MTBF.Utility.Helper
{
    public class CurrentCell
    {
        private int _rowIndex = 0;
        private int _columnIndex = 0;

        public CurrentCell(int rowIndex, int columnIndex)
        {
            this._rowIndex = rowIndex;
            this._columnIndex = columnIndex;
        }
        public int RowIndex { get { return _rowIndex; } }
        public int ColumnIndex { get { return _columnIndex; } }
    }
}
