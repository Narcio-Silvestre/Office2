using Office.Models;
using System.Data.SqlClient;
using System.Data;

namespace Office.Dataset
{

    public class HomeDataSet
    {
        static SqlConnection? _connection = new (System.Configuration.ConfigurationManager.ConnectionStrings["_connection"].ConnectionString);
        static SqlDataAdapter? _adapter;
        static DataTable? _dataTable;


        public static int allNumEncargos()
        {
            _adapter = new SqlDataAdapter("select count(*) from encargo where YEAR(data) = YEAR(GETDATE());", _connection);
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(_dataTable.Rows[0][0]);
            }
            return 0;
        }

       

        public static int allEncCompleted()
        {
            _adapter = new SqlDataAdapter("select count(*) from encargo where YEAR(data) = YEAR(GETDATE()) and estadoid = 1;", _connection);
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(_dataTable.Rows[0][0]);
            }
            return 0;
        }


        public static int AllEncInt()
        {
            _adapter = new SqlDataAdapter("select * from encargo where estadoid = 2;", _connection);
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(_dataTable.Rows[0][0]);
            }
            return 0;
        }


        public static int AllEncVal()
        {
            _adapter = new SqlDataAdapter("select * from encargo where estadoid = 4;", _connection);
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(_dataTable.Rows[0][0]);
            }
            return 0;
        }
    }
}
