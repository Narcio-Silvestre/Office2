using Office.Models;
using System.Data.SqlClient;
using System.Data;

namespace Office.Dataset
{
    /// <summary>
    /// Classe para obter os moldes
    /// </summary>
    public class MoldeDataSet
    {
        static SqlConnection? _connection = new (System.Configuration.ConfigurationManager.ConnectionStrings["_connection"].ConnectionString);
        static SqlDataAdapter? _adapter;
        static DataTable? _dataTable;

        /// <summary>
        /// Método para obter todos os moldes
        /// </summary>
        /// <returns>null ou uma lista de moldes</returns>
        public static List<MoldeModel>? Index()
        {
            _adapter = new SqlDataAdapter("select id,nrMolde, maxShots,(nrMolde+'-'+nome) as descricao,(select count(*) from encargo where encargo.moldeid = molde.id) as nrEncargos,shots,nome from molde", _connection);
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
                    model.descricao = Convert.ToString(_dataTable.Rows[x][6]);
                    model.nrEncargos = Convert.ToInt32(_dataTable.Rows[x][4]);
                    model.shots = Convert.ToInt32(_dataTable.Rows[x][5]);

                    list.Add(model);
                    
                }
                return list;
            }
            return null;
        }

        /// <summary>
        /// Obtém todos os moldes que têm encargo no momento
        /// </summary>
        /// <returns>null ou uma lista de moldes</returns>
        public static List<MoldeModel>? MoldesEmIntv()
        {
            _adapter = new SqlDataAdapter("select id,nrMolde, maxShots,(nrMolde+'-'+nome) as descricao,(select count(*) from encargo where encargo.moldeid = molde.id) as nrEncargos,shots,nome from molde where molde.id in (select moldeid from encargo where estadoid <> 1);", _connection);
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
                    model.descricao = Convert.ToString(_dataTable.Rows[x][6]);
                    model.nrEncargos = Convert.ToInt32(_dataTable.Rows[x][4]);
                    model.shots = Convert.ToInt32(_dataTable.Rows[x][5]);
                    list.Add(model);

                }
                return list;
            }
            return null;
        }

        /// <summary>
        /// Obtém todos os moldes que têm encargo no momento
        /// </summary>
        /// <returns>null ou uma lista de moldes</returns>
        public static List<MoldeModel>? MoldesDisp()
        {
            _adapter = new SqlDataAdapter("select id,nrMolde, maxShots,(nrMolde+'-'+nome) as descricao,(select count(*) from encargo where encargo.moldeid = molde.id) as nrEncargos,shots,nome from molde where molde.id not in (select moldeid from encargo where estadoid <> 1);", _connection);
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
                    model.descricao = Convert.ToString(_dataTable.Rows[x][6]);
                    model.nrEncargos = Convert.ToInt32(_dataTable.Rows[x][4]);
                    model.shots = Convert.ToInt32(_dataTable.Rows[x][5]);
                    list.Add(model);

                }
                return list;
            }
            return null;
        }

        /// <summary>
        /// Obtém todos os moldes que têm encargo no momento
        /// </summary>
        /// <returns>null ou uma lista de moldes</returns>
        public static List<MoldeModel>? MoldesSemEncargo()
        {
            _adapter = new SqlDataAdapter("select id,nrMolde, maxShots,(nrMolde+'-'+nome) as descricao,(select count(*) from encargo where encargo.moldeid = molde.id) as nrEncargos,shots,nome from molde where molde.id not in (select moldeid from encargo);", _connection);
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
                    model.descricao = Convert.ToString(_dataTable.Rows[x][6]);
                    model.nrEncargos = Convert.ToInt32(_dataTable.Rows[x][4]);
                    model.shots = Convert.ToInt32(_dataTable.Rows[x][5]);
                    list.Add(model);

                }
                return list;
            }
            return null;
        }

        /// <summary>
        /// Obtém o molde pelo id
        /// </summary>
        /// <param name="id">id do molde</param>
        /// <returns>retorna null ou um molde</returns>
        public static MoldeModel? Get(int id)
        {

            _adapter = new SqlDataAdapter("select id,nrMolde, maxShots,nome,maquina from molde where id=@id", _connection);
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("id", id));
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count > 0)
            {
                MoldeModel model = new MoldeModel();
                model.id = Convert.ToInt32(_dataTable.Rows[0][0]);
                model.maxShots = Convert.ToString(_dataTable.Rows[0][2]) ;
                model.nrMolde = Convert.ToString(_dataTable.Rows[0][1]);
                model.descCompleta = Convert.ToString(_dataTable.Rows[0][3]);
                model.descricao = Convert.ToString(_dataTable.Rows[0][4]);
                return model;
            }
            return null;
        }

        /// <summary>
        /// Método para criar um molde
        /// </summary>
        /// <param name="molde"></param>
        /// <returns></returns>
        public static bool Create(MoldeModel molde)
        {


            _adapter = new SqlDataAdapter("criarMolde", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@desc", molde.descCompleta));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@nrMolde", molde.nrMolde));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@maxShots", molde.maxShots));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@maquina", molde.descricao));
            _dataTable = new DataTable();
            try
            {
                _adapter.Fill(_dataTable);

            }
            catch (Exception ex)
            {
                return false;
            }
            if (_dataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Método para editar um molde
        /// </summary>
        /// <param name="molde"></param>
        /// <returns></returns>
        public static bool Edit(MoldeModel molde)
        {


            _adapter = new SqlDataAdapter("editMolde", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@id", molde.id));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@desc", molde.descCompleta));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@nrMolde", molde.nrMolde));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@maxShots", molde.maxShots));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@maquina", molde.descricao));
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Método para apagar um molde
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {

            _adapter = new SqlDataAdapter("deleteAllFromByMolde", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@id", id));
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
