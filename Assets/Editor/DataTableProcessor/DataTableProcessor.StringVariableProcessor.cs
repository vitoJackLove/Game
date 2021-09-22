using System;
using System.IO;

namespace GameFramework.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class StringVariableProcessor : GenericDataProcessor<string>
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
                    return SpecialType.StringVariable;
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
            
            public override string Parse(string value)
            {
                return null;
            }

            public override void WriteToStream(BinaryWriter stream, string value)
            {
                value = value.Insert(0, "[").Insert(value.Length, "]");
                stream.Write(value);
            }
        }
    }
}