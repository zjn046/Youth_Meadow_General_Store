using System.Collections.Generic;

namespace YouthMeadowGeneralStore
{
    public partial class MainForm
    {
        private void CheckFestivals(bool init = false)
        {
            if (!TryParseCurrentDate(out var month, out var day))
            {
                month = 7;
                day = 1;
            }

            var tempProducts = new List<Models.Product>();
            foreach (var product in CreateBaseProducts())
            {
                tempProducts.Add(product);
            }

            if (month == 2 || month == 3 || month == 4)
            {
                tempProducts.Add(new Models.Product { Name = "麻辣春笋", Buy = 3m, Sell = 5m, Festival = true });
                if (!init && month == 2 && day == 1)
                {
                    ShowInfo("春回大地，萬物復蘇，許多春笋開始生長了，麻辣春笋上市啦，趕快進貨吧。", "春笋上市");
                }
            }

            if (month >= 4 && month <= 8)
            {
                tempProducts.Add(new Models.Product { Name = "农家西瓜", Buy = 3m, Sell = 5m, Festival = true });
                if (!init && month == 5 && day == 1)
                {
                    ShowInfo("夏天到啦，天气炎热，来一点农家自己种的西瓜吧。", "西瓜上市");
                }
            }

            if (month >= 9 && month <= 11)
            {
                tempProducts.Add(new Models.Product { Name = "农家特产腊肉礼盒", Buy = 33m, Sell = 88m, Festival = true });
                if (!init && month == 9 && day == 1)
                {
                    ShowInfo("秋风起，农家自己晒的特产腊肉上市啦，赶快进货吧。", "腊肉上市");
                }
            }

            if (month == 12 || month == 1)
            {
                tempProducts.Add(new Models.Product { Name = "农家高粱酒", Buy = 15m, Sell = 30m, Festival = true });
                if (!init && month == 12 && day == 1)
                {
                    ShowInfo("冬季来临，天气变得十分寒冷，农家自己酿的米酒可以驱寒保暖，要来一点吗？", "米酒上市");
                }
            }

            if (month == 8 && day >= 1 && day <= 5)
            {
                tempProducts.Add(new Models.Product { Name = "五星红旗", Buy = 3m, Sell = 5m, Festival = true });
                if (!init && day == 1)
                {
                    TryPlayBackground("建军节.mp3");
                    ShowInfo("铁血军魂，卫国无疆！八一建军节到了。\n八一建军节商品已上架，记得进货五星红旗！", "建军节到啦");
                }
            }

            if (month == 9 && day >= 1 && day <= 5)
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "书包", Buy = 20m, Sell = 30m, Festival = true },
                    new Models.Product { Name = "笔袋", Buy = 8m, Sell = 12m, Festival = true },
                    new Models.Product { Name = "尺子", Buy = 3m, Sell = 5m, Festival = true },
                    new Models.Product { Name = "圆珠笔", Buy = 3m, Sell = 5m, Festival = true },
                    new Models.Product { Name = "橡皮擦", Buy = 3m, Sell = 8m, Festival = true },
                    new Models.Product { Name = "小天才平板电脑", Buy = 888m, Sell = 1888m, Festival = true }
                });
                if (!init && day == 1)
                {
                    TryPlayBackground("开学.mp3");
                    ShowInfo("重返校园，告别往日的懵懂与懈怠，于知识的沃土里扎根生长 ，在思维的磨砺中褪去青涩外壳，向着明亮未来破茧成蝶，开启熠熠生辉的成长新篇章。开学日道了，你准备好了吗？", "开学啦");
                }
            }

            if (month == 9 && day >= 5 && day <= 15)
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "康乃馨", Buy = 5m, Sell = 10m, Festival = true },
                    new Models.Product { Name = "祝福贺卡", Buy = 2m, Sell = 3m, Festival = true }
                });
                if (!init && day == 5)
                {
                    TryPlayBackground("教师节.mp3");
                    ShowInfo("老师啊老师你像我兄长，老师啊老师像好朋友一样，教师节到了，不少学生都准备了礼物，表达对老师的爱戴之情。\n教师节商品已上架，康乃馨和贺卡热卖中！别忘了把你精心准备的礼物送给你心爱的老师哦", "教师节到啦");
                }
            }

            if (month == 10 && day >= 6 && day <= 10)
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "广州酒家月饼礼盒", Buy = 80m, Sell = 188m, Festival = true },
                    new Models.Product { Name = "陶陶居月饼", Buy = 10m, Sell = 15m, Festival = true },
                    new Models.Product { Name = "赣南脐橙礼盒", Buy = 30m, Sell = 50m, Festival = true },
                    new Models.Product { Name = "苹果", Buy = 3m, Sell = 5m, Festival = true }
                });
                if (!init && (day == 10 || day == 15))
                {
                    TryPlayBackground("中秋节.mp3");
                    ShowInfo("明月几时有，把酒问青天。不知天上宫阙，京西适合年。中秋节就要到了。\n中秋佳节商品已上架，多种礼盒可选！别忘了去商品列表购买并上架商品哦！", "中秋节到啦");
                }
            }

            if (month == 10 && day >= 1 && day <= 7)
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "五星红旗", Buy = 3m, Sell = 5m, Festival = true },
                    new Models.Product { Name = "红星笔袋", Buy = 6m, Sell = 10m, Festival = true }
                });
                if (!init && (day == 1 || day == 10))
                {
                    TryPlayBackground("国庆节.mp3");
                    ShowInfo("九州同庆山河壮，万民共欢岁月新。赤旗漫卷迎盛世，家国同辉耀星辰。国庆节到了，大家在享受黄金周假期的同时也歌颂着祖国的伟大。\n国庆节爱国商品已上架！别忘了放入仓库哦", "国庆节到啦");
                }
            }

            if ((month == 10 && day >= 31) || (month == 11 && day == 2))
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "南瓜头", Buy = 5m, Sell = 15m, Festival = true },
                    new Models.Product { Name = "七彩糖果", Buy = 5m, Sell = 10m, Festival = true }
                });
                if (!init && (day == 31 || day == 10))
                {
                    TryPlayBackground("万圣节.mp3");
                    ShowInfo("不给糖，就捣蛋，万圣狂欢嗨翻。万圣节到了，你准备好了吗。\n万圣节搞怪商品已上架！", "万圣节到啦");
                }
            }

            if (month == 12 && day == 25)
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "圣诞树", Buy = 10m, Sell = 20m, Festival = true },
                    new Models.Product { Name = "圣诞帽", Buy = 5m, Sell = 10m, Festival = true },
                    new Models.Product { Name = "巧克力礼盒", Buy = 30m, Sell = 66m, Festival = true }
                });
                if (!init)
                {
                    TryPlayBackground("圣诞节.mp3");
                    ShowInfo("雪橇铃儿清脆作响，穿越雪幕；璀璨星光点亮夜空，指引方向。圣诞之际，愿绮梦飞扬，欢乐满溢，开启一场梦幻的西方节庆之旅。\n圣诞节日商品已上架，营造节日氛围！", "圣诞节到啦");
                }
            }

            if (month == 2 && day >= 1 && day <= 14)
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "大地红", Buy = 8m, Sell = 15m, Festival = true },
                    new Models.Product { Name = "加特林", Buy = 50m, Sell = 100m, Festival = true },
                    new Models.Product { Name = "夜明珠", Buy = 15m, Sell = 30m, Festival = true },
                    new Models.Product { Name = "仙女棒", Buy = 2m, Sell = 5m, Festival = true },
                    new Models.Product { Name = "二踢脚", Buy = 8m, Sell = 12m, Festival = true },
                    new Models.Product { Name = "沙泡", Buy = 1m, Sell = 2m, Festival = true },
                    new Models.Product { Name = "瓜子", Buy = 5m, Sell = 8m, Festival = true },
                    new Models.Product { Name = "花生", Buy = 5m, Sell = 8m, Festival = true },
                    new Models.Product { Name = "煌上煌礼盒", Buy = 100m, Sell = 188m, Festival = true }
                });
                if (!init && day == 1)
                {
                    TryPlayBackground("春节0.mp3");
                    ShowInfo("剪一纸窗花，裁一段春光；燃几簇烟火，聚几分热望。新春至，亲友相拥，把幸福珍藏。春节到啦。\n春节年货已上架，红红火火过新年！", "春节到啦");
                }
            }

            if (month == 2 && day == 14)
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "郁金香", Buy = 5m, Sell = 10m, Festival = true },
                    new Models.Product { Name = "玫瑰花", Buy = 8m, Sell = 15m, Festival = true },
                    new Models.Product { Name = "永生花", Buy = 30m, Sell = 52m, Festival = true },
                    new Models.Product { Name = "巧克力", Buy = 50m, Sell = 100m, Festival = true },
                    new Models.Product { Name = "钻石戒指", Buy = 3000m, Sell = 6000m, Festival = true },
                    new Models.Product { Name = "黄金手镯", Buy = 20000m, Sell = 30000m, Festival = true }
                });
                if (!init)
                {
                    TryPlayBackground("情人节.mp3");
                    ShowInfo("爱意不打折，浪漫不缺席。心动无需挑日子，今天偏爱多一点。与你共赴，这人间限定浪漫。以爱之名，定格岁岁年年。", "情人节到啦");
                }
            }

            if (month == 2)
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "汤圆", Buy = 5m, Sell = 10m, Festival = true },
                    new Models.Product { Name = "团员烟花大礼盒", Buy = 500m, Sell = 888m, Festival = true }
                });
                if (!init && day == 15)
                {
                    TryPlayBackground("元宵节.mp3");
                    ShowInfo("花灯耀元宵，团圆乐逍遥。元宵节就要到了。村里张灯结彩，十分热闹。", "元宵节到啦");
                }
            }

            if (month == 3)
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "红枣", Buy = 5m, Sell = 10m, Festival = true },
                    new Models.Product { Name = "小米米家保温杯", Buy = 66m, Sell = 99m, Festival = true },
                    new Models.Product { Name = "香奈儿香水", Buy = 500m, Sell = 520m, Festival = true },
                    new Models.Product { Name = "Lv包包", Buy = 666m, Sell = 888m, Festival = true }
                });
                if (!init && day == 8)
                {
                    TryPlayBackground("妇女节.mp3");
                    ShowInfo("春风如你，熠熠芳华。三八节，致每一位了不起的她，愿生活常温柔以待，愿梦想皆如愿以偿。", "妇女节到啦");
                }
            }

            if (month == 5 && day == 11)
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "康乃馨", Buy = 2m, Sell = 100m, Festival = true },
                    new Models.Product { Name = "萱草花", Buy = 1m, Sell = 5m, Festival = true },
                    new Models.Product { Name = "永远年轻护肤品", Buy = 500m, Sell = 88m, Festival = true }
                });
                if (!init)
                {
                    TryPlayBackground("母亲节.mp3");
                    ShowInfo("【妈妈，是比月光更早的温柔】  \n萱草香漫过窗台时，总想起您藏进毛衣针脚的絮叨，  \n把清晨熬成粥，把暮色缝成我远行的背包。\n\n后来世界很大，我却总在风起的路口攥紧衣角——\n原来那些被您熨烫过的岁月，早已裹着月光，\n在我心里长成一片永不枯萎的春潮。\n\n母亲节快乐，我的时光魔术师。", "母亲节到啦");
                }
            }

            if (month == 6 && day == 1)
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "旺旺零食大礼包", Buy = 50m, Sell = 88m, Festival = true },
                    new Models.Product { Name = "小天才电话手表", Buy = 800m, Sell = 999m, Festival = true }
                });
                if (!init)
                {
                    TryPlayBackground("儿童节.mp3");
                    ShowInfo("儿童节到啦，孩子们兴奋不已，都在等待着父母的礼物。\n儿童节商品已上架，别忘了为孩子们挑选心仪的礼物哦！", "儿童节到啦");
                }
            }

            if (month == 6 && day == 17)
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "花花公子休闲衬衫", Buy = 60m, Sell = 80m, Festival = true },
                    new Models.Product { Name = "爱健康运动手表", Buy = 200m, Sell = 399m, Festival = true },
                    new Models.Product { Name = "凤凰丹虫", Buy = 350m, Sell = 500m, Festival = true }
                });
                if (!init)
                {
                    TryPlayBackground("父亲节.mp3");
                    ShowInfo("【沉默的山水】\n\n您把岁月泡进搪瓷杯，\n烟灰缸盛过我的叛逆，\n却始终沉默如一座山。\n\n后来我数遍世界的棱角，\n才读懂您藏在旧手表里的涛声——\n那秒针走过的每道褶皱，\n都是未说出口的“回家吃饭”。\n\n父亲节到啦。\n父亲节商品已上架，快为爸爸挑选一份特别的礼物吧！", "父亲节到啦");
                }
            }

            if (month == 5 && day == 31)
            {
                tempProducts.AddRange(new[]
                {
                    new Models.Product { Name = "咸肉粽", Buy = 6m, Sell = 8m, Festival = true },
                    new Models.Product { Name = "艾包", Buy = 5m, Sell = 6m, Festival = true },
                    new Models.Product { Name = "雄黄酒", Buy = 98m, Sell = 198m, Festival = true }
                });
                if (!init)
                {
                    TryPlayBackground("端午节.mp3");
                    ShowInfo("端午佳节翩然而至，龙舟竞渡水花激扬，于锣鼓喧天中传承千年民俗记忆，续写华夏古韵新章。\n端午节商品已上架，快去为端午添购应景好货吧！", "端午节到啦");
                }
            }

            _productList = tempProducts;
            if (month == 8 && day == 6)
            {
                SetBackgroundTrack("背景2.mp3", !init);
            }
            else if (month == 9 && day == 16)
            {
                SetBackgroundTrack("背景3.mp3", !init);
            }
            else if (month == 10 && day == 13)
            {
                SetBackgroundTrack("背景5.mp3", !init);
            }
            else if (month == 10 && day == 8)
            {
                SetBackgroundTrack("背景4.mp3", !init);
            }
            else if (month == 11 && day == 3)
            {
                SetBackgroundTrack("背景6.mp3", !init);
            }
            else if (month == 12 && day == 27)
            {
                SetBackgroundTrack("背景5.mp3", !init);
            }
            else if (month == 2 && day == 16)
            {
                SetBackgroundTrack("背景1.mp3", !init);
            }
            else if (month == 3 && day == 8)
            {
                SetBackgroundTrack("背景2.mp3", !init);
            }
            else if (month == 5 && day == 13)
            {
                SetBackgroundTrack("背景3.mp3", !init);
            }
            else if (month == 6 && day == 3)
            {
                SetBackgroundTrack("背景7.mp3", !init);
            }
            else if (month == 6 && day == 19)
            {
                SetBackgroundTrack("背景5.mp3", !init);
            }
        }
    }
}
