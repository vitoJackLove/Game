using System.IO;

namespace GameFramework.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class StringProcessor : GenericDataProcessor<string>
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
                    return "string";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "string",
                    "system.string"
                };
            }

            public override string Parse(string value)
            {
                return value;
            }

            public override void WriteToStream(BinaryWriter stream, string value)
            {
                /*//Intention的变量需要Json格式 策划懒得填这个括号 
                if (value.StartsWith("[") && value.EndsWith("]"))
                {
                    value = value.Insert(0, "[").Insert(value.Length,"]");
                }*/
                stream.Write(Parse(value));
            }
        }
    }
}
