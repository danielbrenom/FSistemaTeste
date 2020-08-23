using System.Collections.Generic;
using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        protected DaoBeneficiario cli;

        public BoBeneficiario(DaoBeneficiario daoBeneficiario)
        {
            cli = daoBeneficiario;
        }
        /// <summary>
        /// Adiciona novo beneficiario
        /// </summary>
        /// <param name="beneficiario">O objeto do beneficiario</param>
        /// <returns></returns>
        public long Incluir(Beneficiario beneficiario)
        {
            return cli.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiario
        /// </summary>
        /// <param name="beneficiario">O objeto para alterar</param>
        public void Alterar(Beneficiario beneficiario)
        {
            cli.Alterar(beneficiario);
        }

        /// <summary>
        /// Consulta um beneficiário
        /// </summary>
        /// <param name="Id">O id do beneficiário</param>
        /// <returns></returns>
        public Beneficiario Consultar(long Id)
        {
            return cli.Consultar(Id);
        }

        /// <summary>
        /// Exlui um beneficiario
        /// </summary>
        /// <param name="Id">O ID para exluir</param>
        public void Excluir(long Id)
        {
            cli.Excluir(Id);
        }

        /// <summary>
        /// Listar os beneficiarios
        /// </summary>
        /// <returns>A lista de beneficiários</returns>
        public List<Beneficiario> Listar(long Id)
        {
            return cli.Listar(Id);
        }

        /// <summary>
        /// Verifica a existência de um beneficiário com o CPF
        /// </summary>
        /// <param name="CPF">O cpf para buscar</param>
        /// <returns></returns>
        public bool VerificaExistencia(string CPF)
        {
            return cli.VerificarExistencia(CPF);
        }
    }
}
