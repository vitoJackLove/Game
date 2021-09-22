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
    public class DRBuffData : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取配置Id。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取说明。
        /// </summary>
        public string Text
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否无法覆盖。
        /// </summary>
        public bool IsOverlay
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取功能组。
        /// </summary>
        public int Intention
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取行为树ID。
        /// </summary>
        public int BehaviorTreeId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取改变层数是否刷新存活时间。
        /// </summary>
        public bool ChangeLayeResetTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取Buff标签。
        /// </summary>
        public List<string> Tags
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取最大层数。
        /// </summary>
        public int MaxLayer
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否是定时buff。
        /// </summary>
        public bool IsTimingBuff
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取Buff结束时减少一层还是全部消除。
        /// </summary>
        public BuffOverTypeEnum OverType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取总时长 过时自动销毁。
        /// </summary>
        public float Time
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否显示层数。
        /// </summary>
        public bool IsShowLayer
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否重生时删除。
        /// </summary>
        public bool IsRebornDelete
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取buffUI图标。
        /// </summary>
        public string BuffIcon
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
            Text = columnTexts[index++];
            IsOverlay = bool.Parse(columnTexts[index++]);
            Intention = int.Parse(columnTexts[index++]);
            BehaviorTreeId = int.Parse(columnTexts[index++]);
            ChangeLayeResetTime = bool.Parse(columnTexts[index++]);
            Tags = DataTableExtension.ParseListstring(columnTexts[index++]);
            MaxLayer = int.Parse(columnTexts[index++]);
            IsTimingBuff = bool.Parse(columnTexts[index++]);
            OverType = (BuffOverTypeEnum)System.Enum.Parse(typeof(BuffOverTypeEnum),columnTexts[index++]);
            Time = float.Parse(columnTexts[index++]);
            IsShowLayer = bool.Parse(columnTexts[index++]);
            IsRebornDelete = bool.Parse(columnTexts[index++]);
            BuffIcon = columnTexts[index++];
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
                    Text = binaryReader.ReadString();
                    IsOverlay = binaryReader.ReadBoolean();
                    Intention = binaryReader.ReadInt32();
                    BehaviorTreeId = binaryReader.ReadInt32();
                    ChangeLayeResetTime = binaryReader.ReadBoolean();
                    Tags = DataTableExtension.ParseListstring(binaryReader.ReadString());
                    MaxLayer = binaryReader.ReadInt32();
                    IsTimingBuff = binaryReader.ReadBoolean();
            OverType = (BuffOverTypeEnum)System.Enum.Parse(typeof(BuffOverTypeEnum),binaryReader.ReadString());
                    Time = binaryReader.ReadSingle();
                    IsShowLayer = binaryReader.ReadBoolean();
                    IsRebornDelete = binaryReader.ReadBoolean();
                    BuffIcon = binaryReader.ReadString();
                }
            }

            return true;
        }

//__DATA_TABLE_STREAM_PARSER__

//__DATA_TABLE_PROPERTY_ARRAY__
    }
}
