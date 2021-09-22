using UnityEngine;

namespace GameFramework.DataTable
{
    /// <summary>
    /// 数据表行基类。
    /// </summary>
    public abstract class DataRowBase : IDataRow
    {
        /// <summary>
        /// 获取数据表行的编号。
        /// </summary>
        public abstract int Id
        {
            get;
        }

        /// <summary>
        /// 数据表行文本解析器。
        /// </summary>
        /// <param name="dataRowSegment">要解析的数据表行片段。</param>
        /// <returns>是否解析数据表行成功。</returns>
        public virtual bool ParseDataRow(GameFrameworkSegment<string> dataRowSegment)
        {
            Debug.LogWarning("Not implemented ParseDataRow(GameFrameworkSegment<string>)");
            return false;
        }

        /// <summary>
        /// 数据表行二进制流解析器。
        /// </summary>
        /// <param name="dataRowSegment">要解析的数据表行片段。</param>
        /// <returns>是否解析数据表行成功。</returns>
        public virtual bool ParseDataRow(GameFrameworkSegment<byte[]> dataRowSegment)
        {
            Debug.LogWarning("Not implemented ParseDataRow(GameFrameworkSegment<byte[]>)");
            return false;
        }
    }
}
