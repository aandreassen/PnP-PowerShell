﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;
using SharePointPnP.PowerShell.CmdletHelpAttributes;

namespace SharePointPnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "SPOPublishingPage")]
    [CmdletHelp("Adds a publishing page",
      Category = CmdletHelpCategory.Publishing)]
    public class AddPublishingPage : SPOWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("Name")]
        public string PageName = string.Empty;

        [Parameter(Mandatory = true, HelpMessage = "The name of the page layout you want to use. Specify without the .aspx extension. So 'ArticleLeft' or 'BlankWebPartPage'")]
        public string PageTemplateName = null;

        [Parameter(Mandatory = false, ParameterSetName = "WithTitle")]
        public string Title;

        [Parameter(Mandatory = false, HelpMessage = "Publishes the page. Also Approves it if moderation is enabled on the Pages library.")]
        public SwitchParameter Publish;

        protected override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case "WithTitle":
                    {
                        SelectedWeb.AddPublishingPage(PageName, PageTemplateName, Title, publish: Publish);
                        break;
                    }
                default:
                    {
                        SelectedWeb.AddPublishingPage(PageName, PageTemplateName, publish: Publish);
                        break;
                    }
            }
        }
    }
}
