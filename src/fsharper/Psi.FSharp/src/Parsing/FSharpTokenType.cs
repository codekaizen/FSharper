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
      private readonly string representation;

      public GenericTokenNodeType(string s, string representation = null)
        : base(s)
      {
        this.representation = representation;
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

    private class FixedTokenNodeType : GenericTokenNodeType
    {
      private readonly string representation;

      public FixedTokenNodeType(string s, string representation = null) : base(s, representation)
      {
        this.representation = representation;
      }

      public override LeafElementBase Create(IBuffer buffer, TreeOffset startOffset, TreeOffset endOffset)
      {
        return new FixedTokenElement(this);
      }

      public override string TokenRepresentation
      {
        get { return representation; }
      }
    }

    private class FixedTokenElement : FSharpTokenBase
    {
      private readonly FixedTokenNodeType keywordTokenNodeType;

      public FixedTokenElement(FixedTokenNodeType keywordTokenNodeType)
      {
        this.keywordTokenNodeType = keywordTokenNodeType;
      }

      public override int GetTextLength()
      {
        return keywordTokenNodeType.TokenRepresentation.Length;
      }

      public override string GetText()
      {
        return keywordTokenNodeType.TokenRepresentation;
      }

      public override NodeType NodeType
      {
        get { return keywordTokenNodeType; }
      }
    }

    private abstract class KeywordTokenNodeType : FixedTokenNodeType
    {
      public KeywordTokenNodeType(string s, string representation) : base(s, representation) { }
    }

    static FSharpTokenType()
    {
      KEYWORDS = new NodeTypeSet
      (
        ABSTRACT_KEYWORD,
        AS_KEYWORD,
        BASE_KEYWORD,
        CLASS_KEYWORD,
        DEFAULT_KEYWORD,
        DELEGATE_KEYWORD,
        DO_KEYWORD,
        ELSE_KEYWORD,
        EXTERN_KEYWORD,
        FALSE_KEYWORD,
        FINALLY_KEYWORD,
        FOR_KEYWORD,
        IF_KEYWORD,
        IN_KEYWORD,
        INTERFACE_KEYWORD,
        INTERNAL_KEYWORD,
        NAMESPACE_KEYWORD,
        NEW_KEYWORD,
        NULL_KEYWORD,
        OVERRIDE_KEYWORD,
        PRIVATE_KEYWORD,
        PUBLIC_KEYWORD,
        RETURN_KEYWORD,
        STATIC_KEYWORD,
        STRUCT_KEYWORD,
        TRUE_KEYWORD,
        TRY_KEYWORD,
        VOID_KEYWORD,
        WHILE_KEYWORD,
        YIELD_KEYWORD,
        LET_KEYWORD
      );

      IDENTIFIER_KEYWORDS = new NodeTypeSet
      (
        YIELD_KEYWORD,
        LET_KEYWORD
      );

      TYPE_KEYWORDS = new NodeTypeSet
      (
        VOID_KEYWORD
      );

      LITERALS = new NodeTypeSet
      (
        STRING_LITERAL,
        CHARACTER_LITERAL,
        TRUE_KEYWORD,
        FALSE_KEYWORD,
        NULL_KEYWORD
      );

      PREPROCESSOR = new NodeTypeSet
      (
        PP_BAD_CHARACTER,
        PP_CONDITIONAL_SYMBOL,
        PP_SKIPPED_LINE,
        PP_DEC_DIGITS,
        PP_FILENAME,
        PP_DEFAULT,
        PP_HIDDEN,
        PP_MESSAGE,
        PP_BAD_DIRECTIVE
      );
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