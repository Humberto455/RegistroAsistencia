using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuickJump
{
    class Proceso
    {
        protected Conexion con = new Conexion();
        protected DataTable dt = new DataTable();
    }
}
