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
    public class DRIntentionsData : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取IntentionId。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取计算优先级。
        /// </summary>
        public int Priority
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取timing的冷却时间。
        /// </summary>
        public float TimingCooling
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取时机。
        /// </summary>
        public string Timing
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取不必要条件。
        /// </summary>
        public List<string> UnNecessaryConditionkeys
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取不必要条件的变量。
        /// </summary>
        public string UnNecessaryConditionVariable
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取必要条件。
        /// </summary>
        public List<string> NecessaryConditionkeys
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取必要条件的变量。
        /// </summary>
        public string NecessaryConditionVariable
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取执行的功能。
        /// </summary>
        public List<int> FunctionKeys
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否开启新的流程。
        /// </summary>
        public bool IsStartNewProccess
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否响应其它buff。
        /// </summary>
        public bool IsCallOtherBuff
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
            Priority = int.Parse(columnTexts[index++]);
            TimingCooling = float.Parse(columnTexts[index++]);
            Timing = columnTexts[index++];
            UnNecessaryConditionkeys = DataTableExtension.ParseListstring(columnTexts[index++]);
            UnNecessaryConditionVariable = columnTexts[index++];
            NecessaryConditionkeys = DataTableExtension.ParseListstring(columnTexts[index++]);
            NecessaryConditionVariable = columnTexts[index++];
            FunctionKeys = DataTableExtension.ParseListInt(columnTexts[index++]);
            IsStartNewProccess = bool.Parse(columnTexts[index++]);
            IsCallOtherBuff = bool.Parse(columnTexts[index++]);
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
                    Priority = binaryReader.ReadInt32();
                    TimingCooling = binaryReader.ReadSingle();
                    Timing = binaryReader.ReadString();
                    UnNecessaryConditionkeys = DataTableExtension.ParseListstring(binaryReader.ReadString());
                    UnNecessaryConditionVariable = binaryReader.ReadString();
                    NecessaryConditionkeys = DataTableExtension.ParseListstring(binaryReader.ReadString());
                    NecessaryConditionVariable = binaryReader.ReadString();
                    FunctionKeys = DataTableExtension.ParseListInt(binaryReader.ReadString());
                    IsStartNewProccess = binaryReader.ReadBoolean();
                    IsCallOtherBuff = binaryReader.ReadBoolean();
                }
            }

            return true;
        }

//__DATA_TABLE_STREAM_PARSER__

//__DATA_TABLE_PROPERTY_ARRAY__
    }
}
