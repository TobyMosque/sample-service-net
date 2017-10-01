using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace TobyMosque.Sample.Service.Net.Models
{
    [DataContract]
    public class ResourceModel : IValidatableObject
    {
        [DataMember(Order = 1)]
        public Guid? ResourceID { get; set; }

        [DataMember(Order = 1)]
        [Required(ErrorMessage = "O Campo Descrição é obrigatorio")]
        public string Description { get; set; }

        [DataMember(Order = 1)]
        public string Observation { get; set; }

        [DataMember(Order = 1)]
        [Range(1, Int32.MaxValue, ErrorMessage = "O Campo Quantidade é obrigatorio e deve ser maior que 0")]
        public int Quantity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}
