//------------------------------------------------------------------------------
// <auto-generated>
//     Generated by IntelliJ parserGen
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#pragma warning disable 0168, 0219, 0108, 0414
// ReSharper disable RedundantNameQualifier
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
namespace JetBrains.ReSharper.Psi.FSharp {
  public partial interface IQualifiedIdReference : JetBrains.ReSharper.Psi.FSharp.IValueReferenceExpressionOld {
    JetBrains.ReSharper.Psi.Tree.TreeNodeCollection<JetBrains.ReSharper.Psi.FSharp.Tree.IIdReference> References { get; }
    JetBrains.ReSharper.Psi.FSharp.Tree.IIdReference AddReferenceBefore (JetBrains.ReSharper.Psi.FSharp.Tree.IIdReference param, JetBrains.ReSharper.Psi.FSharp.Tree.IIdReference anchor);
    JetBrains.ReSharper.Psi.FSharp.Tree.IIdReference AddReferenceAfter (JetBrains.ReSharper.Psi.FSharp.Tree.IIdReference param, JetBrains.ReSharper.Psi.FSharp.Tree.IIdReference anchor);
    void RemoveReference (JetBrains.ReSharper.Psi.FSharp.Tree.IIdReference param);
    new JetBrains.ReSharper.Psi.FSharp.IQualifiedIdReferenceNode ToTreeNode();
  }
}