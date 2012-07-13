using ActiveMesa.R2P.FSharp.Psi.FSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace JetBrains.ReSharper.Psi.FSharp.Tree
{
  internal class NewLine : WhitespaceBase
  {
    public NewLine(string text)
      : base(text)
    {
    }

    public override NodeType NodeType
    {
      get { return FSharpTokenType.NEW_LINE; }
    }

    public override PsiLanguageType Language
    {
      get { return FSharpLanguage.Instance; }
    }

    public override bool IsNewLine
    {
      get { return true; }
    }
  }

  public enum CommentType : byte
  {
    END_OF_LINE_COMMENT, // example: //
    MULTILINE_COMMENT,   // example: (* *)
    DOC_COMMENT          // example: ///
  }

  public interface IFSharpCommentNode : ICommentNode
  {
    CommentType CommentType { get; }
  }

  public interface IDocCommentNode : IFSharpCommentNode
  {
    IDocCommentNode ReplaceBy(IDocCommentNode docCommentNode);
  }
}