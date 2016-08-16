﻿using System.Management.Automation;
using SharePointPnP.PowerShell.CmdletHelpAttributes;
using Microsoft.SharePoint.Client;

namespace SharePointPnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Set, "SPOContext")]
    [CmdletHelp("Sets the Client Context to use by the cmdlets",
        Category = CmdletHelpCategory.Base)]
    [CmdletExample(
        Code = @"PS:> Connect-SPOnline -Url $siteAurl -Credentials $credentials
PS:> $ctx = Get-SPOContext
PS:> Get-SPOList # returns the lists from site specified with $siteAurl
PS:> Connect-SPOnline -Url $siteBurl -Credentials $credentials
PS:> Get-SPOList # returns the lists from the site specified with $siteBurl
PS:> Set-SPOContext -Context $ctx # switch back to site A
PS:> Get-SPOList # returns the lists from site A", SortOrder = 1)]
    public class SetContext : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1, HelpMessage = "The ClientContext to set")]
        public ClientContext Context;

        protected override void ProcessRecord()
        {
            SPOnlineConnection.CurrentConnection.Context = Context;
        }
    }
}
