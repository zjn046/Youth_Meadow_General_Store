using System.Collections.Generic;
using System.Linq;
using YouthMeadowGeneralStore.Configuration;

namespace YouthMeadowGeneralStore
{
    public partial class MainForm
    {
        private void InitializeEventDefinitions()
        {
            var randomEventDefinitions = BuildRandomEventDefinitions().ToList();
            _priorityRandomEventDefinitions = randomEventDefinitions
                .Where(item => item.TriggerChance.HasValue)
                .ToList();
            _generalRandomEventDefinitions = randomEventDefinitions
                .Where(item => !item.TriggerChance.HasValue)
                .ToList();
            _specialDateEventDefinitions = BuildSpecialDateEventDefinitions().ToList();
        }

        private IEnumerable<RandomEventDefinition> BuildRandomEventDefinitions()
        {
            return new[]
            {
                new RandomEventDefinition(0, _ => true, HandleEvent00),
                new RandomEventDefinition(1, _ => true, HandleEvent01),
                new RandomEventDefinition(2, _ => true, HandleEvent02),
                new RandomEventDefinition(3, _ => true, HandleEvent03),
                new RandomEventDefinition(4, _ => true, HandleEvent04),
                new RandomEventDefinition(5, _ => true, HandleEvent05),
                new RandomEventDefinition(6, _ => true, HandleEvent06),
                new RandomEventDefinition(7, _ => true, HandleEvent07),
                new RandomEventDefinition(8, _ => true, HandleEvent08),
                new RandomEventDefinition(9, _ => true, HandleEvent09),
                new RandomEventDefinition(10, _ => true, HandleEvent10),
                new RandomEventDefinition(11, _ => true, HandleEvent11),
                new RandomEventDefinition(12, _ => true, HandleEvent12),
                new RandomEventDefinition(13, _ => true, HandleEvent13),
                new RandomEventDefinition(14, _ => true, HandleEvent14),
                new RandomEventDefinition(15, _ => _money >= 100000m, HandleEvent15),
                new RandomEventDefinition(16, date => (date.Month == 8 && date.Day >= 20) || (date.Month == 9 && date.Day == 1), HandleEvent16),
                new RandomEventDefinition(17, _ => true, HandleEvent17),
                new RandomEventDefinition(18, _ => true, HandleEvent18),
                new RandomEventDefinition(19, _ => true, HandleEvent19),
                new RandomEventDefinition(20, _ => true, HandleEvent20),
                new RandomEventDefinition(21, _ => true, HandleEvent21),
                new RandomEventDefinition(22, _ => true, HandleEvent22),
                new RandomEventDefinition(23, _ => true, HandleEvent23),
                new RandomEventDefinition(24, _ => true, HandleEvent24),
                new RandomEventDefinition(25, _ => _money >= 10000m, HandleEvent25),
                new RandomEventDefinition(26, _ => true, HandleEvent26),
                new RandomEventDefinition(28, _ => ShouldTriggerTobaccoEvent(), HandleEvent28, 0.1)
            };
        }

        private IEnumerable<SpecialDateEventDefinition> BuildSpecialDateEventDefinitions()
        {
            return new[]
            {
                new SpecialDateEventDefinition("ValentineDay", date => date.Month == 2 && date.Day == 14, HandleValentineDayAsync),
                new SpecialDateEventDefinition("NewYearSale", date => date.Month == 1 && date.Day >= 25 && date.Day <= 31 && _random.NextDouble() < 0.1, HandleNewYearSaleAsync),
                new SpecialDateEventDefinition("FlowerMarket", date => date.Month == 1 && date.Day >= 21 && date.Day <= 31 && _random.NextDouble() < 0.1, HandleFlowerMarketAsync),
                new SpecialDateEventDefinition("Matchmaking", date => date.Month == 2 && date.Day >= 1 && date.Day <= 13 && !_matchmakingDone && _random.NextDouble() < 0.1, HandleMatchmakingAsync),
                new SpecialDateEventDefinition("Romantic520", date => date.Month == 5 && date.Day == 20 && _relationshipEstablished && !_event520Done, Handle520Async)
            };
        }
    }
}
