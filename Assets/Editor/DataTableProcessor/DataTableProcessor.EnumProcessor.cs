﻿using System;
using System.IO;

namespace GameFramework.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class EnumProcessor : GenericDataProcessor<Enum>
        {
            private string enumLanguageKeyword { get; set; }

            public override bool IsSystem
            {
                get { return false; }
            }

            public override SpecialType specialType
            {
                get
                {
                    return SpecialType.Enum;
                }
            }

            public override string LanguageKeyword
            {
                get { return enumLanguageKeyword; }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    /*"bool",
                    "boolean",
                    "system.boolean"*/
                };
            }

            public void SetLanguageKeyword(string languageKeyword)
            {
                this.enumLanguageKeyword = languageKeyword;
            }
            
            public override Enum Parse(string value)
            {
                return null;
            }

            public override void WriteToStream(BinaryWriter stream, string value)
            {
                if (string.IsNullOrEmpty(value))
                {
                    stream.Write(DataTableGeneratorEnumCode.GetEnumDefaultValue(enumLanguageKeyword));
                }
                else
                {
                    stream.Write(value);
                }
            }
        }
    }
}