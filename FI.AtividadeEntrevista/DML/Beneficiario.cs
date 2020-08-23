namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Modelo de representação de um beneficiário na tabela de Beneficiarios
    /// </summary>
    public class Beneficiario
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// IdCliente
        /// </summary>
        public long IdCliente { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// CPF
        /// </summary>
        public string CPF {get;set;}
    }
}
