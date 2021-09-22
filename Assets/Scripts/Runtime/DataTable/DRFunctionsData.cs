using GameFramework;
using System.IO;
using System.Text;
using GameFramework.DataTable;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// C。
    /// </summary>
    public class DRFunctionsData : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取函数Id。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取函数名字。
        /// </summary>
        public string FunctionName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取变量。
        /// </summary>
        public string FunctionVariable
        {
            get;
            private set;
        }

        public override bool ParseDataRow(GameFrameworkSegment<string> dataRowSegment)
        {
            // Star Force 示例代码，正式项目使用时请调整此处的生成代码，以处理 GCAlloc 问题！
            string[] columnTexts = dataRowSegment.Source.Substring(dataRowSegment.Offset, dataRowSegment.Length).Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnTexts.Length; i++)
            {
                columnTexts[i] = columnTexts[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnTexts[index++]);
            FunctionName = columnTexts[index++];
            FunctionVariable = columnTexts[index++];
            index++;

            return true;
        }

        public override bool ParseDataRow(GameFrameworkSegment<byte[]> dataRowSegment)
        {
            // Star Force 示例代码，正式项目使用时请调整此处的生成代码，以处理 GCAlloc 问题！
            using (MemoryStream memoryStream = new MemoryStream(dataRowSegment.Source, dataRowSegment.Offset, dataRowSegment.Length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.ReadInt32();
                    FunctionName = binaryReader.ReadString();
                    FunctionVariable = binaryReader.ReadString();
                }
            }

            return true;
        }

//__DATA_TABLE_STREAM_PARSER__

//__DATA_TABLE_PROPERTY_ARRAY__
    }
}
