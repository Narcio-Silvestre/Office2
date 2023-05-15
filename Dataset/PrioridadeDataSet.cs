using Office.Models;
using System.Data.SqlClient;
using System.Data;

namespace Office.Dataset
{

    public class PrioridadeDataSet
    {
        static SqlConnection? _connection = new (System.Configuration.ConfigurationManager.ConnectionStrings["_connection"].ConnectionString);
        static SqlDataAdapter? _adapter;
        static DataTable? _dataTable;


        public static List<PrioridadeModel>? Index()
        {
            _adapter = new SqlDataAdapter("select * from Prioridade", _connection);
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            List<PrioridadeModel> prioridades = new List<PrioridadeModel>();
            if (_dataTable.Rows.Count > 0)
            {
                for (int x = 0; x < _dataTable.Rows.Count; x++)
                {
                    PrioridadeModel prioridade = new PrioridadeModel();
                    prioridade.id = Convert.ToInt32(_dataTable.Rows[x][0]);
                    prioridade.desc = Convert.ToString(_dataTable.Rows[x][1]);
                    prioridades.Add(prioridade);
                }
                return prioridades;
            }
            return null;
        }

        
        public static PrioridadeModel? Index(int id)
        {
            _adapter = new SqlDataAdapter("select * from Priridade where id=@id", _connection);
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("id", id));
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);

            if (_dataTable.Rows.Count > 0)
            {
                PrioridadeModel prioridade = new PrioridadeModel();
                prioridade.id = Convert.ToInt32(_dataTable.Rows[0][0]);
                prioridade.desc = Convert.ToString(_dataTable.Rows[0][1]);
                return prioridade;
            }
            return null;
        }
    }
}
