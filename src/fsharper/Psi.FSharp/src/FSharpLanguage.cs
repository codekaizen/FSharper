using JetBrains.Annotations;

namespace JetBrains.ReSharper.Psi.FSharp
{
  [LanguageDefinition(Name)]
  public class FSharpLanguage : KnownLanguage
  {
    public new const string Name = "FSHARP";
    public new const string PresentableName = "F#";

    [CanBeNull] public static readonly FSharpLanguage Instance;
    
    private FSharpLanguage() : base(Name, PresentableName) { }

    protected FSharpLanguage([NotNull] string name) : base(name)
    {
    }

    protected FSharpLanguage([NotNull] string name, [NotNull] string presentableName) : base(name, presentableName)
    {
    }
  }
}