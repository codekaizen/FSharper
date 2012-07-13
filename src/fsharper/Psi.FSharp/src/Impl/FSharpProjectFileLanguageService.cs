using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Impl;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Text;
using JetBrains.UI.Icons;
using JetBrains.Util;

namespace JetBrains.ReSharper.Psi.FSharp.Impl
{
  [ProjectFileType(typeof(FSharpProjectFileType))]
  public class FSharpProjectFileLanguageService : IProjectFileLanguageService
  {
    private readonly FSharpProjectFileType myFSharpProjectFileType;

    public ProjectFileType LanguageType
    {
      get
      {
        return myFSharpProjectFileType;
      }
    }

    public IconId Icon { get { throw new NotImplementedException(); } }

    public FSharpProjectFileLanguageService(FSharpProjectFileType fsharpProjectFileType)
    {
      this.myFSharpProjectFileType = fsharpProjectFileType;
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
        return (PsiLanguageType)FSharpLanguage.Instance;
      else
        return (PsiLanguageType)UnknownLanguage.Instance;
    }

    public ILexerFactory GetMixedLexerFactory(ISolution solution, IBuffer buffer, IPsiSourceFile sourceFile = null)
    {
      return FSharpLanguage.Instance.LanguageService().GetPrimaryLexerFactory();
    }

    public PreProcessingDirective[] GetPreprocessorDefines(IProject project)
    {
      PreProcessingDirective[] processingDirectiveArray = EmptyArray<PreProcessingDirective>.Instance;
      IFSharpProjectConfiguration projectConfiguration = project.ActiveConfiguration as IFSharpProjectConfiguration;
      if (projectConfiguration != null)
      {
        string compilationConstants = projectConfiguration.ConditionalCompilationConstants;
        if (!string.IsNullOrEmpty(compilationConstants))
        {
          string[] strArray = compilationConstants.Split(new char[3]
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
      Assertion.Assert(ProjectFileTypeEx.IsProjectFileType(projectFile.LanguageType, this.LanguageType), "projectFile.LanguageType == LanguageType");
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
