namespace JetBrains.ReSharper.Psi.FSharp.Impl
{
  using ProjectModel;
  using Psi.Impl;
  using Psi.Parsing;
  using Text;
  using UI.Icons;
  using JetBrains.Util;
  using CSharp.Resources;

  [ProjectFileType(typeof(FSharpProjectFileType))]
  public class FSharpProjectFileLanguageService : IProjectFileLanguageService
  {
    private readonly FSharpProjectFileType fsharpProjectFileType;

    public ProjectFileType LanguageType
    {
      get
      {
        return fsharpProjectFileType;
      }
    }

    public IconId Icon { get { return PsiCSharpThemedIcons.Csharp.Id; } }

    public FSharpProjectFileLanguageService(FSharpProjectFileType fsharpProjectFileType)
    {
      this.fsharpProjectFileType = fsharpProjectFileType;
    }

    public PsiLanguageType GetPsiLanguageType(IProjectFile projectFile)
    {
      if (ProjectFileTypeEx.Is<FSharpProjectFileType>(projectFile.LanguageType))
        return FSharpLanguage.Instance;
      else
        return UnknownLanguage.Instance;
    }

    public PsiLanguageType GetPsiLanguageType(ProjectFileType languageType)
    {
      if (ProjectFileTypeEx.Is<FSharpProjectFileType>(languageType))
        return FSharpLanguage.Instance;
      else
        return UnknownLanguage.Instance;
    }

    public ILexerFactory GetMixedLexerFactory(ISolution solution, IBuffer buffer, IPsiSourceFile sourceFile = null)
    {
      return FSharpLanguage.Instance.LanguageService().GetPrimaryLexerFactory();
    }

    public PreProcessingDirective[] GetPreprocessorDefines(IProject project)
    {
      PreProcessingDirective[] processingDirectiveArray = EmptyArray<PreProcessingDirective>.Instance;
      var projectConfiguration = project.ActiveConfiguration as IFSharpProjectConfiguration;
      if (projectConfiguration != null)
      {
        var compilationConstants = projectConfiguration.ConditionalCompilationConstants;
        if (!string.IsNullOrEmpty(compilationConstants))
        {
          var strArray = compilationConstants.Split(new[]
          {
            ';',
            ',',
            ' '
          });
          processingDirectiveArray = new PreProcessingDirective[strArray.Length];
          for (int index = 0; index < strArray.Length; ++index)
            processingDirectiveArray[index] = new PreProcessingDirective(strArray[index].Trim(), string.Empty);
        }
      }
      return processingDirectiveArray;
    }

    public IPsiSourceFileProperties GetPsiProperties(IProjectFile projectFile, IPsiSourceFile sourceFile)
    {
      Assertion.Assert(ProjectFileTypeEx.IsProjectFileType(projectFile.LanguageType, LanguageType), 
        "projectFile.LanguageType == LanguageType");
      return new FSharpPsiProperties(projectFile, sourceFile);
    }

    private class FSharpPsiProperties : DefaultPsiProjectFileProperties
    {
      bool IsCompile
      {
        get
        {
          return ProjectFile.GetProperties().BuildAction == BuildAction.COMPILE;
        }
      }

      public override bool IsNonUserFile
      {
        get
        {
          return !IsCompile;
        }
      }

      public override bool ProvidesCodeModel
      {
        get
        {
          return IsCompile;
        }
      }

      public FSharpPsiProperties(IProjectFile projectFile, IPsiSourceFile sourceFile)
        : base(projectFile, sourceFile)
      {
      }
    }
  }
}
