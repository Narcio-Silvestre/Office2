using Office.Models;
using System.Data.SqlClient;
using System.Data;

namespace Office.Dataset
{

    public class MoldeDataSet
    {
        static SqlConnection? _connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["_connection"].ConnectionString);
        static SqlDataAdapter? _adapter;
        static DataTable? _dataTable;


        public static List<MoldeModel>? Index()
        {
            _adapter = new SqlDataAdapter("select id,nrMolde, maxShots,(nrMolde+'-'+nome) as descricao from molde", _connection);
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            List<MoldeModel> list = new List<MoldeModel>();
            if (_dataTable.Rows.Count > 0)
            {
                for (int x = 0; x < _dataTable.Rows.Count; x++)
                {
                    MoldeModel model = new MoldeModel();
                    model.id = Convert.ToInt32(_dataTable.Rows[x][0]);
                    model.maxShots = Convert.ToString(_dataTable.Rows[x][2]);
                    model.nrMolde = Convert.ToString(_dataTable.Rows[x][1]);
                    model.descCompleta = Convert.ToString(_dataTable.Rows[x][3]);
                    list.Add(model);
                    
                }
                return list;
            }
            return null;
        }

        public static List<MoldeModel>? MoldesEmIntv()
        {
            _adapter = new SqlDataAdapter("select id,nrMolde, maxShots,(nrMolde+'-'+nome) as descricao from molde where molde.id in (select moldeid from encargo where estadoid <> 1 )", _connection);
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            List<MoldeModel> list = new List<MoldeModel>();
            if (_dataTable.Rows.Count > 0)
            {
                for (int x = 0; x < _dataTable.Rows.Count; x++)
                {
                    MoldeModel model = new MoldeModel();
                    model.id = Convert.ToInt32(_dataTable.Rows[x][0]);
                    model.maxShots = Convert.ToString(_dataTable.Rows[x][2]);
                    model.nrMolde = Convert.ToString(_dataTable.Rows[x][1]);
                    model.descCompleta = Convert.ToString(_dataTable.Rows[x][3]);
                    list.Add(model);

                }
                return list;
            }
            return null;
        }

      


        
        public static MoldeModel? Get(int id)
        {

            _adapter = new SqlDataAdapter("select id,nrMolde, maxShots from molde where id=@id", _connection);
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("id", id));
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count > 0)
            {
                MoldeModel model = new MoldeModel();
                model.id = Convert.ToInt32(_dataTable.Rows[0][0]);
                model.maxShots = Convert.ToString(_dataTable.Rows[0][2]) ;
                model.nrMolde = Convert.ToString(_dataTable.Rows[0][1]);
                return model;
            }
            return null;
        }
    }
}
