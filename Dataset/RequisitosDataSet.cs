using Office.Models;
using System.Data.SqlClient;
using System.Data;

namespace Office.Dataset
{

    public class RequisitosDataSet
    {
        static SqlConnection? _connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["_connection"].ConnectionString);
        static SqlDataAdapter? _adapter;
        static DataTable? _dataTable;


        public static List<RequisitosModel>? Index()
        {
            _adapter = new SqlDataAdapter("select * from tipoIntv", _connection);
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            List<RequisitosModel> requisitos1 = new List<RequisitosModel>();
            if (_dataTable.Rows.Count > 0)
            {
                for (int x = 0; x < _dataTable.Rows.Count; x++)
                {
                    RequisitosModel requisitos = new();
                    requisitos.id = Convert.ToInt32(_dataTable.Rows[x][0]);
                    requisitos.desc = Convert.ToString(_dataTable.Rows[x][1]);
                    requisitos1.Add(requisitos);
                }
                return requisitos1;
            }
            return null;
        }

        
        public static List<RequisitosModel>? Index(int id)
        {
            _adapter = new SqlDataAdapter("select id,tipoIntv.\"desc\" from EncReq join tipoIntv on tipoIntv.id = tipoIntvid where encargoid = @id", _connection);
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("id", id));
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);

            if (_dataTable.Rows.Count > 0)
            {
                List<RequisitosModel> list = new();
                for (int x = 0; x < _dataTable.Rows.Count; x++)
                {
                    RequisitosModel requisitos = new();
                    requisitos.id = Convert.ToInt32(_dataTable.Rows[x][0]);
                    requisitos.desc = Convert.ToString(_dataTable.Rows[x][1]);
                    list.Add(requisitos);
                }
                return list;
            }
            return null;
        }

        
        public static List<RequisitosModel>? Index2(int id)
        {
            _adapter = new SqlDataAdapter("select * from (select tipoIntv.\"desc\" from tipoIntv )A where A.\"desc\" not in (select tipoIntv.\"desc\" from tipoIntv  join EncReq on tipoIntv.id = tipoIntvid where encargoid = @id) \r\n", _connection);
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("id", id));
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);

            if (_dataTable.Rows.Count > 0)
            {
                List<RequisitosModel> list = new();
                for (int x = 0; x < _dataTable.Rows.Count; x++)
                {
                    RequisitosModel requisitos = new();

                    requisitos.desc = Convert.ToString(_dataTable.Rows[x][0]);
                    list.Add(requisitos);
                }
                return list;
            }
            return null;
        }
    }
}
