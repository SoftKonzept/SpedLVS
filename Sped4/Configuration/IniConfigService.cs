using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Reporting;
using Telerik.Reporting.AI;

namespace Sped4.Configuration
{
    public class IniConfigService
    {
        private readonly IniConfigService _config;

        //public DbConnectionFactory(IniConfigService config)
        //{
        //    _config = config;
        //}

        //public SqlConnection CreateLvs()
        //    => new SqlConnection(
        //        _config.GetConnectionStringForSection("ConfigLVS"));

        //public SqlConnection CreateCom()
        //    => new SqlConnection(
        //        _config.GetConnectionStringForSection("ConfigCOM"));

        //public SqlConnection CreateCall()
        //    => new SqlConnection(
        //        _config.GetConnectionStringForSection("ConfigCALL"));

        //public SqlConnection CreateArchiv()
        //    => new SqlConnection(
        //        _config.GetConnectionStringForSection("ConfigARCHIV"));

    }
}
