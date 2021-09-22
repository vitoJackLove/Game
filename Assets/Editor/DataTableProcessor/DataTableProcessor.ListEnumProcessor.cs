using System;
using System.Collections.Generic;
using System.IO;

namespace GameFramework.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class ListEnumProcessor : GenericDataProcessor<List<Enum>>
        {
            /// <summary>
            /// 枚举类型
            /// </summary>
            private string _enumType;
            public override bool IsSystem
            {
                get { return false; }
            }

            public override SpecialType specialType
            {
                get { return SpecialType.List; }
            }

            /// <summary>
            /// 生成代码的变量类型
            /// </summary>
            public override string LanguageKeyword
            {
                get { return $"List<{_enumType}>"; }
            }

            public void SetEnumType(string enumType)
            {
                this._enumType = enumType;
            }
            
            public override string MethodName
            {
                get { return $"ListEnum<{_enumType}>"; }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                   $"array<{_enumType}>",
                };
            }
            
            public override List<Enum> Parse(string value)
            {
                return null;
            }

            public override void WriteToStream(BinaryWriter stream, string value)
            {
                stream.Write(value);
            }
        }
    }
}