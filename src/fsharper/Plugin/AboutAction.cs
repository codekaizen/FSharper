using System.Windows.Forms;
using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;

namespace Plugin
{
  [ActionHandler("Plugin.About")]
  public class AboutAction : IActionHandler
  {
    public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
    {
      // return true or false to enable/disable this action
      return true;
    }

    public void Execute(IDataContext context, DelegateExecute nextExecute)
    {
      MessageBox.Show(
        "FSharper\nJetBrains Inc.\n\nF# Support for ReSharper :)",
        "About FSharper",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information);
    }
  }
}
