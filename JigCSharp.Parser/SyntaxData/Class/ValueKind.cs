using JigCSharp.Parser.SyntaxData.Common;

namespace JigCSharp.Parser.SyntaxData.Class
{
    public class ValueKind : Enumeration
    {
        public static ValueKind Enumeration = new ValueKind(1, "列挙型");
        public static ValueKind ValueObject = new ValueKind(2, "ValueObject型");
        public static ValueKind None = new ValueKind(9, "None");
        public ValueKind(int id, string name) : base(id, name)
        {
        }
    }
}