using ActiveMesa.R2P.FSharp.Psi.FSharp.Parsing;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Text;

namespace JetBrains.ReSharper.Psi.FSharp.Parsing
{
  public class FSharpLexer : FSharpLexerGenerated
  {
    public FSharpLexer(IBuffer buffer) : base(buffer)
    {
    }

    public static string GetTokenText(TokenNodeType token)
    {
      return GetKeywordTextByTokenType(token);
    }

    protected static string GetKeywordTextByTokenType(NodeType tokenType)
    {
      return tokenTypesToText[tokenType];
    }
  }
}