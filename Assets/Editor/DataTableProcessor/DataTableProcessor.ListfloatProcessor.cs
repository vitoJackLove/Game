using System;
using System.Collections.Generic;
using System.IO;

namespace GameFramework.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class ListfloatProcessor : GenericDataProcessor<List<float>>
        {
            public override bool IsSystem
            {
                get { return false; }
            }

            public override SpecialType specialType
            {
                get { return SpecialType.List; }
            }

            public override string LanguageKeyword
            {
                get { return "List<float>"; }
            }

            public override string MethodName
            {
                get { return "Listfloat"; }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "array<float>",
                };
            }
            
            public override List<float> Parse(string value)
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