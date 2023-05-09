﻿using Office.Models;
using System.Data.SqlClient;
using System.Data;

namespace Office.Dataset
{

    public class AnexoDataSet 
    {
        static SqlConnection? _connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["_connection"].ConnectionString);
        static SqlDataAdapter? _adapter;
        static DataTable? _dataTable;

        
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