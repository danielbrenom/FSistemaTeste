using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Modelo de Beneficiario para inserção 
    /// </summary>
    public class BeneficiarioModel
    {
        /// <summary>
        /// Id do Beneficiario cadastrado
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Nome do beneficiario
        /// </summary>
        [Required]
        public string Nome { get; set; }
        /// <summary>
        /// CPF do beneficiario
        /// </summary>
        [Required]
        public string CPF { get; set; }
        /// <summary>
        /// Id do Cliente para vincular
        /// </summary>
        [Required]
        public long IdCliente { get; set; }
    }
}