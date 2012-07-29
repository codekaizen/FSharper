namespace JetBrains.ReSharper.Psi.FSharp.Parsing
{
  using ExtensionsAPI.Tree;
  using Psi.Parsing;
  using Text;

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