namespace JetBrains.ReSharper.Psi.FSharp.Parsing
{
  using System.Collections.Generic;
  using ExtensionsAPI.Tree;
  using Psi.Parsing;
  using Psi.Tree;

  internal class FSharpParser : FSharpParserGenerated, IFSharpParser
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
      throw new System.NotImplementedException();
    }

    IFile IFSharpParser.ParseFile()
    {
      throw new System.NotImplementedException();
    }

    public override TreeElement parseBindingSource()
    {
      throw new System.NotImplementedException();
    }

    public override TreeElement parseBindingValue(TreeElement value)
    {
      throw new System.NotImplementedException();
    }

    public override TreeElement parseIdentifier()
    {
      throw new System.NotImplementedException();
    }

    public override TreeElement parseModuleReference()
    {
      throw new System.NotImplementedException();
    }

    public override TreeElement parseQualifiedIdentifierName(TreeElement qualifier)
    {
      throw new System.NotImplementedException();
    }

    public override TreeElement parseQualifiedValueName()
    {
      throw new System.NotImplementedException();
    }
  }
}