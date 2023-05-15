using Office.Models;
using System.Data.SqlClient;
using System.Data;

namespace Office.Dataset
{
    /// <summary>
    /// Essa é a classe de anexos de encargos e intervenções
    /// </summary>
    public class AnexoDataSet 
    {
        static SqlConnection? _connection = new (System.Configuration.ConfigurationManager.ConnectionStrings["_connection"].ConnectionString);
        static SqlDataAdapter? _adapter;
        static DataTable? _dataTable;

        /// <summary>
        /// Obtém o caminho dos anexos de encargo
        /// </summary>
        /// <param name="id">id do encargo</param>
        /// <returns>retorna null se não houver anexos ou uma lista de caminhos para os anexos</returns>
        public static List<AnexoModel>? Index(int id)
        {
            _adapter = new SqlDataAdapter("select anexo.path from anexo where encargoid = @id;", _connection);
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("id", id));
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);

            if (_dataTable.Rows.Count > 0)
            {
                List<AnexoModel> list = new();
                for (int x = 0; x < _dataTable.Rows.Count; x++)
                {
                    AnexoModel anexos = new();
                    anexos.desc = Convert.ToString(_dataTable.Rows[x][0]);
                    list.Add(anexos);
                }
                return list;
            }
            return null;
        }


        /// <summary>
        /// Obtém os anexos de uma intervenção
        /// </summary>
        /// <param name="id">id da intervenção</param>
        /// <returns>retorna null se não houver anexos ou uma lista de caminhos para os anexos</returns>
        public static List<AnexoModel>? Index2(int id)
        {
            _adapter = new SqlDataAdapter("select anexoIntv.path from anexoIntv where intervencaoid = @id;", _connection);
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("id", id));
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);

            if (_dataTable.Rows.Count > 0)
            {
                List<AnexoModel> list = new();
                for (int x = 0; x < _dataTable.Rows.Count; x++)
                {
                    AnexoModel anexos = new();
                    anexos.desc = Convert.ToString(_dataTable.Rows[x][0]);
                    list.Add(anexos);
                }
                return list;
            }
            return null;
        }
    }


}
