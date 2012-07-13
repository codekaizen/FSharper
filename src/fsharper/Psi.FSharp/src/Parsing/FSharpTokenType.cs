using System;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.FSharp.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Text;
using JetBrains.Util;

namespace JetBrains.ReSharper.Psi.FSharp.Parsing
{
  public static partial class FSharpTokenType
  {
    private abstract class FSharpTokenNodeType : TokenNodeType
    {
      protected FSharpTokenNodeType(string s)
        : base(s)
      {
      }

      public override LeafElementBase Create(IBuffer buffer, TreeOffset startOffset, TreeOffset endOffset)
      {
        throw new InvalidOperationException();
      }

      public override bool IsWhitespace
      {
        get { return this == WHITE_SPACE || this == NEW_LINE; }
      }

      public override bool IsComment
      {
        get { return false; }
      }

      public override bool IsStringLiteral
      {
        get { return this == STRING_LITERAL; }
      }

      public override bool IsConstantLiteral
      {
        get { return LITERALS[this]; }
      }

      public override bool IsIdentifier
      {
        get { return this == IDENTIFIER; }
      }

      public override bool IsKeyword
      {
        get { return KEYWORDS[this]; }
      }
    }

    private sealed class WhitespaceNodeType : FSharpTokenNodeType
    {
      public WhitespaceNodeType() : base("WHITE_SPACE") { }

      public override LeafElementBase Create(IBuffer buffer, TreeOffset startOffset, TreeOffset endOffset)
      {
        return new Whitespace(buffer.GetText(new TextRange(startOffset.Offset, endOffset.Offset)));
      }

      public override string TokenRepresentation
      {
        get { throw new NotImplementedException(); }
      }
    }

    private sealed class NewLineNodeType : FSharpTokenNodeType
    {
      public NewLineNodeType() : base("NEW_LINE") { }

      public override LeafElementBase Create(IBuffer buffer, TreeOffset startOffset, TreeOffset endOffset)
      {
        return new NewLine(buffer.GetText(new TextRange(startOffset.Offset, endOffset.Offset)));
      }

      public override string TokenRepresentation
      {
        get { throw new NotImplementedException(); }
      }
    }

    private class GenericTokenNodeType : FSharpTokenNodeType
    {
      private readonly string presentation;

      public GenericTokenNodeType(string s, string presentation = null)
        : base(s)
      {
        this.presentation = presentation;
      }

      public override string TokenRepresentation
      {
        get { throw new NotImplementedException(); }
      }
    }

    private class CommentNodeType : GenericTokenNodeType
    {
      public CommentNodeType(string s) : base(s) { }

      public override LeafElementBase Create(IBuffer buffer, TreeOffset startOffset, TreeOffset endOffset)
      {
        return new Comment(this, buffer.GetText(new TextRange(startOffset.Offset, endOffset.Offset)));
      }

      public override bool IsComment
      {
        get { return true; }
      }
    }

    private sealed class EndOfLineCommentNodeType : CommentNodeType
    {
      public EndOfLineCommentNodeType()
        : base("END_OF_LINE_COMMENT")
      {
      }

      public override LeafElementBase Create(IBuffer buffer, TreeOffset startOffset, TreeOffset endOffset)
      {
        if (endOffset - startOffset > 2 && buffer[startOffset.Offset + 2] == '/' &&
            (endOffset - startOffset == 3 || buffer[startOffset.Offset + 3] != '/'))
        {
          return new DocComment(this, buffer.GetText(new TextRange(startOffset.Offset, endOffset.Offset)));
        }
        return new Comment(this, buffer.GetText(new TextRange(startOffset.Offset, endOffset.Offset)));
      }
    }

    private sealed class IdentifierNodeType : FSharpTokenNodeType
    {
      public IdentifierNodeType() : base("IDENTIFIER") { }

      public override LeafElementBase Create(IBuffer buffer, TreeOffset startOffset, TreeOffset endOffset)
      {
        return new Identifier(buffer.GetText(new TextRange(startOffset.Offset, endOffset.Offset)));
      }

      public override string TokenRepresentation
      {
        get { throw new NotImplementedException(); }
      }
    }

    private abstract class KeywordTokenNodeType : FSharpTokenNodeType
    {
      protected KeywordTokenNodeType(string s) : base(s) { }
    }

    // parser skippable
    public static readonly TokenNodeType WHITE_SPACE = new WhitespaceNodeType();
    public static readonly TokenNodeType NEW_LINE = new NewLineNodeType();
    public static readonly TokenNodeType END_OF_LINE_COMMENT = new EndOfLineCommentNodeType();
    public static readonly TokenNodeType C_STYLE_COMMENT = new CommentNodeType("C_STYLE_COMMENT");

    // parser non-skippable
    public static readonly TokenNodeType IDENTIFIER = new IdentifierNodeType();

    public static readonly TokenNodeType INT_LITERAL = new GenericTokenNodeType("INT_LITERAL", "int literal");
    public static readonly TokenNodeType CHARACTER_LITERAL = new GenericTokenNodeType("CHARACTER_LITERAL", "char literal");
    public static readonly TokenNodeType STRING_LITERAL = new GenericTokenNodeType("STRING_LITERAL", "string literal");

    public static readonly TokenNodeType BAD_CHARACTER = new GenericTokenNodeType("BAD_CHARACTER");
    public static readonly TokenNodeType CHAMELEON = new GenericTokenNodeType("CHAMELEON");

    public static readonly TokenNodeType PP_BAD_CHARACTER = new GenericTokenNodeType("PP_BAD_CHARACTER");
    public static readonly TokenNodeType PP_SKIPPED_LINE = new GenericTokenNodeType("PP_SKIPPED_LINE");
    public static readonly TokenNodeType PP_DEC_DIGITS = new GenericTokenNodeType("PP_DEC_DIGITS");
    public static readonly TokenNodeType PP_FILENAME = new GenericTokenNodeType("PP_FILENAME");
    public static readonly TokenNodeType PP_DEFAULT = new GenericTokenNodeType("PP_DEFAULT");
    public static readonly TokenNodeType PP_MESSAGE = new GenericTokenNodeType("PP_MESSAGE");
    public static readonly TokenNodeType PP_HIDDEN = new GenericTokenNodeType("PP_HIDDEN");
    public static readonly TokenNodeType PP_CONDITIONAL_SYMBOL = new GenericTokenNodeType("PP_CONDITIONAL_SYMBOL");
    public static readonly TokenNodeType PP_BAD_DIRECTIVE = new GenericTokenNodeType("PP_BAD_DIRECTIVE");

    /// <summary>
    /// Special token for some implementation details.
    /// It should never be returned from the F# lexer.
    /// </summary>
    public static readonly TokenNodeType EOF = new GenericTokenNodeType("EOF");

    public static readonly NodeTypeSet KEYWORDS;
    public static readonly NodeTypeSet TYPE_KEYWORDS;
    public static readonly NodeTypeSet IDENTIFIER_KEYWORDS;
    public static readonly NodeTypeSet LITERALS;
    public static readonly NodeTypeSet PREPROCESSOR;
  }
}