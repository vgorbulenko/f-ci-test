﻿using System.Reflection;

[assembly: AssemblyVersion(Constants.ASSEMBLY_VERSION)]
[assembly: AssemblyFileVersion(Constants.ASSEMBLY_VERSION)]
[assembly: AssemblyCompany(Constants.AUTHOR_NAME)]
[assembly: AssemblyProduct(Constants.DESCRIPTION)]
[assembly: AssemblyCopyright(Constants.COPYRIGHT)]

public static class Constants
{
    internal const string ASSEMBLY_VERSION = "0.1.0";
    internal const string AUTHOR_NAME = "Frustum Inc.";
    internal const string DESCRIPTION = "GENERATE Installer.";
    internal const string COPYRIGHT = "Copyright 2018";
}
