using System;
using System.Collections.Generic;
using System.IO;

namespace GameFramework.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class ListIntProcessor : GenericDataProcessor<List<int>>
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
                get { return "List<int>"; }
            }

            public override string MethodName
            {
                get { return "ListInt"; }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "array<int>",
                };
            }
            
            public override List<int> Parse(string value)
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