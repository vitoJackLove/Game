﻿using System.IO;

namespace GameFramework.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class IdProcessor : DataProcessor
        {
            public override System.Type Type
            {
                get
                {
                    return typeof(int);
                }
            }

            public override bool IsId
            {
                get
                {
                    return true;
                }
            }

            public override bool IsComment
            {
                get
                {
                    return false;
                }
            }

            public override bool IsSystem
            {
                get
                {
                    return false;
                }
            }

            public override string LanguageKeyword
            {
                get
                {
                    return "int";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "id"
                };
            }

            public override void WriteToStream(BinaryWriter stream, string value)
            {
                stream.Write(int.Parse(value));
            }
        }
    }
}
