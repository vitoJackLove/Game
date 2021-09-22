using System;
using System.Collections.Generic;
using System.IO;

namespace GameFramework.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class ListStringProcessor : GenericDataProcessor<List<string>>
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
                get { return "List<string>"; }
            }

            public override string MethodName
            {
                get { return "Liststring"; }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "array<string>",
                };
            }
            
            public override List<string> Parse(string value)
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