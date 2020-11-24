namespace JigCSharp.Parser.SyntaxData.Common
{
    public class DeclarationName : ValueObject<DeclarationName>
    {
        public string Name { get; }
        public  string _displayName { get; }

        public string DisplayName => !string.IsNullOrEmpty(_displayName) ? _displayName : Name;

        public DeclarationName(string name, string displayName)
        {
            Name = name;
            _displayName = displayName;
        }

        protected override bool EqualsCore(DeclarationName other)
        {
            return this.Name == other.Name;
        }
    }
}
