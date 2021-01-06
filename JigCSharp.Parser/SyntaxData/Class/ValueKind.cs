using JigCSharp.Parser.SyntaxData.Common;

namespace JigCSharp.Parser.SyntaxData.Class
{
    public class ValueKind : Enumeration
    {
        public static readonly ValueKind Enumeration = new ValueKind(1, "列挙型");
        public static readonly ValueKind ValueObject = new ValueKind(2, "ValueObject型");
        public static readonly ValueKind Exception = new ValueKind(3, "例外型");
        public static readonly ValueKind None = new ValueKind(9, "None");
        public ValueKind(int id, string name) : base(id, name)
        {
        }
    }
}