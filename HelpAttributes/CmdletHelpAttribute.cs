﻿using System;

namespace SharePointPnP.PowerShell.CmdletHelpAttributes
{
    [AttributeUsage(AttributeTargets.Class,
                       AllowMultiple = false)]
    public sealed class CmdletHelpAttribute : Attribute
    {
        readonly string description;

        [Obsolete("Is not used. Use DetailedDescription instead.")]
        public string Details { get; set; }

        public string DetailedDescription { get; set; }
        public string Copyright { get; set; }
        public string Version { get; set; }

        public CmdletHelpCategory Category { get; set; }
        public CmdletHelpAttribute(string description)
        {
            this.description = description;
        }

        public string Description
        {
            get
            {
                return description;
            }
        }
        
    }

}