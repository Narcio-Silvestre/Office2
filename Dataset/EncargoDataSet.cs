using Office.Models;
using System.Data.SqlClient;
using System.Data;

namespace Office.Dataset
{

    

    /// <summary>
    /// Classe de encargos, para criar,editar e obter todos os encargos.
    /// </summary>
    public class EncargoDataSet  
    {
        static SqlConnection? _connection = new (System.Configuration.ConfigurationManager.ConnectionStrings["_connection"].ConnectionString);
        static SqlDataAdapter? _adapter;
        static DataTable? _dataTable;



        /// <summary>
        /// Método para obter o encargo pelo id
        /// </summary>
        /// <param name="id">id do encargo</param>
        /// <returns>null ou o encargo</returns>
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
                encargo.descMolde = Convert.ToString(_dataTable.Rows[0][12]);

                return encargo;
            }
            return null;
        }

        
        /// <summary>
        /// Método para obter todos os encargos
        /// </summary>
        /// <returns>null ou uma lista de encargos</returns>
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
                    encargo.descMolde = Convert.ToString(_dataTable.Rows[x][12]);

                    //encargo.dataConc = Convert.ToDateTime(_dataTable.Rows[x][5]);
                    encargos.Add(encargo);
                }
                return encargos;
            }
            return null;
        }


        /// <summary>
        /// Método para obter todos os encargos em intervenção
        /// </summary>
        /// <returns>retorna null ou a lista de encargos em intervenção</returns>
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
                    encargo.descMolde = Convert.ToString(_dataTable.Rows[x][11]);

                    encargos.Add(encargo);
                }
                return encargos;
            }
            return null;
        }

        /// <summary>
        /// Método para obter todos os encargos para validar
        /// </summary>
        /// <returns>retorna null ou a lista de encargos em intervenção</returns>
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
                    encargo.descMolde = Convert.ToString(_dataTable.Rows[0][11]);

                    encargos.Add(encargo);
                }
                return encargos;
            }
            return null;
        }

        /// <summary>
        /// Cria um encargo
        /// </summary>
        /// <param name="encargo">modelo de encargo</param>
        /// <returns>retorna verdadeiro se for bem-sucedido,e falso se for o contrário</returns>
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

        /// <summary>
        /// Edita a descrição de um encargo
        /// </summary>
        /// <param name="encargo">modelo de vista de encargo</param>
        /// <returns>retorna verdadeiro se for bem-sucedido,e falso se for o contrário</returns>
        public static bool Edit(EncargoViewModel encargo)
        {


            _adapter = new SqlDataAdapter("editEncargo", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@desc", encargo.descProblema));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@id", encargo.id));
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
