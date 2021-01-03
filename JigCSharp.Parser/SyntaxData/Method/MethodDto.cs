namespace JigCSharp.Parser.SyntaxData.Method
{
    public class MethodDto
    {
        public string Name { get; }
        public string DisplayName { get; }
        public string Modifier { get; }
        public string ReturnType { get; }

        public MethodDto(string name, string displayName, string modifier, string returnType )
        {
            Name = name;
            DisplayName = displayName;
            Modifier = modifier;
            ReturnType = returnType;
        }
    }
}