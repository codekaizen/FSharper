namespace JetBrains.ReSharper.Psi.FSharp.Parsing
{
  using System.Collections.Generic;
  using ExtensionsAPI.Tree;
  using Psi.Parsing;
  using Psi.Tree;

  internal class FSharpParser : /* FSharpParserGenerated, */ IFSharpParser
  {
    private ILexer<int> originalLexer;
    private LexerTokenIntern lexerTokenIntern;

    public FSharpParser(ILexer<int> lexer, IEnumerable<PreProcessingDirective> defines)
    {
      originalLexer = lexer;
      lexerTokenIntern = new LexerTokenIntern();
    }

    IFile IParser.ParseFile()
    {
      throw new System.NotImplementedException("Not ready yet");
    }

    IFile IFSharpParser.ParseFile()
    {
      throw new System.NotImplementedException("Not ready yet");
    }
  }
}