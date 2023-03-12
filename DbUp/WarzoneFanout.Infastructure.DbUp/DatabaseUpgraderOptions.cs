using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarzoneFanout.Infastructure.DbUp
{
    public class DatabaseUpgraderOptions
    {
        [Option('s', "server", Required = true, HelpText = "SQL Instance")]
        public string DatabaseServer { get; set; }

        [Option('d', "database", Required = true, HelpText = "Database to Upgrade")]
        public string DatabaseName { get; set; }

        [Option('u', "user", Required = true, HelpText = "SQL Login")]
        public string User { get; set; }

        [Option('p', "password", Required = true, HelpText = "SQL Password")]
        public string Password { get; set; }

        [Option('t', "testdata", Required = false, HelpText = "Include scripts ending '.TestData.sql'")]
        public bool IncludeTestData { get; set; }
    }
}
