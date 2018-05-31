using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlan
{
    public class ModelParameter
    {
        public string label;//图形的标注
        public string value;//参数值

        /// <summary>
        /// 初始化，解析模型输入/输出参数
        /// </summary>
        public void Initial()
        { }

        public void Initial(string shapeInfo)
        {
            if ((shapeInfo != "") && (shapeInfo != "SQL") && (shapeInfo != null))
            {
                string[] infos = shapeInfo.Split('|');
                label = infos[0];
                value = infos[1];
            }
        }
    }


}
