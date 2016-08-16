﻿using Microsoft.SharePoint.Client;
using SharePointPnP.PowerShell.CmdletHelpAttributes;
using SharePointPnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "SPOEventReceiver")]
    [CmdletHelp("Adds a new event receiver",
        Category = CmdletHelpCategory.EventReceivers)]
    [CmdletExample(
      Code = @"PS:> Add-SPOEventReceiver -List ""ProjectList"" -Name ""TestEventReceiver"" -Url https://yourserver.azurewebsites.net/eventreceiver.svc -EventReceiverType ItemAdded -Synchronization Asynchronous",
      Remarks = @"This will add a new event receiver that is executed after an item has been added to the ProjectList list", SortOrder = 1)]
    public class AddEventReceiver : SPOWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public string Name;

        [Parameter(Mandatory = true)]
        public string Url;

        [Parameter(Mandatory = true)]
        [Alias("Type")]
        public EventReceiverType EventReceiverType;

        [Parameter(Mandatory = true)]
        [Alias("Sync")]
        public EventReceiverSynchronization Synchronization;

        [Parameter(Mandatory = false)]
        public int SequenceNumber = 1000;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (this.MyInvocation.BoundParameters.ContainsKey("List"))
            {
                var list = List.GetList(SelectedWeb);
                WriteObject(list.AddRemoteEventReceiver(Name, Url, EventReceiverType, Synchronization, SequenceNumber, Force));
            }
            else
            {
                WriteObject(SelectedWeb.AddRemoteEventReceiver(Name, Url, EventReceiverType, Synchronization, SequenceNumber, Force));
            }

        }

    }

}


