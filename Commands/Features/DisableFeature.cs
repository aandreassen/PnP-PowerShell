﻿using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using SharePointPnP.PowerShell.CmdletHelpAttributes;
using SharePointPnP.PowerShell.Commands.Base.PipeBinds;
using SharePointPnP.PowerShell.Commands.Enums;

namespace SharePointPnP.PowerShell.Commands.Features
{
    [Cmdlet("Disable", "SPOFeature", SupportsShouldProcess = false)]
    [CmdletHelp("Disables a feature", Category = CmdletHelpCategory.Features)]
    [CmdletExample(
        Code = "PS:> Disable-SPOFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe",
        Remarks = @"This will enable the feature with the id ""99a00f6e-fb81-4dc7-8eac-e09c6f9132fe""",          
        SortOrder = 1)]
    [CmdletExample(
        Code = "PS:> Disable-SPOFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Force", 
        Remarks = @"This will enable the feature with the id ""99a00f6e-fb81-4dc7-8eac-e09c6f9132fe"" with force.", 
        SortOrder = 2)]
    [CmdletExample(
        Code = "PS:> Disable-SPOFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Scope Web", 
        Remarks = @"This will enable the feature with the id ""99a00f6e-fb81-4dc7-8eac-e09c6f9132fe"" with the web scope.", 
        SortOrder = 3)]
    public class DisableFeature : SPOWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterAttribute.AllParameterSets, HelpMessage = "The id of the feature to disable.")]
        public GuidPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets, HelpMessage = "Forcibly disable the feature.")]
        public SwitchParameter Force;

        [Parameter(Mandatory = false, HelpMessage = "Specify the scope of the feature to deactivate, either Web or Site. Defaults to Web.")]
        public FeatureScope Scope = FeatureScope.Web;

        protected override void ExecuteCmdlet()
        {
            Guid featureId = Identity.Id;

            if (Scope == FeatureScope.Web)
            {
                this.SelectedWeb.DeactivateFeature(featureId);
            }
            else
            {
                ClientContext.Site.DeactivateFeature(featureId);
            }
        }
    }
}
