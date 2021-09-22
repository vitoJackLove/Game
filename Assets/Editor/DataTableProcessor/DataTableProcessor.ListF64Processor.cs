using System;
using System.Collections.Generic;
using System.IO;

namespace GameFramework.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class ListF64Processor : GenericDataProcessor<List<Single>>
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
                get { return "List<F64>"; }
            }

            public override string MethodName
            {
                get { return "ListF64"; }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "array<lfloat>",
                };
            }
            
            public override List<Single> Parse(string value)
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