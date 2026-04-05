using System;
using System.Collections.Generic;
using System.Linq;
using YouthMeadowGeneralStore.Models;

namespace YouthMeadowGeneralStore.Configuration
{
    public static class GameContentConfig
    {
        public static IReadOnlyList<FestivalCatalogEntry> FestivalCatalog { get; } = new List<FestivalCatalogEntry>
        {
            new FestivalCatalogEntry(
                date => date.Month == 2 || date.Month == 3 || date.Month == 4,
                new[]
                {
                    CreateProduct("麻辣春笋", 3m, 5m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 2 && date.Day == 1,
                    "春笋上市",
                    "春回大地，萬物復蘇，許多春笋開始生長了，麻辣春笋上市啦，趕快進貨吧。")),
            new FestivalCatalogEntry(
                date => date.Month >= 4 && date.Month <= 8,
                new[]
                {
                    CreateProduct("农家西瓜", 3m, 5m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 5 && date.Day == 1,
                    "西瓜上市",
                    "夏天到啦，天气炎热，来一点农家自己种的西瓜吧。")),
            new FestivalCatalogEntry(
                date => date.Month >= 9 && date.Month <= 11,
                new[]
                {
                    CreateProduct("农家特产腊肉礼盒", 33m, 88m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 9 && date.Day == 1,
                    "腊肉上市",
                    "秋风起，农家自己晒的特产腊肉上市啦，赶快进货吧。")),
            new FestivalCatalogEntry(
                date => date.Month == 12 || date.Month == 1,
                new[]
                {
                    CreateProduct("农家高粱酒", 15m, 30m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 12 && date.Day == 1,
                    "米酒上市",
                    "冬季来临，天气变得十分寒冷，农家自己酿的米酒可以驱寒保暖，要来一点吗？")),
            new FestivalCatalogEntry(
                date => date.Month == 8 && date.Day >= 1 && date.Day <= 5,
                new[]
                {
                    CreateProduct("五星红旗", 3m, 5m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 8 && date.Day == 1,
                    "建军节到啦",
                    "铁血军魂，卫国无疆！八一建军节到了。\n八一建军节商品已上架，记得进货五星红旗！",
                    "建军节.mp3")),
            new FestivalCatalogEntry(
                date => date.Month == 9 && date.Day >= 1 && date.Day <= 5,
                new[]
                {
                    CreateProduct("书包", 20m, 30m, true),
                    CreateProduct("笔袋", 8m, 12m, true),
                    CreateProduct("尺子", 3m, 5m, true),
                    CreateProduct("圆珠笔", 3m, 5m, true),
                    CreateProduct("橡皮擦", 3m, 8m, true),
                    CreateProduct("小天才平板电脑", 888m, 1888m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 9 && date.Day == 1,
                    "开学啦",
                    "重返校园，告别往日的懵懂与懈怠，于知识的沃土里扎根生长，在思维的磨砺中褪去青涩外壳，向着明亮未来破茧成蝶，开启熠熠生辉的成长新篇章。开学日道了，你准备好了吗？",
                    "开学.mp3")),
            new FestivalCatalogEntry(
                date => date.Month == 9 && date.Day >= 5 && date.Day <= 15,
                new[]
                {
                    CreateProduct("康乃馨", 5m, 10m, true),
                    CreateProduct("祝福贺卡", 2m, 3m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 9 && date.Day == 5,
                    "教师节到啦",
                    "老师啊老师你像我兄长，老师啊老师像好朋友一样，教师节到了，不少学生都准备了礼物，表达对老师的爱戴之情。\n教师节商品已上架，康乃馨和贺卡热卖中！别忘了把你精心准备的礼物送给你心爱的老师哦",
                    "教师节.mp3")),
            new FestivalCatalogEntry(
                date => date.Month == 10 && date.Day >= 6 && date.Day <= 10,
                new[]
                {
                    CreateProduct("广州酒家月饼礼盒", 80m, 188m, true),
                    CreateProduct("陶陶居月饼", 10m, 15m, true),
                    CreateProduct("赣南脐橙礼盒", 30m, 50m, true),
                    CreateProduct("苹果", 3m, 5m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 10 && (date.Day == 10 || date.Day == 15),
                    "中秋节到啦",
                    "明月几时有，把酒问青天。不知天上宫阙，京西适合年。中秋节就要到了。\n中秋佳节商品已上架，多种礼盒可选！别忘了去商品列表购买并上架商品哦！",
                    "中秋节.mp3")),
            new FestivalCatalogEntry(
                date => date.Month == 10 && date.Day >= 1 && date.Day <= 7,
                new[]
                {
                    CreateProduct("五星红旗", 3m, 5m, true),
                    CreateProduct("红星笔袋", 6m, 10m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 10 && (date.Day == 1 || date.Day == 10),
                    "国庆节到啦",
                    "九州同庆山河壮，万民共欢岁月新。赤旗漫卷迎盛世，家国同辉耀星辰。国庆节到了，大家在享受黄金周假期的同时也歌颂着祖国的伟大。\n国庆节爱国商品已上架！别忘了放入仓库哦",
                    "国庆节.mp3")),
            new FestivalCatalogEntry(
                date => (date.Month == 10 && date.Day >= 31) || (date.Month == 11 && date.Day == 2),
                new[]
                {
                    CreateProduct("南瓜头", 5m, 15m, true),
                    CreateProduct("七彩糖果", 5m, 10m, true)
                },
                new FestivalAnnouncement(
                    date => date.Day == 31 || date.Day == 10,
                    "万圣节到啦",
                    "不给糖，就捣蛋，万圣狂欢嗨翻。万圣节到了，你准备好了吗。\n万圣节搞怪商品已上架！",
                    "万圣节.mp3")),
            new FestivalCatalogEntry(
                date => date.Month == 12 && date.Day == 25,
                new[]
                {
                    CreateProduct("圣诞树", 10m, 20m, true),
                    CreateProduct("圣诞帽", 5m, 10m, true),
                    CreateProduct("巧克力礼盒", 30m, 66m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 12 && date.Day == 25,
                    "圣诞节到啦",
                    "雪橇铃儿清脆作响，穿越雪幕；璀璨星光点亮夜空，指引方向。圣诞之际，愿绮梦飞扬，欢乐满溢，开启一场梦幻的西方节庆之旅。\n圣诞节日商品已上架，营造节日氛围！",
                    "圣诞节.mp3")),
            new FestivalCatalogEntry(
                date => date.Month == 2 && date.Day >= 1 && date.Day <= 14,
                new[]
                {
                    CreateProduct("大地红", 8m, 15m, true),
                    CreateProduct("加特林", 50m, 100m, true),
                    CreateProduct("夜明珠", 15m, 30m, true),
                    CreateProduct("仙女棒", 2m, 5m, true),
                    CreateProduct("二踢脚", 8m, 12m, true),
                    CreateProduct("沙泡", 1m, 2m, true),
                    CreateProduct("瓜子", 5m, 8m, true),
                    CreateProduct("花生", 5m, 8m, true),
                    CreateProduct("煌上煌礼盒", 100m, 188m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 2 && date.Day == 1,
                    "春节到啦",
                    "剪一纸窗花，裁一段春光；燃几簇烟火，聚几分热望。新春至，亲友相拥，把幸福珍藏。春节到啦。\n春节年货已上架，红红火火过新年！",
                    "春节0.mp3")),
            new FestivalCatalogEntry(
                date => date.Month == 2 && date.Day == 14,
                new[]
                {
                    CreateProduct("郁金香", 5m, 10m, true),
                    CreateProduct("玫瑰花", 8m, 15m, true),
                    CreateProduct("永生花", 30m, 52m, true),
                    CreateProduct("巧克力", 50m, 100m, true),
                    CreateProduct("钻石戒指", 3000m, 6000m, true),
                    CreateProduct("黄金手镯", 20000m, 30000m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 2 && date.Day == 14,
                    "情人节到啦",
                    "爱意不打折，浪漫不缺席。心动无需挑日子，今天偏爱多一点。与你共赴，这人间限定浪漫。以爱之名，定格岁岁年年。",
                    "情人节.mp3")),
            new FestivalCatalogEntry(
                date => date.Month == 2,
                new[]
                {
                    CreateProduct("汤圆", 5m, 10m, true),
                    CreateProduct("团员烟花大礼盒", 500m, 888m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 2 && date.Day == 15,
                    "元宵节到啦",
                    "花灯耀元宵，团圆乐逍遥。元宵节就要到了。村里张灯结彩，十分热闹。",
                    "元宵节.mp3")),
            new FestivalCatalogEntry(
                date => date.Month == 3,
                new[]
                {
                    CreateProduct("红枣", 5m, 10m, true),
                    CreateProduct("小米米家保温杯", 66m, 99m, true),
                    CreateProduct("香奈儿香水", 500m, 520m, true),
                    CreateProduct("Lv包包", 666m, 888m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 3 && date.Day == 8,
                    "妇女节到啦",
                    "春风如你，熠熠芳华。三八节，致每一位了不起的她，愿生活常温柔以待，愿梦想皆如愿以偿。",
                    "妇女节.mp3")),
            new FestivalCatalogEntry(
                date => date.Month == 5 && date.Day == 11,
                new[]
                {
                    CreateProduct("康乃馨", 2m, 100m, true),
                    CreateProduct("萱草花", 1m, 5m, true),
                    CreateProduct("永远年轻护肤品", 500m, 88m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 5 && date.Day == 11,
                    "母亲节到啦",
                    "【妈妈，是比月光更早的温柔】  \n萱草香漫过窗台时，总想起您藏进毛衣针脚的絮叨，  \n把清晨熬成粥，把暮色缝成我远行的背包。\n\n后来世界很大，我却总在风起的路口攥紧衣角——\n原来那些被您熨烫过的岁月，早已裹着月光，\n在我心里长成一片永不枯萎的春潮。\n\n母亲节快乐，我的时光魔术师。",
                    "母亲节.mp3")),
            new FestivalCatalogEntry(
                date => date.Month == 6 && date.Day == 1,
                new[]
                {
                    CreateProduct("旺旺零食大礼包", 50m, 88m, true),
                    CreateProduct("小天才电话手表", 800m, 999m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 6 && date.Day == 1,
                    "儿童节到啦",
                    "儿童节到啦，孩子们兴奋不已，都在等待着父母的礼物。\n儿童节商品已上架，别忘了为孩子们挑选心仪的礼物哦！",
                    "儿童节.mp3")),
            new FestivalCatalogEntry(
                date => date.Month == 6 && date.Day == 17,
                new[]
                {
                    CreateProduct("花花公子休闲衬衫", 60m, 80m, true),
                    CreateProduct("爱健康运动手表", 200m, 399m, true),
                    CreateProduct("凤凰丹虫", 350m, 500m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 6 && date.Day == 17,
                    "父亲节到啦",
                    "【沉默的山水】\n\n您把岁月泡进搪瓷杯，\n烟灰缸盛过我的叛逆，\n却始终沉默如一座山。\n\n后来我数遍世界的棱角，\n才读懂您藏在旧手表里的涛声——\n那秒针走过的每道褶皱，\n都是未说出口的“回家吃饭”。\n\n父亲节到啦。\n父亲节商品已上架，快为爸爸挑选一份特别的礼物吧！",
                    "父亲节.mp3")),
            new FestivalCatalogEntry(
                date => date.Month == 5 && date.Day == 31,
                new[]
                {
                    CreateProduct("咸肉粽", 6m, 8m, true),
                    CreateProduct("艾包", 5m, 6m, true),
                    CreateProduct("雄黄酒", 98m, 198m, true)
                },
                new FestivalAnnouncement(
                    date => date.Month == 5 && date.Day == 31,
                    "端午节到啦",
                    "端午佳节翩然而至，龙舟竞渡水花激扬，于锣鼓喧天中传承千年民俗记忆，续写华夏古韵新章。\n端午节商品已上架，快去为端午添购应景好货吧！",
                    "端午节.mp3"))
        };

        public static List<Product> CreateBaseProducts()
        {
            return new List<Product>
            {
                CreateProduct("雪碧", 1m, 3m),
                CreateProduct("可乐", 1m, 3m),
                CreateProduct("笔记本", 1m, 2m),
                CreateProduct("钢笔", 1m, 2m),
                CreateProduct("旺仔牛奶", 1m, 4m),
                CreateProduct("苏打饼", 2m, 4m),
                CreateProduct("乐视薯片", 2m, 3m),
                CreateProduct("白象方便面", 2m, 4m),
                CreateProduct("文件袋", 2m, 3m),
                CreateProduct("王老吉", 2m, 4m),
                CreateProduct("早餐面包", 2m, 5m),
                CreateProduct("果粒橙", 2m, 5m),
                CreateProduct("珠江啤酒", 5m, 8m),
                CreateProduct("红塔山", 7m, 10m),
                CreateProduct("二锅头", 8m, 10m),
                CreateProduct("黄鹤楼", 15m, 20m),
                CreateProduct("合金甩棍", 20m, 30m),
                CreateProduct("菜刀", 20m, 35m),
                CreateProduct("软中华", 60m, 75m)
            };
        }

        private static Product CreateProduct(string name, decimal buy, decimal sell, bool festival = false)
        {
            return new Product
            {
                Name = name,
                Buy = buy,
                Sell = sell,
                Festival = festival
            };
        }
    }

    public sealed class FestivalCatalogEntry
    {
        public FestivalCatalogEntry(Func<MonthDay, bool> isActive, IEnumerable<Product> products, FestivalAnnouncement announcement = null)
        {
            IsActive = isActive;
            Products = products.ToList();
            Announcement = announcement;
        }

        public Func<MonthDay, bool> IsActive { get; }

        public IReadOnlyList<Product> Products { get; }

        public FestivalAnnouncement Announcement { get; }
    }

    public sealed class FestivalAnnouncement
    {
        public FestivalAnnouncement(Func<MonthDay, bool> shouldAnnounce, string title, string message, string backgroundTrack = null)
        {
            ShouldAnnounce = shouldAnnounce;
            Title = title;
            Message = message;
            BackgroundTrack = backgroundTrack;
        }

        public Func<MonthDay, bool> ShouldAnnounce { get; }

        public string Title { get; }

        public string Message { get; }

        public string BackgroundTrack { get; }
    }
}
