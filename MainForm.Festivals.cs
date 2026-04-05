using System.Linq;
using YouthMeadowGeneralStore.Configuration;

namespace YouthMeadowGeneralStore
{
    public partial class MainForm
    {
        private void CheckFestivals(bool init = false)
        {
            if (!MonthDay.TryParse(_currentDate, out var currentDate))
            {
                currentDate = new MonthDay(7, 1);
            }

            var tempProducts = CreateBaseProducts();
            foreach (var festivalEntry in GameContentConfig.FestivalCatalog.Where(item => item.IsActive(currentDate)))
            {
                tempProducts.AddRange(festivalEntry.Products.Select(product => product.Clone()));
                if (init || festivalEntry.Announcement == null || !festivalEntry.Announcement.ShouldAnnounce(currentDate))
                {
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(festivalEntry.Announcement.BackgroundTrack))
                {
                    TryPlayBackground(festivalEntry.Announcement.BackgroundTrack);
                }

                ShowInfo(festivalEntry.Announcement.Message, festivalEntry.Announcement.Title);
            }

            _productList = tempProducts;
            if (GameAppConfig.BackgroundTrackSchedule.TryGetValue(currentDate, out var backgroundTrack))
            {
                SetBackgroundTrack(backgroundTrack, !init);
            }
        }
    }
}
