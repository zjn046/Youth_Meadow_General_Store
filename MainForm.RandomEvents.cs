using System.Linq;
using YouthMeadowGeneralStore.Configuration;
using YouthMeadowGeneralStore.Models;

namespace YouthMeadowGeneralStore
{
    public partial class MainForm
    {
        private async void TriggerRandomEvent()
        {
            if (!MonthDay.TryParse(_currentDate, out var currentDate))
            {
                currentDate = new MonthDay(1, 1);
            }

            if (await HandleSpecialDateEventsAsync(currentDate))
            {
                return;
            }

            RandomEventDefinition selectedEvent = null;
            foreach (var priorityEvent in _priorityRandomEventDefinitions.Where(item => item.IsAvailable(currentDate)))
            {
                if (priorityEvent.TriggerChance.HasValue && _random.NextDouble() < priorityEvent.TriggerChance.Value)
                {
                    selectedEvent = priorityEvent;
                    break;
                }
            }

            if (selectedEvent == null)
            {
                var availableEvents = _generalRandomEventDefinitions
                    .Where(item => item.IsAvailable(currentDate))
                    .ToList();

                if (availableEvents.Count == 0)
                {
                    return;
                }

                selectedEvent = availableEvents[_random.Next(availableEvents.Count)];
            }

            if (!selectedEvent.Execute())
            {
                return;
            }

            CheckBankruptcy();
            if (_bankruptcyTriggered || IsDisposed || Disposing)
            {
                return;
            }

            UpdateMoneyLabel();
            _money -= 100m;
            await EndBusinessDayAsync();
        }

        private void AddWarehouseItems(string name, decimal buy, decimal sell, int quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                _warehouse.Add(new Product { Name = name, Buy = buy, Sell = sell });
            }
        }
    }
}
