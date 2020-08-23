using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FI.AtividadeEntrevista.DAL.Padrao;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.DAL.Clientes
{
    /// <summary>
    /// Classe de acesso a dados de Cliente
    /// </summary>
    public class DaoCliente : AcessoDados
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal long Incluir(Cliente cliente)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("Nome", cliente.Nome),
                new SqlParameter("CPF", cliente.CPF),
                new SqlParameter("Sobrenome", cliente.Sobrenome),
                new SqlParameter("Nacionalidade", cliente.Nacionalidade),
                new SqlParameter("CEP", cliente.CEP),
                new SqlParameter("Estado", cliente.Estado),
                new SqlParameter("Cidade", cliente.Cidade),
                new SqlParameter("Logradouro", cliente.Logradouro),
                new SqlParameter("Email", cliente.Email),
                new SqlParameter("Telefone", cliente.Telefone)
            };

            if (VerificarExistencia(cliente.CPF))
            {
                throw new Exception("CPF já cadastrado");
            }

            var ds = base.Consultar("FI_SP_IncClienteV2", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Consulta um cliente
        /// </summary>
        /// <param name="Id">Id do cliente</param>
        internal Cliente Consultar(long Id)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("Id", Id)
            };
            var ds = base.Consultar("FI_SP_ConsCliente", parametros);
            var cli = Converter(ds);
            return cli.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("CPF", CPF)
            };
            var ds = base.Consultar("FI_SP_VerificaCliente", parametros);
            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente,
            out int qtd)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("iniciarEm", iniciarEm),
                new SqlParameter("quantidade", quantidade),
                new SqlParameter("campoOrdenacao", campoOrdenacao),
                new SqlParameter("crescente", crescente)
            };
            var ds = base.Consultar("FI_SP_PesqCliente", parametros);
            var cli = Converter(ds);
            var iQtd = 0;
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);
            qtd = iQtd;
            return cli;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<Cliente> Listar()
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("Id", 0)
            };
            var ds = base.Consultar("FI_SP_ConsCliente", parametros);
            var cli = Converter(ds);
            return cli;
        }

        /// <summary>
        /// Altera os dados de um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(Cliente cliente)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("Nome", cliente.Nome),
                new SqlParameter("CPF", cliente.CPF),
                new SqlParameter("Sobrenome", cliente.Sobrenome),
                new SqlParameter("Nacionalidade", cliente.Nacionalidade),
                new SqlParameter("CEP", cliente.CEP),
                new SqlParameter("Estado", cliente.Estado),
                new SqlParameter("Cidade", cliente.Cidade),
                new SqlParameter("Logradouro", cliente.Logradouro),
                new SqlParameter("Email", cliente.Email),
                new SqlParameter("Telefone", cliente.Telefone),
                new SqlParameter("ID", cliente.Id)
            };
            Executar("FI_SP_AltCliente", parametros);
        }


        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Excluir(long Id)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("Id", Id)
            };
            Executar("FI_SP_DelCliente", parametros);
        }

        private static List<Cliente> Converter(DataSet ds)
        {
            var lista = new List<Cliente>();
            if (ds?.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return lista;
            lista.AddRange(from DataRow row in ds.Tables[0].Rows
                select new Cliente
                {
                    Id = row.Field<long>("Id"),
                    CEP = row.Field<string>("CEP"),
                    Cidade = row.Field<string>("Cidade"),
                    Email = row.Field<string>("Email"),
                    Estado = row.Field<string>("Estado"),
                    Logradouro = row.Field<string>("Logradouro"),
                    Nacionalidade = row.Field<string>("Nacionalidade"),
                    Nome = row.Field<string>("Nome"),
                    CPF = row.Field<string>("Cpf"),
                    Sobrenome = row.Field<string>("Sobrenome"),
                    Telefone = row.Field<string>("Telefone")
                });
            return lista;
        }
    }
}