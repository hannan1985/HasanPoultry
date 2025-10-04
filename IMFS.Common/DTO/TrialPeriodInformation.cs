using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("TrialPeriodInformation")]
    public class TrialPeriodInformation
    {
        private int _SerialNo = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int SerialNo
        {
            get { return _SerialNo; }
            set { _SerialNo = value; }
        }


        private string _TrialPeriod = string.Empty;

        [DataType("varchar")]
        public string TrialPeriod
        {
            get { return _TrialPeriod; }
            set { _TrialPeriod = value; }
        }

        private string _CheckPeriod = string.Empty;

        [DataType("varchar")]
        public string CheckPeriod
        {
            get { return _CheckPeriod; }
            set { _CheckPeriod = value; }
        }

        private string _CheckDate = string.Empty;

        [DataType("varchar")]
        public string CheckDate
        {
            get { return _CheckDate; }
            set { _CheckDate = value; }
        }

    }
}
