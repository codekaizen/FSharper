namespace Daemon.FSharp.Stages
{
  using System.Collections.Generic;
  using JetBrains.Application.Settings;
  using JetBrains.ReSharper.Daemon;
  using JetBrains.ReSharper.Psi;
  using JetBrains.ReSharper.Psi.FSharp;
  using JetBrains.ReSharper.Psi.Tree;

  public abstract class FSharpDaemonStageBase : IDaemonStage
  {
    protected static bool IsSupported(IPsiSourceFile sourceFile)
    {
      if (sourceFile == null || !sourceFile.IsValid())
        return false;
      IFile psiFile = sourceFile.GetNonInjectedPsiFile<FSharpLanguage>();
      if (psiFile != null)
        return psiFile.Language.Is<FSharpLanguage>();
      else
        return false;
    }

    public abstract IEnumerable<IDaemonStageProcess> CreateProcess(IDaemonProcess process, IContextBoundSettingsStore settings, DaemonProcessKind processKind);
    public abstract ErrorStripeRequest NeedsErrorStripe(IPsiSourceFile sourceFile, IContextBoundSettingsStore settingsStore);
  }
}
