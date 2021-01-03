using JigCSharp.Parser.SyntaxData.Common;

namespace JigCSharp.Parser.SyntaxData.Class
{
    /// <summary>
    /// クラスがどのレイヤに所属しているか
    /// </summary>
    public class ClassBelongedLayerKind : Enumeration
    {
        public static ClassBelongedLayerKind CONTROLLER = new ClassBelongedLayerKind(1, "コントローラ");
        public static ClassBelongedLayerKind SERVICE = new ClassBelongedLayerKind(2, "サービス");
        public static ClassBelongedLayerKind REPOSITORY = new ClassBelongedLayerKind(3, "リポジトリ");
        public static ClassBelongedLayerKind NONE = new ClassBelongedLayerKind(9, "なし");
        public ClassBelongedLayerKind(int id, string name) : base(id, name)
        {
        }
    }
}