using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FI.AtividadeEntrevista.DAL.Padrao
{
    public class AcessoDados
    {
        private static string StringDeConexao
        {
            get
            {
                var conn = ConfigurationManager.ConnectionStrings["BancoDeDados"];
                return conn != null ? conn.ConnectionString : string.Empty;
            }
        }

        internal void Executar(string NomeProcedure, IEnumerable<SqlParameter> parametros)
        {
            var comando = new SqlCommand();
            var conexao = new SqlConnection(StringDeConexao);
            comando.Connection = conexao;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = NomeProcedure;
            foreach (var item in parametros)
                comando.Parameters.Add(item);
            conexao.Open();
            try
            {
                comando.ExecuteNonQuery();
            }
            finally
            {
                conexao.Close();
            }
        }

        internal DataSet Consultar(string NomeProcedure, List<SqlParameter> parametros)
        {
            var comando = new SqlCommand();
            var conexao = new SqlConnection(StringDeConexao);

            comando.Connection = conexao;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = NomeProcedure;
            foreach (var item in parametros)
                comando.Parameters.Add(item);

            var adapter = new SqlDataAdapter(comando);
            var ds = new DataSet();
            conexao.Open();
            try
            {
                adapter.Fill(ds);
            }
            finally
            {
                conexao.Close();
            }

            return ds;
        }
    }
}