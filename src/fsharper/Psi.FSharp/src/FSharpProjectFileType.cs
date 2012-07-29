namespace JetBrains.ReSharper.Psi.FSharp
{
  using ProjectModel;

  [ProjectFileTypeDefinition(Name, Edition = "Csharp")]
  public class FSharpProjectFileType : KnownProjectFileType
  {
    public new const string Name = "FSHARP";
    private new const string PresentableName = "F#";

    public static string FS_EXTENSION = ".fs";
    public static string FSI_EXTENSION = ".fsi";
    public static string ML_EXTENSION = ".ml";
    public static string MLI_EXTENSION = ".mli";
    public static string FSX_EXTENSION = ".fsx";
    public static string FSSCRIPT_EXTENSION = ".fsscrtipt";

    public new static readonly FSharpProjectFileType Instance;

    private FSharpProjectFileType()
      : base(Name, PresentableName, new[] { FS_EXTENSION, FSI_EXTENSION, ML_EXTENSION, MLI_EXTENSION, FSX_EXTENSION, FSSCRIPT_EXTENSION }) { }

    protected FSharpProjectFileType(string name) : base(name) { }
    protected FSharpProjectFileType(string name, string presentableName) : base(name, presentableName) { }
  }
}