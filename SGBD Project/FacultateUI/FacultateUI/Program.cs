using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace FacultateUI
{class Global
    {
        public static string strConectare =ConfigurationManager.ConnectionStrings["Conectare"].ConnectionString ;
        public static SqlConnection connection;
        public static DataSet dataSet;

        public static SqlDataAdapter dataAdapter;

    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dashboard());
        }
    }
}
