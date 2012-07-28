using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.FSharp.Parsing;

namespace JetBrains.ReSharper.Psi.FSharp.Tree
{
  internal class Whitespace : WhitespaceBase
  {
    public Whitespace(string text)
      : base(text)
    {
    }

    public override NodeType NodeType
    {
      get { return FSharpTokenType.WHITE_SPACE; }
    }

    public override PsiLanguageType Language
    {
      get { return FSharpLanguage.Instance; }
    }

    public override bool IsNewLine
    {
      get { return false; }
    }
  }
}