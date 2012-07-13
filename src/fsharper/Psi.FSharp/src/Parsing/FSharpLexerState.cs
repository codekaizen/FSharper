using JetBrains.ReSharper.Psi.Parsing;

namespace JetBrains.ReSharper.Psi.FSharp.Parsing
{
  public struct FSharpLexerState
  {
    public TokenNodeType currTokenType;
    public int yy_buffer_index;
    public int yy_buffer_start;
    public int yy_buffer_end;
    public int yy_lexical_state;
  }
}