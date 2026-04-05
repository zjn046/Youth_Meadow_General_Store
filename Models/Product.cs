using System.Runtime.Serialization;

namespace YouthMeadowGeneralStore.Models
{
    [DataContract]
    public sealed class Product
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "buy")]
        public decimal Buy { get; set; }

        [DataMember(Name = "sell")]
        public decimal Sell { get; set; }

        [DataMember(Name = "festival")]
        public bool Festival { get; set; }

        public Product Clone()
        {
            return new Product
            {
                Name = Name,
                Buy = Buy,
                Sell = Sell,
                Festival = Festival
            };
        }
    }
}
