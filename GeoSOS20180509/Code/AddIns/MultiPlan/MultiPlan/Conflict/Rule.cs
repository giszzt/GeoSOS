using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace MultiPlan
{
    public class Rule
    {
        /// <summary>
        /// 冲突字典
        /// </summary>
        public Dictionary<string, Conflict> ConflictDic
        {
            get
            {
                return _conflictDic;
            }
            set
            {
                _conflictDic = value;
            }
        }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        Dictionary<string, Conflict> _conflictDic = new Dictionary<string, Conflict>();//冲突字典
        string _name;//规则名称
    }
}
