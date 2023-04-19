using Microsoft.AspNetCore.Mvc;
using Office.Models;
using System.Data;
using System.Data.SqlClient;

namespace Office.Dataset
{

    public class LoginDataSet : ControllerBase
    {
        static SqlConnection? _connection = new SqlConnection("Data Source=Lolly;Initial Catalog=WORK;Integrated Security=True;");
        static SqlDataAdapter? _adapter;
        static DataTable? _dataTable;
        

        public static UserModel? Create(LoginModel login)
        {
            
            _adapter = new SqlDataAdapter("Login", _connection);
            _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            _dataTable = new DataTable();
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@email", login.Email));
            _adapter.SelectCommand.Parameters.Add(new SqlParameter("@password", login.Password));
            DataTable vn = new ();
            _adapter.Fill(vn);
            if (vn.Rows.Count > 0)
            {

                if (vn.Rows[0][0].ToString() == login.Email && vn.Rows[0][1].ToString() == login.Password)
                {

                    UserModel user = new UserModel();
                    user.Name = vn.Rows[0][2].ToString();
                    user.Id = vn.Rows[0][3].ToString();
                    user.FuncId = Convert.ToInt32(vn.Rows[0][4]);
                    return user;
                }
            }
            return null;
        }
    }
}
