using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public class CSharpCodeParser : CSharpSyntaxWalker
    {
        private SemanticModel _semanticModel;
        
        private NamespaceDataList _namespaceDataList;

        private NamespaceData _currentNamespaceData;
        private ClassOrInterfaceData _currentClassOrInterfaceData;
        private MethodData _currentMethodData;

        public CSharpCodeParser()
        {
            _namespaceDataList = new NamespaceDataList();
        }

        public NamespaceDataList Generate(Stream stream)
        {
            var tree = CSharpSyntaxTree.ParseText(SourceText.From(stream));
            var mscorlib = MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location);
            var compilation = CSharpCompilation.Create("tempcompilation", syntaxTrees: new[] {tree}, new []{mscorlib});
            _semanticModel = compilation.GetSemanticModel(tree);
            var root = tree.GetRoot();

            Visit(root);

            return _namespaceDataList;
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            _currentNamespaceData = new NamespaceData(new DeclarationName(node.Name.ToString(), ""));

            base.VisitNamespaceDeclaration(node);

            _namespaceDataList.Add(_currentNamespaceData);
        }

        /// <summary>
        /// クラス宣言の解析
        /// </summary>
        /// <param name="node"></param>
        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var comment = GetComment(node);

            var classAttributeKind = GetClassBelongedLayerKind(node);

            var baseClasses = GetBaseClass(node);

            _currentClassOrInterfaceData = new ClassOrInterfaceData(new DeclarationName(node.Identifier.Text, comment.Summary),
                ClassOrInterfaceType.Class, baseClasses, classAttributeKind);

            base.VisitClassDeclaration(node);

            _currentNamespaceData.AddClassData(_currentClassOrInterfaceData);
        }

        /// <summary>
        /// クラス属性からどの層に対応しているか取得
        /// </summary>
        /// <param name="node">クラスノード</param>
        /// <returns></returns>
        private ClassBelongedLayerKind GetClassBelongedLayerKind(TypeDeclarationSyntax node)
        {
            var attributes = node.AttributeLists.SelectMany(x => x.Attributes);
            var returnAttributes = new List<string>();
            foreach (var attribute in attributes)
            {
                returnAttributes.Add(_semanticModel.GetTypeInfo(attribute).Type.ToDisplayString());
            }

            if (returnAttributes.Exists(x => x == "CONTROLLER"))
            {
                return ClassBelongedLayerKind.CONTROLLER;
            }

            if (returnAttributes.Exists(x => x == "SERVICE"))
            {
                return ClassBelongedLayerKind.SERVICE;
            }

            if (returnAttributes.Exists(x => x == "REPOSITORY"))
            {
                return ClassBelongedLayerKind.REPOSITORY;
            }
            
            return ClassBelongedLayerKind.NONE;
        }

        private BaseTypeList GetBaseClass(ClassDeclarationSyntax node)
        {
            var baseTypes = new List<BaseType>();
            if (node.BaseList == null)
            {
                return new BaseTypeList(baseTypes);
            }
            
            foreach (var baseTypeSyntax in node.BaseList.Types)
            {
                baseTypes.Add(new BaseType(baseTypeSyntax.Type.Parent.ToString()));
            }

            return new BaseTypeList(baseTypes);
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            var comment = GetComment(node);

            var classBelongedLayerKind = GetClassBelongedLayerKind(node);

            _currentClassOrInterfaceData = new ClassOrInterfaceData(
                new DeclarationName(node.Identifier.Text, comment.Summary), ClassOrInterfaceType.Interface,
                BaseTypeList.Empty(), classBelongedLayerKind);

            base.VisitInterfaceDeclaration(node);

            _currentNamespaceData.AddClassData(_currentClassOrInterfaceData);

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
            _currentClassOrInterfaceData.AddProperty(property);
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

                _currentClassOrInterfaceData.AddProperty(field);
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

            _currentClassOrInterfaceData.AddMethod(_currentMethodData);
            
        }
    }
}