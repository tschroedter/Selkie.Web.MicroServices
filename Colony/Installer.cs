﻿using System.Diagnostics.CodeAnalysis;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.Colony
{
    [ExcludeFromCodeCoverage]
    public sealed class Installer : BaseInstaller <Installer>
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "Selkie.Web.MicroServices.Colony";
        }
    }
}