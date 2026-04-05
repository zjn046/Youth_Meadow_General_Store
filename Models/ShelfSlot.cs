using System.Runtime.Serialization;

namespace YouthMeadowGeneralStore.Models
{
    [DataContract]
    public sealed class ShelfSlot
    {
        [DataMember(Name = "unlocked")]
        public bool Unlocked { get; set; }

        [DataMember(Name = "product")]
        public Product Product { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        [DataMember(Name = "unlock_cost")]
        public decimal UnlockCost { get; set; }

        public ShelfSlot Clone()
        {
            return new ShelfSlot
            {
                Unlocked = Unlocked,
                Product = Product?.Clone(),
                Quantity = Quantity,
                UnlockCost = UnlockCost
            };
        }
    }
}
