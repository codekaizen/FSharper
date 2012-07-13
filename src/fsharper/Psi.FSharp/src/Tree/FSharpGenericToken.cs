using ActiveMesa.R2P.FSharp.Psi.FSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;

namespace JetBrains.ReSharper.Psi.FSharp.Tree
{
  public class FSharpGenericToken : FSharpTokenBase
  {
    private readonly TokenNodeType myNodeType;
    private readonly string myText;

    public FSharpGenericToken(TokenNodeType nodeType, string text)
    {
      myNodeType = nodeType;
      myText = text;
    }

    public override NodeType NodeType
    {
      get { return myNodeType; }
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