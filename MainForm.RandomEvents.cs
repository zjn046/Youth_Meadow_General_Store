using System.Collections.Generic;
using System.Linq;
using YouthMeadowGeneralStore.Models;

namespace YouthMeadowGeneralStore
{
    public partial class MainForm
    {
        private async void TriggerRandomEvent()
        {
            if (!TryParseCurrentDate(out var month, out var day))
            {
                month = 1;
                day = 1;
            }

            if (await HandleSpecialDateEventsAsync(month, day))
            {
                return;
            }

            int eventIndex;
            if (ShouldTriggerTobaccoEvent() && _random.NextDouble() < 0.1)
            {
                eventIndex = 28;
            }
            else
            {
                var possibleEvents = Enumerable.Range(0, 27).ToList();
                if (_money < 100000m && possibleEvents.Contains(15))
                {
                    possibleEvents.Remove(15);
                }
                else if (_money < 10000m && possibleEvents.Contains(25))
                {
                    possibleEvents.Remove(25);
                }

                if (!((month == 8 && day >= 20) || (month == 9 && day == 1)))
                {
                    possibleEvents.Remove(16);
                }

                eventIndex = possibleEvents[_random.Next(possibleEvents.Count)];
            }

            if (!HandleGeneralEvent(eventIndex))
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

        private bool HandleGeneralEvent(int eventIndex)
        {
            switch (eventIndex)
            {
                case 0: return HandleEvent00();
                case 1: return HandleEvent01();
                case 2: return HandleEvent02();
                case 3: return HandleEvent03();
                case 4: return HandleEvent04();
                case 5: return HandleEvent05();
                case 6: return HandleEvent06();
                case 7: return HandleEvent07();
                case 8: return HandleEvent08();
                case 9: return HandleEvent09();
                case 10: return HandleEvent10();
                case 11: return HandleEvent11();
                case 12: return HandleEvent12();
                case 13: return HandleEvent13();
                case 14: return HandleEvent14();
                case 15: return HandleEvent15();
                case 16: return HandleEvent16();
                case 17: return HandleEvent17();
                case 18: return HandleEvent18();
                case 19: return HandleEvent19();
                case 20: return HandleEvent20();
                case 21: return HandleEvent21();
                case 22: return HandleEvent22();
                case 23: return HandleEvent23();
                case 24: return HandleEvent24();
                case 25: return HandleEvent25();
                case 26: return HandleEvent26();
                case 28: return HandleEvent28();
                default: return true;
            }
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
