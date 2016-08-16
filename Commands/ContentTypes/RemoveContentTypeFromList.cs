﻿using SharePointPnP.PowerShell.CmdletHelpAttributes;
using Microsoft.SharePoint.Client;
using System.Management.Automation;
using SharePointPnP.PowerShell.Commands.Base.PipeBinds;

namespace SharePointPnP.PowerShell.Commands
{

    [Cmdlet(VerbsCommon.Remove, "SPOContentTypeFromList")]
    [CmdletHelp("Removes a content type from a list", 
        Category = CmdletHelpCategory.ContentTypes)]
    [CmdletExample(
        Code = @"PS:> Remove-SPOContentTypeFromList -List ""Documents"" -ContentType ""Project Document""",
        Remarks = @"This will remove a content type called ""Project Document"" from the ""Documents"" list",
        SortOrder = 1)]
    public class RemoveContentTypeFromList : SPOWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public ContentTypePipeBind ContentType;

        protected override void ExecuteCmdlet()
        {
            ContentType ct = null;
            List list = List.GetList(SelectedWeb);

            if (ContentType.ContentType == null)
            {
                if (ContentType.Id != null)
                {
                    ct = this.SelectedWeb.GetContentTypeById(ContentType.Id,true);
                }
                else if (ContentType.Name != null)
                {
                    ct = this.SelectedWeb.GetContentTypeByName(ContentType.Name,true);
                }
            }
            else
            {
                ct = ContentType.ContentType;
            }
            if (ct != null)
            {
                this.SelectedWeb.RemoveContentTypeFromList(list, ct);
            }
        }

    }
}
