﻿using System.IO;

namespace GameFramework.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class UShortProcessor : GenericDataProcessor<ushort>
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
                    return "ushort";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "ushort",
                    "uint16",
                    "system.uint16"
                };
            }

            public override ushort Parse(string value)
            {
                return ushort.Parse(value);
            }

            public override void WriteToStream(BinaryWriter stream, string value)
            {
                stream.Write(Parse(value));
            }
        }
    }
}
