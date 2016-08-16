﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;
using SharePointPnP.PowerShell.CmdletHelpAttributes;
using SharePointPnP.PowerShell.Commands.Base.PipeBinds;

namespace SharePointPnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Remove, "SPOUserFromGroup")]
    [CmdletHelp("Removes a user from a group",
        Category = CmdletHelpCategory.Principals)]
    [CmdletExample(
        Code = @"PS:> Remove-SPOUserFromGroup -LoginName user@company.com -GroupName 'Marketing Site Members'",
        SortOrder = 1)]
    public class RemoveUserFromGroup : SPOWebCmdlet
    {

        [Parameter(Mandatory = true, HelpMessage = "A valid login name of a user")]
        [Alias("LogonName")]
        public string LoginName = string.Empty;

        [Parameter(Mandatory = true, HelpMessage = "A group object, an ID or a name of a group")]
        [Alias("GroupName")]
        public GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var group = Identity.GetGroup(SelectedWeb);
            try
            {
                User user = SelectedWeb.SiteUsers.GetByEmail(LoginName);
                ClientContext.Load(user);
                ClientContext.ExecuteQueryRetry();
                SelectedWeb.RemoveUserFromGroup(group, user);
            }
            catch
            {
                User user = SelectedWeb.SiteUsers.GetByLoginName(LoginName);
                ClientContext.Load(user);
                ClientContext.ExecuteQueryRetry();
                SelectedWeb.RemoveUserFromGroup(group, user);
            }
        }
    }
}
