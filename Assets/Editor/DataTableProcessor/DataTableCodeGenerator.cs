using System.Text;

namespace GameFramework.Editor.DataTableTools
{
    public delegate void DataTableCodeGenerator(DataTableProcessor dataTableProcessor, StringBuilder codeContent, object userData);
}
