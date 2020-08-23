using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FI.AtividadeEntrevista.DAL.Padrao;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.DAL.Beneficiarios
{
    public class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal long Incluir(Beneficiario beneficiario)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("Nome", beneficiario.Nome),
                new SqlParameter("CPF", beneficiario.CPF),
                new SqlParameter("IdCliente", beneficiario.IdCliente)
            };
            var verifica = VerificarExistencia(beneficiario.CPF);
            if (verifica)
                throw new Exception("CPF já cadastrado");
            var ds = base.Consultar("FI_SP_IncBeneficiario", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Consulta um beneficiario
        /// </summary>
        /// <param name="Id">Id do beneficiario</param>
        internal Beneficiario Consultar(long Id)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("Id", Id)
            };
            var ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            var cli = Converter(ds);
            return cli.FirstOrDefault();
        }

        /// <summary>
        /// Verifica se o CPF já existe na tabela
        /// </summary>
        /// <param name="CPF">O CPF para buscar</param>
        /// <returns></returns>
        internal bool VerificarExistencia(string CPF)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("CPF", CPF)
            };
            var ds = base.Consultar("FI_SP_VerificaBeneficiario", parametros);
            return ds.Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<Beneficiario> Listar(long clientId)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("Id", clientId)
            };
            var ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            var cli = Converter(ds);
            return cli;
        }

        /// <summary>
        /// Altera os dados de um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(Beneficiario beneficiario)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("Nome", beneficiario.Nome),
                new SqlParameter("CPF", beneficiario.CPF),
                new SqlParameter("Id", beneficiario.Id)
            };
            Executar("FI_SP_AltBeneficiario", parametros);
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
            Executar("FI_SP_DelBeneficiario", parametros);
        }

        private static List<Beneficiario> Converter(DataSet ds)
        {
            var lista = new List<Beneficiario>();
            if (ds?.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return lista;
            lista.AddRange(from DataRow row in ds.Tables[0].Rows
                select new Beneficiario
                {
                    Id = row.Field<long>("Id"), IdCliente = row.Field<long>("IdCliente"),
                    Nome = row.Field<string>("Nome"), CPF = row.Field<string>("CPF")
                });
            return lista;
        }
    }
}