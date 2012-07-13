using System.Text;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.FSharp.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Text;

namespace ActiveMesa.R2P.FSharp.Psi.FSharp.Tree
{
  public abstract class FSharpTokenBase : LeafElementBase, IFSharpTreeNode, ITokenNode
  {
    public TokenNodeType GetTokenType()
    {
      return (TokenNodeType) NodeType;
    }

    public override string ToString()
    {
      return base.ToString() + "(type:" + NodeType + ", text:" + GetText() + ")";
    }

    public override StringBuilder GetText(StringBuilder to)
    {
      to.Append(GetText());
      return to;
    }

    public override IBuffer GetTextAsBuffer()
    {
      return new StringBuffer(GetText());
    }
  }
}