using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GIS.AddIns.Ca.CommonDialog
{
    public partial class AhpSetUpForm : Form
    {
        #region fields
        List<string> fileNames = null;
        float[,] matrix = null;
        float[] weights = null;
        #endregion

        #region properties
        public List<string> FileNames
        {
            get
            {
                return this.fileNames;
            }
            set
            {
                this.fileNames = value;
            }
        }

        public float[] Weights
        {
            get
            {

                return this.weights;
            }
        }
        #endregion

        #region constructor
        public AhpSetUpForm(List<string> fileNames)
        {
            this.fileNames = fileNames;
            InitializeComponent();
            InitializeMatrix();
        }
        #endregion

        #region private methods
        public void InitializeMatrix()
        {
            


            for (int i = 0; i < fileNames.Count; i++)
            {

                DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
                column.ReadOnly = false;
                column.HeaderText = fileNames[i];
                dataGridViewMatrix.Columns.Add(column);
                DataGridViewRow row = new DataGridViewRow();
                row.HeaderCell.Value = fileNames[i];
                dataGridViewMatrix.Rows.Add(row);
            }
            dataGridViewMatrix.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            DataGridViewComboBoxCell cell = null;
            for (int i = 0; i < fileNames.Count; i++)
            {

                for (int j = 0; j < fileNames.Count; j++)
                {

                    cell = dataGridViewMatrix.Rows[i].Cells[j] as DataGridViewComboBoxCell;
                    if (i == j)
                    {
                        cell.Items.Add("1");
                    }
                    else
                    {
                        cell.Items.Add("1");
                        cell.Items.Add("2");
                        cell.Items.Add("3");
                        cell.Items.Add("4");
                        cell.Items.Add("5");
                        cell.Items.Add("6");
                        cell.Items.Add("7");
                        cell.Items.Add("8");
                        cell.Items.Add("9");                        
                        cell.Items.Add("1/2");
                        cell.Items.Add("1/3");
                        cell.Items.Add("1/4");
                        cell.Items.Add("1/5");
                        cell.Items.Add("1/6");
                        cell.Items.Add("1/7");
                        cell.Items.Add("1/8");
                        cell.Items.Add("1/9");
                    }
                }
            }

        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            if (IsDataGridViewCellsNotNull())
            {
                SetMatrix();
                var success = ChenkConsistentAndGetWeight();
                if (!success)
                {
                    MessageBox.Show("不一致");

                }
                else
                {
                    StringBuilder stb = new StringBuilder();
                    stb.AppendLine("权重：");
                    stb.AppendLine("");
                    foreach (var w in weights)
                    {
                        stb.AppendLine(w.ToString());
                    }
                    MessageBox.Show(stb.ToString());
                    this.Close();
                }
                
            }
            else
            {
                MessageBox.Show("没填满");
            }
        }

        /// <summary>
        /// 检测判断矩阵的一致性并得到各因素的权重。
        /// </summary>
        private bool ChenkConsistentAndGetWeight()
        {
            int matrixLength = this.fileNames.Count;

            //1.每一列求和并归一化，得到列归一化后的矩阵。
            float[] columnSumArray = new float[matrixLength];
            float columnSum = 0;
            for (int j = 0; j < matrixLength; j++)
            {
                columnSum = 0;
                for (int i = 0; i < matrixLength; i++)
                {
                    columnSum += matrix[i, j];
                }
                columnSumArray[j] = columnSum;
            }
            float[,] columnNormalizedMatrix = new float[matrixLength, matrixLength];
            for (int j = 0; j < matrixLength; j++)
            {
                for (int i = 0; i < matrixLength; i++)
                {
                    columnNormalizedMatrix[i, j] = matrix[i, j] / columnSumArray[j];
                }
            }

            //2.对列归一化后的矩阵进行每一行求和。
            float[] rowSumArray = new float[matrixLength];
            float rowSum = 0;
            for (int i = 0; i < matrixLength; i++)
            {
                rowSum = 0;
                for (int j = 0; j < matrixLength; j++)
                {
                    rowSum += columnNormalizedMatrix[i, j];
                }
                rowSumArray[i] = rowSum;
            }

            //3.对求和后的行归一化，得到w，检验一致性满足即为最终权重。
            float sum = 0;
            for (int i = 0; i < matrixLength; i++)
                sum += rowSumArray[i];
            float[] w = new float[matrixLength];
            for (int i = 0; i < matrixLength; i++)
            {
                w[i] = rowSumArray[i] / sum;
            }

            //4.一致性检验。得到Aw，λmax，CI，RI，CR
            //4.1 判断矩阵A*w得到Aw。
            float[] Aw = new float[matrixLength];
            for (int i = 0; i < matrixLength; i++)
            {
                for (int j = 0; j < matrixLength; j++)
                {
                    Aw[i] += matrix[i, j] * w[j];
                }
            }

            //4.2 求得λmax。
            float lambdaMax = 0;
            for (int i = 0; i < matrixLength; i++)
            {
                lambdaMax += Aw[i] / w[i] / matrixLength;
            }

            //4.3 求CI，RI，CR。
            float CI = (lambdaMax - matrixLength) / (matrixLength - 1);
            float RI = GetRI(matrixLength);
            float CR = CI / RI;

            if (CR < 0.10)
            {
                this.weights = w;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 得到随机一致性指标。
        /// </summary>
        private float GetRI(int n)
        {
            switch (n)
            {
                case 1:
                    return 0f;
                case 2:
                    return 0f;
                case 3:
                    return 0.58f;
                case 4:
                    return 0.90f;
                case 5:
                    return 1.12f;
                case 6:
                    return 1.24f;
                case 7:
                    return 1.32f;
                case 8:
                    return 1.41f;
                case 9:
                    return 1.45f;
                case 10:
                    return 1.49f;
                case 11:
                    return 1.52f;
                case 12:
                    return 1.54f;
                case 13:
                    return 1.56f;
                case 14:
                    return 1.58f;
                case 15:
                    return 1.59f;
                default:
                    return -1f;
            }
        }

        private void SetMatrix()
        {
            int length = this.fileNames.Count;
            this.matrix = new float[length, length];
            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < length; col++)
                {
                    if(row == col)
                    {
                        this.matrix[row, col] = 1.0f;
                    }
                    else
                    {
                        this.matrix[row, col] = GetFloatValueByText(dataGridViewMatrix.Rows[row].Cells[col].Value.ToString());

                    }
                }
            }            
        }
        /// <summary>
        /// 根据文本值获得Float值。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private float GetFloatValueByText(string text)
        {
            switch (text)
            {
                case "1":
                    return 1f;
                case "2":
                    return 2f;
                case "3":
                    return 3f;
                case "4":
                    return 4f;
                case "5":
                    return 5f;
                case "6":
                    return 6f;
                case "7":
                    return 7f;
                case "8":
                    return 8f;
                case "9":
                    return 9f;
                case "1/2":
                    return 1 / 2f;
                case "1/3":
                    return 1 / 3f;
                case "1/4":
                    return 1 / 4f;
                case "1/5":
                    return 1 / 5f;
                case "1/6":
                    return 1 / 6f;
                case "1/7":
                    return 1 / 7f;
                case "1/8":
                    return 1 / 8f;
                case "1/9":
                    return 1 / 9f;
                default:
                    return 1;
            }
        }


        /// <summary>
        /// 判断矩阵中的值是否为空。
        /// </summary>
        /// <returns></returns>
        private bool IsDataGridViewCellsNotNull()
        {
            int rowCount = this.fileNames.Count;
            int columCount = this.fileNames.Count;

            if (this.dataGridViewMatrix.Rows.Count == 0)
            {
                return false;
            }

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < columCount; col++)
                {
                    if (this.dataGridViewMatrix.Rows[row].Cells[col].Value == null)
                    {
                        return false;
                    }
                     
                }
            }
            return true;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridViewMatrix.Rows[0].Cells[0].Value = "1";
                this.dataGridViewMatrix.Rows[0].Cells[1].Value = "3";
                this.dataGridViewMatrix.Rows[0].Cells[2].Value = "3";
                this.dataGridViewMatrix.Rows[0].Cells[3].Value = "3";
                this.dataGridViewMatrix.Rows[0].Cells[4].Value = "2";

                this.dataGridViewMatrix.Rows[1].Cells[0].Value = "1/3";
                this.dataGridViewMatrix.Rows[1].Cells[1].Value = "1";
                this.dataGridViewMatrix.Rows[1].Cells[2].Value = "2";
                this.dataGridViewMatrix.Rows[1].Cells[3].Value = "1";
                this.dataGridViewMatrix.Rows[1].Cells[4].Value = "1/2";

                this.dataGridViewMatrix.Rows[2].Cells[0].Value = "1/3";
                this.dataGridViewMatrix.Rows[2].Cells[1].Value = "1/2";
                this.dataGridViewMatrix.Rows[2].Cells[2].Value = "1";
                this.dataGridViewMatrix.Rows[2].Cells[3].Value = "1";
                this.dataGridViewMatrix.Rows[2].Cells[4].Value = "1/2";

                this.dataGridViewMatrix.Rows[3].Cells[0].Value = "1/3";
                this.dataGridViewMatrix.Rows[3].Cells[1].Value = "1";
                this.dataGridViewMatrix.Rows[3].Cells[2].Value = "1";
                this.dataGridViewMatrix.Rows[3].Cells[3].Value = "1";
                this.dataGridViewMatrix.Rows[3].Cells[4].Value = "1/2";

                this.dataGridViewMatrix.Rows[4].Cells[0].Value = "1/2";
                this.dataGridViewMatrix.Rows[4].Cells[1].Value = "2";
                this.dataGridViewMatrix.Rows[4].Cells[2].Value = "2";
                this.dataGridViewMatrix.Rows[4].Cells[3].Value = "2";
                this.dataGridViewMatrix.Rows[4].Cells[4].Value = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show("填不好：" + ex.ToString());
            }
            
        }

    }

}
