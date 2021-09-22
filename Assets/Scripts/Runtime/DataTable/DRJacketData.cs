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
    public class DRJacketData : DataRowBase
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
        /// 获取装备类型。
        /// </summary>
        public EquipTypeEnum EquipType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取说明。
        /// </summary>
        public string Doc
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取气血最低值。
        /// </summary>
        public int HPMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取气血最高值。
        /// </summary>
        public int HPMax
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取物防最低值。
        /// </summary>
        public int PhysicsDefenseMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取物防最高值。
        /// </summary>
        public int PhysicsDefenseMax
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取属性最低值。
        /// </summary>
        public int PropertyMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取属性最高值。
        /// </summary>
        public int PropertyMax
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
            EquipType = (EquipTypeEnum)System.Enum.Parse(typeof(EquipTypeEnum),columnTexts[index++]);
            Doc = columnTexts[index++];
            HPMin = int.Parse(columnTexts[index++]);
            HPMax = int.Parse(columnTexts[index++]);
            PhysicsDefenseMin = int.Parse(columnTexts[index++]);
            PhysicsDefenseMax = int.Parse(columnTexts[index++]);
            PropertyMin = int.Parse(columnTexts[index++]);
            PropertyMax = int.Parse(columnTexts[index++]);
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
            EquipType = (EquipTypeEnum)System.Enum.Parse(typeof(EquipTypeEnum),binaryReader.ReadString());
                    Doc = binaryReader.ReadString();
                    HPMin = binaryReader.ReadInt32();
                    HPMax = binaryReader.ReadInt32();
                    PhysicsDefenseMin = binaryReader.ReadInt32();
                    PhysicsDefenseMax = binaryReader.ReadInt32();
                }
            }

            return true;
        }

//__DATA_TABLE_STREAM_PARSER__

//__DATA_TABLE_PROPERTY_ARRAY__
    }
}
