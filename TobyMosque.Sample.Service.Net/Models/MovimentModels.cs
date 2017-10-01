using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TobyMosque.Sample.Service.Net.Models
{
    [DataContract]
    public class MovimentModel
    {
        [DataMember(Order = 1)]
        public Guid? MovimentID { get; set; }

        [DataMember(Order = 2)]
        public Guid ResourceID { get; set; }

        [DataMember(Order = 3)]
        public Guid UserID { get; set; }

        [DataMember(Order = 4)]
        public DataEntities.Enums.MovimentType MovimentTypeID { get; set; }

        [DataMember(Order = 5)]
        [Range(1, Int32.MaxValue, ErrorMessage = "O Campo Quantidade é obrigatorio e deve ser maior que 0")]
        public int Quantity { get; set; }

        [DataMember(Order = 6)]
        public string UserNome { get; set; }

        [DataMember(Order = 7)]
        public Boolean IsOwner { get; set; }
    }

    [DataContract]
    public class UserMovimentsByTenantModel
    {
        [DataMember(Order = 1)]
        public UserModel User { get; set; }

        [DataMember(Order = 2)]
        public ResourceModel Resource { get; set; }

        [DataMember(Order = 3)]
        public List<MovimentModel> Moviments { get; set; }

        [DataMember(Order = 4)]
        public List<DataEntities.Domain.MovimentType> MovimentTypes { get; set; }
    }

    [DataContract]
    public class MovimentStockModel
    {
        [DataMember(Order = 1)]
        public Guid MovimentId { get; set; }

        [DataMember(Order = 2)]
        public int NewResourceStock { get; set; }
    }
}
