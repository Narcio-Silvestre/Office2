using Office.Models;
using System.Data.SqlClient;
using System.Data;

namespace Office.Dataset
{

    

    
    public class EncargoDataSet  
    {
        static SqlConnection? _connection = new SqlConnection("Data Source=Lolly;Initial Catalog=WORK;Integrated Security=True;");
        static SqlDataAdapter? _adapter;
        static DataTable? _dataTable;




        public static EncargoViewModel? Encargo(int id)
        {
            _adapter = new SqlDataAdapter("GetEncargobyId", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _dataTable = new DataTable();
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("id", id));
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count > 0)
            {

                EncargoViewModel encargo = new();
                encargo.id = Convert.ToInt32(_dataTable.Rows[0][0]);
                encargo.descProblema = Convert.ToString(_dataTable.Rows[0][3]);
                encargo.dataNecMeio = Convert.ToDateTime(_dataTable.Rows[0][4]);
                encargo.data = Convert.ToDateTime(_dataTable.Rows[0][1]);
                encargo.nrEncargo = Convert.ToString(_dataTable.Rows[0][2]);
                encargo.entidade = Convert.ToString(_dataTable.Rows[0][6]);
                encargo.estado = Convert.ToString(_dataTable.Rows[0][8]);
                encargo.molde = Convert.ToString(_dataTable.Rows[0][7]);
                encargo.prioridade = Convert.ToString(_dataTable.Rows[0][9]);
                encargo.nrInt = Convert.ToInt32(_dataTable.Rows[0][10]);
                encargo.validQual = Convert.ToInt32(_dataTable.Rows[0][11]);

                return encargo;
            }
            return null;
        }

        
        public static List<EncargoViewModel>? Completed()
        {
            _adapter = new SqlDataAdapter("GetEncargos", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            List<EncargoViewModel> encargos = new List<EncargoViewModel>();
            if (_dataTable.Rows.Count > 0)
            {
                for (int x = 0; x < _dataTable.Rows.Count; x++)
                {
                    EncargoViewModel encargo = new();
                    encargo.id = Convert.ToInt32(_dataTable.Rows[x][0]);
                    encargo.descProblema = Convert.ToString(_dataTable.Rows[x][3]);
                    encargo.dataNecMeio = Convert.ToDateTime(_dataTable.Rows[x][4]);
                    encargo.data = Convert.ToDateTime(_dataTable.Rows[x][1]);
                    encargo.nrEncargo = Convert.ToString(_dataTable.Rows[x][2]);
                    encargo.entidade = Convert.ToString(_dataTable.Rows[x][6]);
                    encargo.estado = Convert.ToString(_dataTable.Rows[x][8]);
                    encargo.molde = Convert.ToString(_dataTable.Rows[x][7]);
                    encargo.prioridade = Convert.ToString(_dataTable.Rows[x][9]);
                    encargo.nrInt = Convert.ToInt32(_dataTable.Rows[x][10]);
                    encargo.validQual = Convert.ToInt32(_dataTable.Rows[x][11]);

                    //encargo.dataConc = Convert.ToDateTime(_dataTable.Rows[x][5]);
                    encargos.Add(encargo);
                }
                return encargos;
            }
            return null;
        }


        
        public static List<EncargoViewModel>? AllInter()
        {
            _adapter = new SqlDataAdapter("GetEncargosAllInInter", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            List<EncargoViewModel> encargos = new List<EncargoViewModel>();
            if (_dataTable.Rows.Count > 0)
            {
                for (int x = 0; x < _dataTable.Rows.Count; x++)
                {
                    EncargoViewModel encargo = new();
                    encargo.id = Convert.ToInt32(_dataTable.Rows[x][0]);
                    encargo.descProblema = Convert.ToString(_dataTable.Rows[x][3]);
                    encargo.dataNecMeio = Convert.ToDateTime(_dataTable.Rows[x][4]);
                    encargo.data = Convert.ToDateTime(_dataTable.Rows[x][1]);
                    encargo.nrEncargo = Convert.ToString(_dataTable.Rows[x][2]);
                    encargo.entidade = Convert.ToString(_dataTable.Rows[x][6]);
                    encargo.estado = Convert.ToString(_dataTable.Rows[x][8]);
                    encargo.molde = Convert.ToString(_dataTable.Rows[x][7]);
                    encargo.prioridade = Convert.ToString(_dataTable.Rows[x][9]);
                    encargo.nrInt = Convert.ToInt32(_dataTable.Rows[x][10]);
                    encargos.Add(encargo);
                }
                return encargos;
            }
            return null;
        }

        
        public static List<EncargoViewModel>? AllVal()
        {
            _adapter = new SqlDataAdapter("GetEncargosAllInVal", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            List<EncargoViewModel> encargos = new List<EncargoViewModel>();
            if (_dataTable.Rows.Count > 0)
            {
                for (int x = 0; x < _dataTable.Rows.Count; x++)
                {
                    EncargoViewModel encargo = new();
                    encargo.id = Convert.ToInt32(_dataTable.Rows[x][0]);
                    encargo.descProblema = Convert.ToString(_dataTable.Rows[x][3]);
                    encargo.dataNecMeio = Convert.ToDateTime(_dataTable.Rows[x][4]);
                    encargo.data = Convert.ToDateTime(_dataTable.Rows[x][1]);
                    encargo.nrEncargo = Convert.ToString(_dataTable.Rows[x][2]);
                    encargo.entidade = Convert.ToString(_dataTable.Rows[x][6]);
                    encargo.estado = Convert.ToString(_dataTable.Rows[x][8]);
                    encargo.molde = Convert.ToString(_dataTable.Rows[x][7]);
                    encargo.prioridade = Convert.ToString(_dataTable.Rows[x][9]);
                    encargo.nrInt = Convert.ToInt32(_dataTable.Rows[x][10]);
                    encargos.Add(encargo);
                }
                return encargos;
            }
            return null;
        }

        public static bool Create(EncargoMolde encargo)
        {


            _adapter = new SqlDataAdapter("criarEncargo", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@desc", encargo.descProblema));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@dataNecMeio", encargo.dataNecMeio));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@utilizadorid", encargo.entidadeid));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@moldeid", encargo.moldeid));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@qualidade", encargo.qualidade));
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                foreach (var item in encargo.anexos)
                {
                    _adapter = new SqlDataAdapter("Criar_Anexo", _connection);
                    _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand.Parameters.Add(new SqlParameter("@desc", item));
                    _dataTable = new DataTable();
                    _adapter.Fill(_dataTable);
                    if (_dataTable.Rows.Count > 0)
                    {
                        return false;
                    }
                }

                foreach (var item in encargo.intervencao)
                {

                    if (item != "false")
                    {
                        _adapter = new SqlDataAdapter("Criar_EncReq", _connection);
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
            }
            return true;
        }
    }
}
