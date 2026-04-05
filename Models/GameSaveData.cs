using System.Collections.Generic;
using System.Runtime.Serialization;
using YouthMeadowGeneralStore.Configuration;

namespace YouthMeadowGeneralStore.Models
{
    [DataContract]
    public sealed class GameSaveData
    {
        [DataMember(Name = "money")]
        public decimal Money { get; set; }

        [DataMember(Name = "warehouse")]
        public List<Product> Warehouse { get; set; } = new List<Product>();

        [DataMember(Name = "shelf_slots")]
        public List<ShelfSlot> ShelfSlots { get; set; } = new List<ShelfSlot>();

        [DataMember(Name = "current_date")]
        public string CurrentDate { get; set; } = GameAppConfig.DefaultCurrentDate;

        [DataMember(Name = "bg_music")]
        public string BackgroundMusic { get; set; } = GameAppConfig.DefaultBackgroundTrack;

        [DataMember(Name = "matchmaking_done")]
        public bool MatchmakingDone { get; set; }

        [DataMember(Name = "relationship_established")]
        public bool RelationshipEstablished { get; set; }

        [DataMember(Name = "event_520_done")]
        public bool Event520Done { get; set; }
    }
}
