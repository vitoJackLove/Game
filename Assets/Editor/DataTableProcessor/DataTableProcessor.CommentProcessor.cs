﻿using System.IO;

namespace GameFramework.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class CommentProcessor : DataProcessor
        {
            public override System.Type Type
            {
                get
                {
                    return null;
                }
            }

            public override bool IsId
            {
                get
                {
                    return false;
                }
            }

            public override bool IsComment
            {
                get
                {
                    return true;
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
                    return null;
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    string.Empty,
                    "#",
                    "comment"
                };
            }

            public override void WriteToStream(BinaryWriter stream, string value)
            {
            }
        }
    }
}
