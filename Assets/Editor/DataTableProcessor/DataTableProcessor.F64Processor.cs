using System;
using System.IO;

namespace GameFramework.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class F64Processor : GenericDataProcessor<Single>
        {
            public override bool IsSystem
            {
                get
                {
                    return true;
                }
            }

            public override string LanguageKeyword
            {
                get
                {
                    return "float";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "lfloat",
                };
            }

            public override Single Parse(string value)
            {
                return float.Parse(value);
            }

            public override void WriteToStream(BinaryWriter stream, string value)
            {
                if (string.IsNullOrEmpty(value))
                {
                    stream.Write(0);
                    return;
                }
                stream.Write(Parse(value));
            }
        }
    }
}
