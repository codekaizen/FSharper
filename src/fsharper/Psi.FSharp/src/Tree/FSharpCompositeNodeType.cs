using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

namespace JetBrains.ReSharper.Psi.FSharp.Tree
{
  public abstract class FSharpCompositeNodeType : CompositeNodeType
  {
    protected FSharpCompositeNodeType(string s) : base(s)
    {
    }
  }
}