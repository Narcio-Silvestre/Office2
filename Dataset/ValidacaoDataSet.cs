using Office.Models;
using System.Data;
using System.Data.SqlClient;

namespace Office.Dataset
{


    public class ValidacaoDataSet
    {
        static SqlConnection? _connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["_connection"].ConnectionString);
        static SqlDataAdapter? _adapter;
        static DataTable? _dataTable;


        public static bool Get(string id)
        {
            return true;
        }

        
        public static bool Qualidade(ValidacaoModel data)
        {
            Console.WriteLine("ent:" + data.idEntidade);
            Console.WriteLine("idInt:" + data.idInter);
            Console.WriteLine("val:" + data.aprovado);
            Console.WriteLine("desc" + data.descricao);
            _adapter = new SqlDataAdapter("criarValidacaoQual", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@desc", data.descricao));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@idResp", data.idEntidade));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@val", data.aprovado));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@id", data.idInter));
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count != 0)
            {
                return false;
            }
            return true;
        }

        
        public static bool Producao(ValidacaoModel data)
        {
            Console.WriteLine("ent:" + data.idEntidade);
            Console.WriteLine("idInt:" + data.idInter);
            Console.WriteLine("val:" + data.aprovado);
            Console.WriteLine("desc" + data.descricao);
            _adapter = new SqlDataAdapter("criarValidacaoProd", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@desc", data.descricao));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@idResp", data.idEntidade));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@val", data.aprovado));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@id", data.idInter));
            _dataTable = new DataTable();
            _adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count != 0)
            {
                return false;
            }
            return true;
        }
    }
}
