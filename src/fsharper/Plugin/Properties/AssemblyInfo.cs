using System.Reflection;
using JetBrains.ActionManagement;
using JetBrains.Application.PluginSupport;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Plugin")]
[assembly: AssemblyDescription("F# Support for ReSharper")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("JetBrains Inc.")]
[assembly: AssemblyProduct("Plugin")]
[assembly: AssemblyCopyright("Copyright © JetBrains Inc., 2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: ActionsXml("Plugin.Actions.xml")]

// The following information is displayed by ReSharper in the Plugins dialog
[assembly: PluginTitle("FSharper")]
[assembly: PluginDescription("F# Support for ReSharper")]
[assembly: PluginVendor("JetBrains Inc.")]
