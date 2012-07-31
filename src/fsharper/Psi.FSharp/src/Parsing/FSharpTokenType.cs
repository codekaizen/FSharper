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

      public FixedTokenNodeType(string s, string representation = null)
        : base(s, representation)
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
        AND_KEYWORD,
        AS_KEYWORD,
        ASSERT_KEYWORD,
        BASE_KEYWORD,
        BEGIN_KEYWORD,
        CLASS_KEYWORD,
        DEFAULT_KEYWORD,
        DELEGATE_KEYWORD,
        DO_KEYWORD,
        DONE_KEYWORD,
        DOWNCAST_KEYWORD,
        DOWNTO_KEYWORD,
        ELIF_KEYWORD,
        ELSE_KEYWORD,
        END_KEYWORD,
        EXCEPTION_KEYWORD,
        EXTERN_KEYWORD,
        FALSE_KEYWORD,
        FINALLY_KEYWORD,
        FOR_KEYWORD,
        FUN_KEYWORD,
        FUNCTION_KEYWORD,
        GLOBAL_KEYWORD,
        IF_KEYWORD,
        IN_KEYWORD,
        INHERIT_KEYWORD,
        INLINE_KEYWORD,
        INTERFACE_KEYWORD,
        INTERNAL_KEYWORD,
        LAZY_KEYWORD,
        LET_KEYWORD,
        MATCH_KEYWORD,
        MEMBER_KEYWORD,
        MODULE_KEYWORD,
        MUTABLE_KEYWORD,
        NAMESPACE_KEYWORD,
        NEW_KEYWORD,
        NOT_KEYWORD,
        NULL_KEYWORD,
        OF_KEYWORD,
        OPEN_KEYWORD,
        OR_KEYWORD,
        OVERRIDE_KEYWORD,
        PRIVATE_KEYWORD,
        PUBLIC_KEYWORD,
        REC_KEYWORD,
        RETURN_KEYWORD,
        STATIC_KEYWORD,
        STRUCT_KEYWORD,
        THEN_KEYWORD,
        TO_KEYWORD,
        TRUE_KEYWORD,
        TRY_KEYWORD,
        TYPE_KEYWORD,
        UPCAST_KEYWORD,
        USE_KEYWORD,
        VAL_KEYWORD,
        VOID_KEYWORD,
        WHEN_KEYWORD,
        WHILE_KEYWORD,
        WITH_KEYWORD,
        YIELD_KEYWORD,

        // ML keywords
        ASR_ML_KEYWORD,
        LAND_ML_KEYWORD,
        LOR_ML_KEYWORD,
        LSL_ML_KEYWORD,
        LSR_ML_KEYWORD,
        LXOR_ML_KEYWORD,
        MOD_ML_KEYWORD,
        SIG_ML_KEYWORD,

        // reserved
        ATOMIC_RESERVED_KEYWORD,
        BREAK_RESERVED_KEYWORD,
        CHECKED_RESERVED_KEYWORD,
        COMPONENT_RESERVED_KEYWORD,
        CONST_RESERVED_KEYWORD,
        CONSTRAINT_RESERVED_KEYWORD,
        CONSTRUCTOR_RESERVED_KEYWORD,
        CONTINUE_RESERVED_KEYWORD,
        EAGER_RESERVED_KEYWORD,
        EVENT_RESERVED_KEYWORD,
        EXTERNAL_RESERVED_KEYWORD,
        FIXED_RESERVED_KEYWORD,
        FUNCTOR_RESERVED_KEYWORD,
        INCLUDE_RESERVED_KEYWORD,
        METHOD_RESERVED_KEYWORD,
        MIXIN_RESERVED_KEYWORD,
        OBJECT_RESERVED_KEYWORD,
        PARALLEL_RESERVED_KEYWORD,
        PROCESS_RESERVED_KEYWORD,
        PROTECTED_RESERVED_KEYWORD,
        PURE_RESERVED_KEYWORD,
        SEALED_RESERVED_KEYWORD,
        TAILCALL_RESERVED_KEYWORD,
        TRAIT_RESERVED_KEYWORD,
        VIRTUAL_RESERVED_KEYWORD,
        VOLATILE_RESERVED_KEYWORD
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
    
    public static readonly TokenNodeType NEW_LINE = new NewLineNodeType();
    public static readonly TokenNodeType END_OF_LINE_COMMENT = new EndOfLineCommentNodeType();
    public static readonly TokenNodeType C_STYLE_COMMENT = new CommentNodeType("C_STYLE_COMMENT");

    // parser non-skippable
    public static readonly TokenNodeType WHITE_SPACE = new WhitespaceNodeType();
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
    // todo: figure out what these are actually for
    public static readonly NodeTypeSet IDENTIFIER_KEYWORDS;
    public static readonly NodeTypeSet LITERALS;
    public static readonly NodeTypeSet PREPROCESSOR;
  }
}