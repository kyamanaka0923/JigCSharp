using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.IO;
using JigCSharp.Parser.SyntaxData.Class;
using JigCSharp.Parser.SyntaxData.Common;
using JigCSharp.Parser.SyntaxData.Method;
using JigCSharp.Parser.SyntaxData.Namespace;
using JigCSharp.Parser.SyntaxData.Property;
using JigCSharp.Parser.SyntaxData.Type;

namespace JigCSharp.Parser
{
    /// <summary>
    /// パーサー
    /// </summary>
    public class ClassParser : CSharpSyntaxWalker
    {
        private SemanticModel _semanticModel;

        private NamespaceDataList _namespaceDataList;

        private NamespaceData _currentNamespaceData;
        private ClassData _currentClassData;
        private MethodData _currentMethodData;

        public ClassParser()
        {
            _namespaceDataList = new NamespaceDataList();
        }

        public void Generate(Stream stream)
        {
            var tree = CSharpSyntaxTree.ParseText(SourceText.From(stream));
            var compilation = CSharpCompilation.Create("tempcompilation", syntaxTrees: new[] {tree});
            _semanticModel = compilation.GetSemanticModel(compilation.SyntaxTrees[0], true);
            var root = tree.GetRoot();

            Visit(root);

            _namespaceDataList.Display();
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            _currentNamespaceData = new NamespaceData(new DeclarationName(node.Name.ToString(), ""));

            base.VisitNamespaceDeclaration(node);

            _namespaceDataList = _namespaceDataList.Add(_currentNamespaceData);
        }

        /// <summary>
        /// クラス宣言の解析
        /// </summary>
        /// <param name="node"></param>
        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var comment = GetComment(node);


            _currentClassData = new ClassData(new DeclarationName(node.Identifier.Text, comment.Summary));

            base.VisitClassDeclaration(node);

            _currentNamespaceData.AddClassData(_currentClassData);
        }

        private XmlDocument GetComment(SyntaxNode node)
        {

            var noComment = XmlDocument.Parse("");

            var methodSymbol = _semanticModel.GetDeclaredSymbol(node);
            if (methodSymbol is null)
            {
                return noComment;
            }
            // コメントを取得
            var xmlComment = methodSymbol.GetDocumentationCommentXml();
            if (string.IsNullOrEmpty(xmlComment))
            {
                return noComment;
            }

            var comment = XmlDocument.Parse(xmlComment);

            return comment;
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var comment = GetComment(node);
            var property = new PropertyAndFieldData(new DeclarationName(node.Identifier.Text, comment.Summary),
                new TypeData(node.Type.ToString()));
            _currentClassData.AddProperty(property);
            base.VisitPropertyDeclaration(node);
        }

        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            var variables = node.Declaration.Variables;
            var type = node.Declaration.Type.ToString();
            var comment = GetComment(node);
            foreach (var variable in variables)
            {
                var field = new PropertyAndFieldData(new DeclarationName(variable.Identifier.Text, comment.Summary), new TypeData(type));

                _currentClassData.AddProperty(field);
            }
            base.VisitFieldDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var type = node.ReturnType.ToString();

            var keyword = "";
            foreach (var modifier in node.Modifiers)
            {
                switch (modifier.Kind())
                {
                    case SyntaxKind.PublicKeyword:
                        keyword = keyword + "+";
                        break;
                    case SyntaxKind.PrivateKeyword:
                        keyword = keyword + "-";
                        break;
                }
            }

            _currentMethodData = new MethodData(new DeclarationName(node.Identifier.ToString(), ""), keyword, new TypeData(type));

            base.VisitMethodDeclaration(node);

            _currentClassData.AddMethod(_currentMethodData);
            
        }
    }
}