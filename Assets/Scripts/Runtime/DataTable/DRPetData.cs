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
    public class DRPetData : DataRowBase
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
        /// 获取名字。
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击力最大值。
        /// </summary>
        public int AttackMax
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击力最小值。
        /// </summary>
        public int AttackMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取防御最大值。
        /// </summary>
        public int DefenseMax
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取防御最小值。
        /// </summary>
        public int DefenseMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取气血最大值。
        /// </summary>
        public int HPMax
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取气血最小值。
        /// </summary>
        public int HPMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取灵力最大值。
        /// </summary>
        public int WakanMax
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取灵力最小值。
        /// </summary>
        public int WakanMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取速度最大值。
        /// </summary>
        public int SpeedMax
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取速度最小值。
        /// </summary>
        public int SpeedMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取成长最大值。
        /// </summary>
        public float GrowMax
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取成长最小值。
        /// </summary>
        public float GrowMin
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取资源路径。
        /// </summary>
        public string AssetsPath
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取最大技能数量。
        /// </summary>
        public int MaxSkillValue
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
            Name = columnTexts[index++];
            AttackMax = int.Parse(columnTexts[index++]);
            AttackMin = int.Parse(columnTexts[index++]);
            DefenseMax = int.Parse(columnTexts[index++]);
            DefenseMin = int.Parse(columnTexts[index++]);
            HPMax = int.Parse(columnTexts[index++]);
            HPMin = int.Parse(columnTexts[index++]);
            WakanMax = int.Parse(columnTexts[index++]);
            WakanMin = int.Parse(columnTexts[index++]);
            SpeedMax = int.Parse(columnTexts[index++]);
            SpeedMin = int.Parse(columnTexts[index++]);
            GrowMax = float.Parse(columnTexts[index++]);
            GrowMin = float.Parse(columnTexts[index++]);
            AssetsPath = columnTexts[index++];
            MaxSkillValue = int.Parse(columnTexts[index++]);
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
                    Name = binaryReader.ReadString();
                    AttackMax = binaryReader.ReadInt32();
                    AttackMin = binaryReader.ReadInt32();
                    DefenseMax = binaryReader.ReadInt32();
                    DefenseMin = binaryReader.ReadInt32();
                    HPMax = binaryReader.ReadInt32();
                    HPMin = binaryReader.ReadInt32();
                    WakanMax = binaryReader.ReadInt32();
                    WakanMin = binaryReader.ReadInt32();
                    SpeedMax = binaryReader.ReadInt32();
                    SpeedMin = binaryReader.ReadInt32();
                    GrowMax = binaryReader.ReadSingle();
                    GrowMin = binaryReader.ReadSingle();
                    AssetsPath = binaryReader.ReadString();
                    MaxSkillValue = binaryReader.ReadInt32();
                }
            }

            return true;
        }

//__DATA_TABLE_STREAM_PARSER__

//__DATA_TABLE_PROPERTY_ARRAY__
    }
}
