using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;

namespace JetBrains.ReSharper.Psi.FSharp.Parsing
{
  public interface IFSharpParser : IParser
  {
    new IFile ParseFile();
    // the rest of the API is a bit too scary right now
  }
}