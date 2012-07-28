using JetBrains.ReSharper.Psi.CSharp.Impl.Resolve;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.FSharp.Parsing;

namespace JetBrains.ReSharper.Psi.FSharp.Tree
{
  internal class Identifier : FSharpTokenBase, IFSharpIdentifier
  {
    private readonly string myText = null;

    public Identifier(string text)
    {
      myText = text;
    }

    public string Name
    { // todo: verify it's ok to use c#
      get { return CSharpResolveUtil.ReferenceName(myText); }
    }

    public override NodeType NodeType
    {
      get { return FSharpTokenType.IDENTIFIER; }
    }

    public override PsiLanguageType Language
    {
      get { return FSharpLanguage.Instance; }
    }

    public override int GetTextLength()
    {
      return myText.Length;
    }

    public override string GetText()
    {
      return myText;
    }
  }
}