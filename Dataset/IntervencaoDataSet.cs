using Office.Models;
using System.Data;
using System.Data.SqlClient;


namespace Office.Dataset
{

    public class IntervencaoDataSet 
    {
        static SqlConnection? _connection = new SqlConnection("Data Source=Lolly;Initial Catalog=WORK;Integrated Security=True;");
        static SqlDataAdapter? _adapter;
        static DataTable? _dataTable;


        public static List<IntervencaoModel2>? Intervencao(int id)
        {
            _adapter = new SqlDataAdapter("GetInterValidbyEncargo", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _dataTable = new DataTable();
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("id", id));
            _adapter.Fill(_dataTable);
            List<IntervencaoModel2> intervencoes = new List<IntervencaoModel2>();
            if (_dataTable.Rows.Count > 0)
            {
                for (int x = 0; x < _dataTable.Rows.Count; x++)
                {
                    IntervencaoModel2 intervencao = new();
                    intervencao.id = Convert.ToInt32(_dataTable.Rows[x][0]);
                    intervencao.data = Convert.ToDateTime(_dataTable.Rows[x][1]);
                    intervencao.descInt = Convert.ToString(_dataTable.Rows[x][2]);
                    intervencao.valProducao = Convert.ToInt32(_dataTable.Rows[x][3]);
                    intervencao.valQualidade = Convert.ToInt32(_dataTable.Rows[x][4]);
                    intervencao.descProducao = Convert.ToString(_dataTable.Rows[x][5]);
                    intervencao.descQualidade = Convert.ToString(_dataTable.Rows[x][6]);
                    intervencao.dataValQual = Convert.ToDateTime(_dataTable.Rows[x][7]);
                    intervencao.dataValProd = Convert.ToDateTime(_dataTable.Rows[x][8]);
                    intervencao.respValQual = Convert.ToString(_dataTable.Rows[x][9]);
                    intervencao.respValProd = Convert.ToString(_dataTable.Rows[x][10]);
                    intervencao.encargo = Convert.ToString(_dataTable.Rows[x][11]);
                    intervencao.utilizador = Convert.ToString(_dataTable.Rows[x][12]);
                    intervencao.estado = Convert.ToString(_dataTable.Rows[x][13]);
                    intervencoes.Add(intervencao);
                }
                return intervencoes;
            }
            return null;
        }

      
        public static IntervencaoModel2? Intervencao2(int id)
        {
            _adapter = new SqlDataAdapter("GetNextInter", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _dataTable = new DataTable();
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("id", id));
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count > 0)
            {

                IntervencaoModel2 intervencao = new();
                intervencao.id = Convert.ToInt32(_dataTable.Rows[0][0]);
                intervencao.data = Convert.ToDateTime(_dataTable.Rows[0][1]);
                intervencao.descInt = Convert.ToString(_dataTable.Rows[0][2]);
                intervencao.valProducao = Convert.ToInt32(_dataTable.Rows[0][3]);
                intervencao.valQualidade = Convert.ToInt32(_dataTable.Rows[0][4]);
                intervencao.descProducao = Convert.ToString(_dataTable.Rows[0][5]);
                intervencao.descQualidade = Convert.ToString(_dataTable.Rows[0][6]);
                intervencao.dataValQual = Convert.ToDateTime(_dataTable.Rows[0][7]);
                intervencao.dataValProd = Convert.ToDateTime(_dataTable.Rows[0][8]);
                intervencao.respValQual = Convert.ToString(_dataTable.Rows[0][9]);
                intervencao.respValProd = Convert.ToString(_dataTable.Rows[0][10]);
                intervencao.encargo = Convert.ToString(_dataTable.Rows[0][11]);
                intervencao.utilizador = Convert.ToString(_dataTable.Rows[0][12]);
                intervencao.estado = Convert.ToString(_dataTable.Rows[0][13]);
                return intervencao;
            }
            return null;
        }

        
        public static bool Intervencao(IntervencaoModel data)
        {

            _adapter = new SqlDataAdapter("criarIntervencao", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@utiId", data.idEntidade));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@descInt", data.descricao));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@encId", data.idEncargo));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@extInt", data.extInt));
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count != 0)
            {
                return false;
            }
            else
            {
                foreach (var item in data.anexos)
                {
                    _adapter = new SqlDataAdapter("criarAnexoIntervencao", _connection);
                    _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand.Parameters.Add(new SqlParameter("@desc", item));
                    _dataTable = new DataTable();
                    _adapter.Fill(_dataTable);
                    if (_dataTable.Rows.Count > 0)
                    {
                        return false;
                    }
                }
            }
            return true;

        }


    }
}
