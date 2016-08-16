﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using SharePointPnP.PowerShell.CmdletHelpAttributes;
using SharePointPnP.PowerShell.Commands.Base.PipeBinds;
using File = System.IO.File;
using Resources = SharePointPnP.PowerShell.Commands.Properties.Resources;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;

namespace SharePointPnP.PowerShell.Commands
{
    [Cmdlet(VerbsData.Export, "SPOTermGroupToXml", SupportsShouldProcess = true)]
    [CmdletHelp("Exports a taxonomy TermGroup to either the output or to an XML file.",
        Category = CmdletHelpCategory.Taxonomy)]
    [CmdletExample(
        Code = @"PS:> Export-SPOTermGroupToXml", 
        Remarks = "Exports all term groups in the default site collection term store to the standard output",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Export-SPOTermGroupToXml -Out output.xml", 
        Remarks = "Exports all term groups in the default site collection term store to the file 'output.xml' in the current folder",
        SortOrder = 2)]
    [CmdletExample(
        Code = @"PS:> Export-SPOTermGroupToXml -Out c:\output.xml -Identity ""Test Group""", 
        Remarks = "Exports the term group with the specified name to the file 'output.xml' located in the root folder of the C: drive.",
        SortOrder = 3)]
    public class ExportTermGroup : SPOCmdlet
    {
        [Parameter(Mandatory = false, HelpMessage = "The ID or name of the termgroup")]
        public TermGroupPipeBind Identity;

        [Parameter(Mandatory = false, HelpMessage = "File to export the data to.")]
        public string Out;

        [Parameter(Mandatory = false, HelpMessage = "If specified, a full provisioning template structure will be returned")]
        public SwitchParameter FullTemplate;

        [Parameter(Mandatory = false)]
        public Encoding Encoding = System.Text.Encoding.Unicode;

        [Parameter(Mandatory = false, HelpMessage = "Overwrites the output file if it exists.")]
        public SwitchParameter Force;


        protected override void ExecuteCmdlet()
        {
           // var template = new ProvisioningTemplate();

            var templateCi = new ProvisioningTemplateCreationInformation(ClientContext.Web) { IncludeAllTermGroups = true };

            templateCi.HandlersToProcess = Handlers.TermGroups;

            var template = ClientContext.Web.GetProvisioningTemplate(templateCi);
           
            template.Security = null;
            template.Features = null;
            template.CustomActions = null;
            template.ComposedLook = null;

            if (this.MyInvocation.BoundParameters.ContainsKey("Identity"))
            {
                if (Identity.Id != Guid.Empty)
                {
                    template.TermGroups.RemoveAll(t => t.Id != Identity.Id);
                }
                else if (Identity.Name != string.Empty)
                {
                    template.TermGroups.RemoveAll(t => t.Name != Identity.Name);
                }
            }
            var outputStream = XMLPnPSchemaFormatter.LatestFormatter.ToFormattedTemplate(template);

            var reader = new StreamReader(outputStream);

            var fullxml = reader.ReadToEnd();

            var xml = string.Empty;

            if (!FullTemplate)
            {
                var document = XDocument.Parse(fullxml);

                
                XNamespace pnp = XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2015_12;

                var termGroupsElement = document.Root.Descendants(pnp + "TermGroups").FirstOrDefault();

                if (termGroupsElement != null)
                {
                    xml = termGroupsElement.ToString();
                }
            }
            else
            {
                xml = fullxml;
            }

            if (!string.IsNullOrEmpty(Out))
            {
                if (!Path.IsPathRooted(Out))
                {
                    Out = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
                }
                if (File.Exists(Out))
                {
                    if (Force || ShouldContinue(string.Format(Resources.File0ExistsOverwrite, Out), Resources.Confirm))
                    {
                        File.WriteAllText(Out, xml, Encoding);
                    }
                }
                else
                {
                    File.WriteAllText(Out, xml, Encoding);
                }
            }
            else
            {
                WriteObject(xml);
            }





        }

    }
}
