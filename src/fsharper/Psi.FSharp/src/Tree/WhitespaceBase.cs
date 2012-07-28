using System;

namespace JetBrains.ReSharper.Psi.FSharp.Tree
{
  internal abstract class WhitespaceBase : FSharpTokenBase, IWhitespaceNode
  {
    private readonly string myText;

    protected WhitespaceBase(string text)
    {
      myText = text;
    }

    public override int GetTextLength()
    {
      return myText.Length;
    }

    public override string GetText()
    {
      return myText;
    }

    public override String ToString()
    {
      return base.ToString() + " spaces:" + "\"" + GetText() + "\"";
    }

    public abstract bool IsNewLine { get; }
  }
}