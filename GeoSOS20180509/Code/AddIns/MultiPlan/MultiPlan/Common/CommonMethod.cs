using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Data;

namespace MultiPlan
{
    public class CommonMethod
    {
        public static IFeatureSet GetFeatureSetByPath(string path)
        {
            DataManager dataManager = new DataManager();
            IFeatureSet featureSet = dataManager.OpenFile(path) as IFeatureSet;
            return featureSet;
        }

        public static List<string> GetFields(IFeatureSet featureSet)
        {
            List<string> fields = new List<string>();

            if (featureSet != null)
            {
                foreach (System.Data.DataColumn item in featureSet.DataTable.Columns)
                {
                    fields.Add(item.Caption);
                }
            }

            return fields;
        }
    }
}
