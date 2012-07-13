using JetBrains;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Text;
using JetBrains.Util;
using System.Collections;

%%

%unicode

%namespace JetBrains.ReSharper.Psi.FSharp.Parsing
%class FSharpLexerGenerated
%public
%implements ILexer
%function _locateToken
%type TokenNodeType
%eofval{ 
  currTokenType = null; return currTokenType;
%eofval}

NULL_CHAR=\u0000
CARRIAGE_RETURN_CHAR=\u000D
LINE_FEED_CHAR=\u000A
NEW_LINE_PAIR={CARRIAGE_RETURN_CHAR}{LINE_FEED_CHAR}
NEW_LINE_CHAR=({CARRIAGE_RETURN_CHAR}|{LINE_FEED_CHAR}|(\u0085)|(\u2028)|(\u2029))
NOT_NEW_LINE=([^\u0085\u2028\u2029\u000D\u000A])
NOT_NEW_LINE_NUMBER_WS=([^\#\u0085)\u2028\u2029\n\r\ \t\f\u0009\u000B\u000C])


INPUT_CHARACTER={NOT_NEW_LINE}
ASTERISKS="*"+

%include ../../../../../tools/UnicodeGroupGenerator/Unicode.lex

WHITE_SPACE_CHAR=({UNICODE_ZS}|(\u0009)|(\u000B)|(\u000C)|(\u200B)|(\uFEFF)|{NULL_CHAR})

DELIMITED_COMMENT_SECTION=([^\*]|({ASTERISKS}[^\*\/]))

UNFINISHED_DELIMITED_COMMENT="/*"{DELIMITED_COMMENT_SECTION}*

DELIMITED_COMMENT={UNFINISHED_DELIMITED_COMMENT}{ASTERISKS}"/"
SINGLE_LINE_COMMENT=("//"{INPUT_CHARACTER}*)


DECIMAL_DIGIT=[0-9]
HEX_DIGIT=({DECIMAL_DIGIT}|[A-Fa-f])
OCT_DIGIT=[0-7]
BIN_DIGIT=[0-1]
INTEGER_TYPE_SUFFIX=([UuLl]|UL|Ul|uL|ul|LU|lU|Lu|lu)


DECIMAL_INTEGER_LITERAL=({DECIMAL_DIGIT}+{INTEGER_TYPE_SUFFIX}?)
HEXADECIMAL_INTEGER_LITERAL=(0[Xx]({HEX_DIGIT})*{INTEGER_TYPE_SUFFIX}?)
INTEGER_LITERAL=({DECIMAL_INTEGER_LITERAL}|{HEXADECIMAL_INTEGER_LITERAL})

EXPONENT_PART=([eE](([+-])?({DECIMAL_DIGIT})*))
REAL_TYPE_SUFFIX=[FfDdMm]
REAL_LITERAL=({DECIMAL_DIGIT}*"."{DECIMAL_DIGIT}+({EXPONENT_PART})?{REAL_TYPE_SUFFIX}?)|({DECIMAL_DIGIT}+({EXPONENT_PART}|({EXPONENT_PART}?{REAL_TYPE_SUFFIX})))

SINGLE_CHARACTER=[^\'\\\u0085\u2028\u2029\u000D\u000A]
SIMPLE_ESCAPE_SEQUENCE=(\\[\'\"\\0abfnrtv])
HEXADECIMAL_ESCAPE_SEQUENCE=(\\x{HEX_DIGIT}({HEX_DIGIT}|{HEX_DIGIT}{HEX_DIGIT}|{HEX_DIGIT}{HEX_DIGIT}{HEX_DIGIT})?)
UNICODE_ESCAPE_SEQUENCE=((\\u{HEX_DIGIT}{HEX_DIGIT}{HEX_DIGIT}{HEX_DIGIT})|(\\U{HEX_DIGIT}{HEX_DIGIT}{HEX_DIGIT}{HEX_DIGIT}{HEX_DIGIT}{HEX_DIGIT}{HEX_DIGIT}{HEX_DIGIT}))
CHARACTER=({SINGLE_CHARACTER}|{SIMPLE_ESCAPE_SEQUENCE}|{HEXADECIMAL_ESCAPE_SEQUENCE}|{UNICODE_ESCAPE_SEQUENCE})
BAD_ESCAPE_SEQUENCE=((\\u)|(\\[^\'\"\\0abfnrtv]))
CHARACTER_LITERAL=\'({CHARACTER})\'
UNFINISHED_CHARACTER_LITERAL=\'(({CHARACTER})|({BAD_ESCAPE_SEQUENCE}(\'?))|\')
EXCEEDING_CHARACTER_LITERAL=\'{CHARACTER}({CHARACTER}|{BAD_ESCAPE_SEQUENCE})+\'

DECIMAL_DIGIT_CHARACTER={UNICODE_ND}
CONNECTING_CHARACTER={UNICODE_PC}
COMBINING_CHARACTER=({UNICODE_MC}|{UNICODE_MN})
FORMATTING_CHARACTER={UNICODE_CF}
LETTER_CHARACTER=({UNICODE_LL}|{UNICODE_LM}|{UNICODE_LO}|{UNICODE_LT}|{UNICODE_LU}|{UNICODE_NL}|{UNICODE_ESCAPE_SEQUENCE})

IDENTIFIER_START_CHARACTER=({LETTER_CHARACTER}|(\u005F))
IDENTIFIER_PART_CHARACTER=({LETTER_CHARACTER}|{DECIMAL_DIGIT_CHARACTER}|{CONNECTING_CHARACTER}|{COMBINING_CHARACTER}|{FORMATTING_CHARACTER})
IDENTIFIER=("@"?{IDENTIFIER_START_CHARACTER}{IDENTIFIER_PART_CHARACTER}*)


REGULAR_STRING_LITERAL_CHARACTER=({SINGLE_REGULAR_STRING_LITERAL_CHARACTER}|{SIMPLE_ESCAPE_SEQUENCE}|{HEXADECIMAL_ESCAPE_SEQUENCE}|{UNICODE_ESCAPE_SEQUENCE})
SINGLE_REGULAR_STRING_LITERAL_CHARACTER=[^\"\\\u0085\u2028\u2029\u000D\u000A]
REGULAR_STRING_LITERAL=(\"{REGULAR_STRING_LITERAL_CHARACTER}*\")

VERBATIM_STRING_LITERAL=(\@\"{VERBATIM_STRING_LITERAL_CHARACTER}*\")
VERBATIM_STRING_LITERAL_CHARACTER=({SINGLE_VERBATIM_STRING_LITERAL_CHARACTER}|{QUOTE_ESCAPE_SEQUENCE})
SINGLE_VERBATIM_STRING_LITERAL_CHARACTER=[^\"]
QUOTE_ESCAPE_SEQUENCE=(\"\")

STRING_LITERAL=({REGULAR_STRING_LITERAL}|{VERBATIM_STRING_LITERAL})
UNFINISHED_REGULAR_STRING_LITERAL=(\"{REGULAR_STRING_LITERAL_CHARACTER}*)
UNFINISHED_VERBATIM_STRING_LITERAL=(@\"{VERBATIM_STRING_LITERAL_CHARACTER}*)
ERROR_REGULAR_STRING_LITERAL=(\"{REGULAR_STRING_LITERAL_CHARACTER}*{BAD_ESCAPE_SEQUENCE}({BAD_ESCAPE_SEQUENCE}|{REGULAR_STRING_LITERAL_CHARACTER})*\"?)
ERROR_STRING_LITERAL=({UNFINISHED_REGULAR_STRING_LITERAL}|{UNFINISHED_VERBATIM_STRING_LITERAL}|{ERROR_REGULAR_STRING_LITERAL})

UINT_LITERAL = (({DIGIT_DEC}+)|(0[xX]{DIGIT_HEX}+)|(0[oO]{DIGIT_OCT}+)|(0[bB]{DIGIT_BIN}+))
INT_LITERAL = -?{UINT_LITERAL}

INT8_LITERAL = ({INT_LITERAL}y)
UINT8_LITERAL = {UINT_LITERAL}uy

INT16_LITERAL = {INT_LITERAL}s
UINT16_LITERAL = {UINT_LITERAL}us
INT32_LITERAL = {INT_LITERAL}l
UINT32_LITERAL = {UINT_LITERAL}ul
NATIVEINT_LITERAL = {INT_LITERAL}n
UNATIVEINT_LITERAL = {UINT_LITERAL}un
INT64_LITERAL = {INT_LITERAL}L
UINT64_LITERAL = {UINT_LITERAL}UL

FLOAT_LITERAL = ((-?{DIGIT_DEC}+"."{DIGIT_DEC}*)|(-?{DIGIT_DEC}+("."{DIGIT_DEC}*)?([eE])(([+-])?){DIGIT_DEC}+))
			
FLOAT32_LITERAL = {FLOAT_LITERAL}F
FLOAT64_LITERAL = {FLOAT_LITERAL}

NOT_NUMBER_SIGN=[^#]
PP_NUMBER_SIGN=#

PP_BAD_DIRECTIVE=(define|undef|if|elif|else|endif|error|warning|region|endregion|line|pragma)({IDENTIFIER}|{DECIMAL_DIGIT})

PP_FILENAME_CHARACTER=[^\"\r\n\u0085\u2028\u2029]

PP_FILENAME=(\"{PP_FILENAME_CHARACTER}+\")
PP_BAD_FILENAME=(\"{PP_FILENAME_CHARACTER}+)
PP_DEC_DIGITS={DECIMAL_DIGIT}+

PP_CONDITIONAL_SYMBOL={IDENTIFIER}

WHITE_SPACE=({WHITE_SPACE_CHAR}+)
END_LINE={NOT_NEW_LINE}*(({PP_NEW_LINE_PAIR})|({PP_NEW_LINE_CHAR}))

%% 

<YYINITIAL> {IDENTIFIER} { currTokenType = makeToken(keywords.GetValueSafe(yytext()) ?? FSharpTokenType.IDENTIFIER); return currTokenType; }
<YYINITIAL> {CHARACTER_LITERAL} { currTokenType = makeToken (FSharpTokenType.CHARACTER_LITERAL); return currTokenType; }
<YYINITIAL> {STRING_LITERAL} { currTokenType = makeToken (FSharpTokenType.STRING_LITERAL); return currTokenType; }

<YYINITIAL> {NEW_LINE_PAIR} { currTokenType = makeToken (FSharpTokenType.NEW_LINE); return currTokenType; }
<YYINITIAL> {WHITE_SPACE} { currTokenType = makeToken(FSharpTokenType.WHITE_SPACE); return currTokenType; }

<YYINITIAL> "<@" { currTokenType = makeToken(FSharpTokenType.LQUOTE); return currTokenType; }
<YYINITIAL> "<@@" { currTokenType = makeToken(FSharpTokenType.LDQUOTE); return currTokenType; }
<YYINITIAL> "@>" { currTokenType = makeToken(FSharpTokenType.RQUOTE); return currTokenType; }
<YYINITIAL> "@@>" { currTokenType = makeToken(FSharpTokenType.RDQUOTE); return currTokenType; }
<YYINITIAL> "#" { currTokenType = makeToken(FSharpTokenType.HASH); return currTokenType; }
<YYINITIAL> "&" { currTokenType = makeToken(FSharpTokenType.AMP); return currTokenType; }
<YYINITIAL> "&&" { currTokenType = makeToken(FSharpTokenType.AMP_AMP); return currTokenType; }
<YYINITIAL> "||" { currTokenType = makeToken(FSharpTokenType.BAR_BAR); return currTokenType; }
<YYINITIAL> "\'" { currTokenType = makeToken(FSharpTokenType.QUOTE); return currTokenType; }
<YYINITIAL> "(" { currTokenType = makeToken(FSharpTokenType.LPAREN); return currTokenType; }
<YYINITIAL> ")" { currTokenType = makeToken(FSharpTokenType.RPAREN); return currTokenType; }
<YYINITIAL> "*" { currTokenType = makeToken(FSharpTokenType.STAR); return currTokenType; }
<YYINITIAL> "," { currTokenType = makeToken(FSharpTokenType.COMMA); return currTokenType; }
<YYINITIAL> "->" { currTokenType = makeToken(FSharpTokenType.RARROW); return currTokenType; }
<YYINITIAL> "?" { currTokenType = makeToken(FSharpTokenType.QMARK); return currTokenType; }
<YYINITIAL> "??" { currTokenType = makeToken(FSharpTokenType.QMARK_QMARK); return currTokenType; }
<YYINITIAL> ".." { currTokenType = makeToken(FSharpTokenType.DOT_DOT); return currTokenType; }
<YYINITIAL> "." { currTokenType = makeToken(FSharpTokenType.DOT); return currTokenType; }
<YYINITIAL> ":" { currTokenType = makeToken(FSharpTokenType.COLON); return currTokenType; }
<YYINITIAL> "::" { currTokenType = makeToken(FSharpTokenType.COLON_COLON); return currTokenType; }
<YYINITIAL> ":>" { currTokenType = makeToken(FSharpTokenType.COLON_GREATER); return currTokenType; }
<YYINITIAL> "@>." { currTokenType = makeToken(FSharpTokenType.RQUOTE_DOT); return currTokenType; }
<YYINITIAL> "@@>." { currTokenType = makeToken(FSharpTokenType.RDQUOTE_DOT); return currTokenType; }
<YYINITIAL> ">|]" { currTokenType = makeToken(FSharpTokenType.GREATER_BAR_RBRACK); return currTokenType; }
<YYINITIAL> ":?>" { currTokenType = makeToken(FSharpTokenType.COLON_QMARK_GREATER); return currTokenType; }
<YYINITIAL> ":?" { currTokenType = makeToken(FSharpTokenType.COLON_QMARK); return currTokenType; }
<YYINITIAL> ":=" { currTokenType = makeToken(FSharpTokenType.COLON_EQUALS); return currTokenType; }
<YYINITIAL> ";;" { currTokenType = makeToken(FSharpTokenType.SEMICOLON_SEMICOLON); return currTokenType; }
<YYINITIAL> ";" { currTokenType = makeToken(FSharpTokenType.SEMICOLON); return currTokenType; }
<YYINITIAL> "<-" { currTokenType = makeToken(FSharpTokenType.LARROW); return currTokenType; }
<YYINITIAL> "=" { currTokenType = makeToken(FSharpTokenType.EQUALS); return currTokenType; }
<YYINITIAL> "[" { currTokenType = makeToken(FSharpTokenType.LBRACK); return currTokenType; }
<YYINITIAL> "[|" { currTokenType = makeToken(FSharpTokenType.LBRACK_BAR); return currTokenType; }
<YYINITIAL> "<" { currTokenType = makeToken(FSharpTokenType.LESS); return currTokenType; }
<YYINITIAL> ">" { currTokenType = makeToken(FSharpTokenType.GREATER); return currTokenType; }
<YYINITIAL> "[<" { currTokenType = makeToken(FSharpTokenType.LBRACK_LESS); return currTokenType; }
<YYINITIAL> "]" { currTokenType = makeToken(FSharpTokenType.RBRACK); return currTokenType; }
<YYINITIAL> "|]" { currTokenType = makeToken(FSharpTokenType.BAR_RBRACK); return currTokenType; }
<YYINITIAL> ">]" { currTokenType = makeToken(FSharpTokenType.GREATER_RBRACK); return currTokenType; }
<YYINITIAL> "{" { currTokenType = makeToken(FSharpTokenType.LBRACE); return currTokenType; }
<YYINITIAL> "|" { currTokenType = makeToken(FSharpTokenType.BAR); return currTokenType; }
<YYINITIAL> "}" { currTokenType = makeToken(FSharpTokenType.RBRACE); return currTokenType; }
<YYINITIAL> "$" { currTokenType = makeToken(FSharpTokenType.DOLLAR); return currTokenType; }
<YYINITIAL> "%" { currTokenType = makeToken(FSharpTokenType.PERCENT_OP); return currTokenType; }
<YYINITIAL> "%%" { currTokenType = makeToken(FSharpTokenType.DPERCENT_OP); return currTokenType; }
<YYINITIAL> "-" { currTokenType = makeToken(FSharpTokenType.MINUS); return currTokenType; }
<YYINITIAL> "~" { currTokenType = makeToken(FSharpTokenType.RESERVED); return currTokenType; }
<YYINITIAL> "`" { currTokenType = makeToken(FSharpTokenType.RESERVED); return currTokenType; }

<YYINITIAL> . { currTokenType = makeToken(FSharpTokenType.BAD_CHARACTER); return currTokenType; }