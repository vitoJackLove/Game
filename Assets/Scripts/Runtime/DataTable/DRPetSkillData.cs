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
    public class DRPetSkillData : DataRowBase
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
        /// 获取等级（0是高级，1是低级）。
        /// </summary>
        public int Level
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能描述。
        /// </summary>
        public string Describe
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否是主动技能。
        /// </summary>
        public bool IsActive
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取消耗的法力值。
        /// </summary>
        public int Magic
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能图标路径。
        /// </summary>
        public string IconPath
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
            Level = int.Parse(columnTexts[index++]);
            Describe = columnTexts[index++];
            IsActive = bool.Parse(columnTexts[index++]);
            Magic = int.Parse(columnTexts[index++]);
            IconPath = columnTexts[index++];
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
                    Level = binaryReader.ReadInt32();
                    Describe = binaryReader.ReadString();
                    IsActive = binaryReader.ReadBoolean();
                    Magic = binaryReader.ReadInt32();
                    IconPath = binaryReader.ReadString();
                }
            }

            return true;
        }

//__DATA_TABLE_STREAM_PARSER__

//__DATA_TABLE_PROPERTY_ARRAY__
    }
}
