namespace YouthMeadowGeneralStore
{
    public partial class MainForm
    {
        private bool HandleEvent09()
        {
            _soundManager.PlayEffect("result_a");
            var cost = _random.Next(20, 101);
            var prize = _random.Next(0, 101);
            var profit = prize - cost;
            _money += profit;
            if (prize == 0)
            {
                ShowWarning($"你走进路边的彩票店花了￥{cost}买了张彩票，结果血本无归！", "恭喜");
            }
            else if (profit > 0)
            {
                if (profit >= 50)
                {
                    _soundManager.PlayEffect("result_a");
                    ShowInfo($"你走进路边的彩票店花了￥{cost}买了张彩票，结果中了￥{prize}，净赚￥{profit}！", "恭喜");
                }
                else
                {
                    ShowInfo($"你走进路边的彩票店花了￥{cost}买了张彩票，结果中了￥{prize}，赚了￥{profit}！", "恭喜");
                }
            }
            else
            {
                ShowWarning($"你走进路边的彩票店花了￥{cost}买了张彩票，只中￥{prize}，倒亏￥{-profit}！", "恭喜");
            }

            return true;
        }

        private bool HandleEvent10()
        {
            _soundManager.PlayEffect("result_a");
            _money += 500m;
            ShowWarning("出门走了没一会，发现地面有一个钱包，你环顾四周并没有发现施主。那就只能据为己有了。+￥500", "恭喜");
            return true;
        }

        private bool HandleEvent11()
        {
            _soundManager.PlayEffect("select_a");
            if (AskYesNo("网红零食热潮", "最近一款网红辣条在网上爆火，好多人在短视频平台分享，不少顾客来你的小卖部打听有没有这款辣条。你有渠道能进到货，要进购这款网红辣条售卖吗？"))
            {
                switch (_random.Next(1, 4))
                {
                    case 1:
                        _soundManager.PlayEffect("result_a");
                        _money += 800m;
                        ShowInfo("你果断进购，辣条上架后大受欢迎，学生和年轻人纷纷抢购，一周内就卖出了很多，盈利￥800 。", "恭喜");
                        break;
                    case 2:
                        _soundManager.PlayEffect("result_a");
                        _money += 500m;
                        ShowWarning("进购回来发现部分辣条包装有轻微破损，你联系厂家换货耽搁了些时间，不过还是顺利卖出，盈利￥500 。", "恭喜");
                        break;
                    default:
                        _soundManager.PlayEffect("result_a");
                        _money += 1200m;
                        ShowInfo("你把辣条摆在显眼位置，还推出买一送一的促销活动，吸引了更多顾客，不仅辣条卖得好，还带动了其他零食的销量，盈利￥1200 。", "恭喜");
                        break;
                }
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    _soundManager.PlayEffect("result_a");
                    ShowInfo("你没进货，看到隔壁小卖部靠卖这款辣条生意火爆，顾客都被吸引过去，你后悔没抓住机会。", "可惜");
                }
                else
                {
                    _soundManager.PlayEffect("result_a");
                    ShowWarning("过了一段时间，有报道称这款辣条卫生不达标，很多进购的小卖部被顾客投诉，你庆幸自己没进货。", "明智选择");
                }
            }

            return true;
        }

        private bool HandleEvent12()
        {
            _soundManager.PlayEffect("select_b");
            if (AskYesNo("迷路的老人", "你在进货的路上遇到了一位迷路的老人，老人说自己身上没钱也没有手机，你会帮助他吗？"))
            {
                switch (_random.Next(1, 5))
                {
                    case 1:
                        _soundManager.PlayEffect("result_a");
                        _money += 200m;
                        ShowInfo("你带着老人去了派出所，警察通过信息查询联系到了老人的家人，老人的家人为了感谢你，给了你￥200 作为酬谢。", "结果");
                        break;
                    case 2:
                        _soundManager.PlayEffect("result_a");
                        _money += 1000m;
                        ShowWarning("你帮老人找到了家人，老人的家人是开工厂的，得知你做生意后，和你达成了合作，让你帮忙代销一些产品，第一个月就赚了￥800。", "恭喜");
                        break;
                    case 3:
                        _soundManager.PlayEffect("result_b");
                        _money -= 300m;
                        ShowWarning("你陪着老人等家人来接，却被路过的人误以为你是骗子，还报了警，虽然最后误会解除了，但浪费了你很多时间，导致进货延迟，损失了一些生意，现金减少￥300。", "结果");
                        break;
                    default:
                        _money += 600m;
                        ShowInfo("你帮助老人后，老人非常感激，给你讲了很多人生经验和生意之道，后来你按照老人的建议调整了经营策略，店里生意变好，一个月多赚了￥600。", "恭喜");
                        break;
                }
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    _soundManager.PlayEffect("result_b");
                    _money -= 400m;
                    ShowInfo("你没管老人，结果老人在路边摔倒了，被路过的人送到了医院，周围有人认出你当时没帮忙，在村里传你冷漠，导致你的生意受到影响，一个月损失约￥400。", "结果");
                }
                else
                {
                    _soundManager.PlayEffect("result_a");
                    _money += 1000m;
                    ShowWarning("你没帮老人，但是在进货时遇到了一个大客户，谈成了一笔大订单，赚了￥1000，弥补了心里的愧疚。", "恭喜");
                }
            }

            return true;
        }

        private bool HandleEvent13()
        {
            if (AskYesNo("仓库堆积", "在整理仓库时，你发现角落里有一些之前积压的旧商品，这些商品占用了不少空间，你要处理吗？"))
            {
                switch (_random.Next(1, 5))
                {
                    case 1:
                        _soundManager.PlayEffect("result_a");
                        _money += 150m;
                        ShowInfo("你将这些旧商品进行打包整理，联系了二手市场，低价卖出，虽然价格不高，但成功清理了库存，还获得了￥150。", "结果");
                        break;
                    case 2:
                        _soundManager.PlayEffect("result_a");
                        _money += 300m;
                        ShowWarning("你把旧商品重新包装，举办了一场促销活动，吸引了不少顾客，不仅清理了库存，还因为活动带动了其他商品的销售，额外盈利￥300。", "恭喜");
                        break;
                    case 3:
                        _soundManager.PlayEffect("result_b");
                        _money += 800m;
                        ShowWarning("你尝试在网上发布出售旧商品的信息，没想到被一位收藏家看中，他以高价收购了其中一件稀有物品，你获得了￥800。", "恭喜");
                        break;
                    default:
                        _money += 500m;
                        ShowInfo("你把旧商品捐赠给了当地的慈善机构，虽然没有收入，但获得了慈善机构的感谢信和荣誉证书，这为你的店铺赢得了良好的口碑，吸引了更多顾客，一个月内多赚了￥500。", "恭喜");
                        break;
                }
            }
            else
            {
                if (_random.Next(1, 4) == 2)
                {
                    _soundManager.PlayEffect("result_b");
                    _money -= 300m;
                    ShowInfo("你没处理旧商品，后来仓库不小心漏水，部分旧商品被损坏，只能报废处理，损失了￥300 的成本。");
                }
                else
                {
                    _soundManager.PlayEffect("result_b");
                    _money -= 250m;
                    ShowWarning("一直没管旧商品，占用了大量仓库空间，不得不租更大的仓库，增加了每月￥250 的租金成本。", "结果");
                }
            }

            return true;
        }

        private bool HandleEvent14()
        {
            if (AskYesNo("欧泡来袭", "欧泡时间到，我要欧泡，我要欧泡，欧泡果奶ooo，你要进购一批吗？"))
            {
                var bubbleCost = 300m;
                if (_money < bubbleCost)
                {
                    ShowError("金钱不足，无法进货欧泡。", "错误");
                }
                else
                {
                    var currentQty = GetDailyPurchase("欧泡");
                    if (currentQty + 100 > 500)
                    {
                        ShowError($"欧泡每天最多只能进货500件。\n当前已进货 {currentQty} 件。", "提示");
                    }
                    else
                    {
                        _money -= bubbleCost;
                        _dailyPurchase["欧泡"] = currentQty + 100;
                        AddWarehouseItems("欧泡", 3m, 5m, 100);
                        ShowInfo("欧泡进货成功，已进货100件。", "提示");
                    }
                }
            }
            else
            {
                ShowInfo("你放弃了欧泡的诱惑。", "提示");
            }

            return true;
        }

        private bool HandleEvent15()
        {
            if (AskYesNo("股市风云", "最近某股票大涨，你要趁机花￥100000入手吗？"))
            {
                if (_money < 100000m)
                {
                    ShowWarning("钱不够了，无法参与股票投资。", "提示");
                }
                else
                {
                    _money -= 100000m;
                    if (_random.Next(0, 2) == 0)
                    {
                        _money += 200000m;
                        ShowInfo("你买的这只股票一路大涨，这回可发达了，现金增加200000元。", "投资成功");
                    }
                    else
                    {
                        _money -= 50000m;
                        ShowWarning("你意气用事，结果刚入手股票就遭遇了暴跌，这回可全砸手里了，现金减少50000元。", "投资失败");
                    }
                }
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    ShowInfo("股票一路大涨，听说买了这只股票的人都发大财了，更有甚者在网络上大四分享成功秘诀，这回真是肠子都悔青了。", "消息");
                }
                else
                {
                    ShowWarning("股票一路暴跌，听说有人亏的连裤衩都没了，更有人从高楼上跳了下来，结束了宝贵的生命，真是令人唏嘘啊。", "消息");
                }
            }

            return true;
        }

        private bool HandleEvent16()
        {
            if (AskYesNo("学期将至", "9月1日快到了。学生们都开始准备起了开学所需的文具。你也敏锐的嗅到了一丝商机。根据你在村子里的调研发现，学生们普遍缺少书包、笔袋、尺子。你想要进货一批吗？"))
            {
                const decimal totalCost = 1550m;
                if (_money < totalCost)
                {
                    ShowError($"金钱不足，进货需要{FormatMoney(totalCost)}元。");
                }
                else
                {
                    _money -= totalCost;
                    for (var i = 0; i < 50; i++)
                    {
                        _warehouse.Add(new Models.Product { Name = "书包", Buy = 20m, Sell = 30m });
                        _warehouse.Add(new Models.Product { Name = "笔袋", Buy = 8m, Sell = 12m });
                        _warehouse.Add(new Models.Product { Name = "尺子", Buy = 3m, Sell = 5m });
                    }

                    if (_random.Next(0, 2) == 0)
                    {
                        _money += 800m;
                        ShowInfo("学生对你的书包、笔袋和尺子非常感兴趣，纷纷购买，这会赚翻了。", "生意兴隆");
                    }
                    else
                    {
                        ShowWarning("附近大爷的店早已布局，把你的潜在客户抢走了。", "生意受挫");
                    }
                }
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    ShowInfo("幸好你没跳进坑里，学生都回大城市了。", "提示");
                }
                else
                {
                    ShowWarning("学生们还等着你的文具呢，没想到你并没有进货。生意全被附近的大爷给抢走了。", "提示");
                }
            }

            return true;
        }

        private bool HandleEvent17()
        {
            if (AskYesNo("询问", "今天早上，店里突然来了一位自称为国聪食品公司的金牌销售。他像你推销了他们公司最新的辣条产品，并号称可以让你的店铺吸引更多小孩来此光顾。你要进货一批吗？"))
            {
                if (_random.Next(0, 2) == 0)
                {
                    const decimal spicyCost = 50m;
                    if (_money < spicyCost)
                    {
                        ShowError("金钱不足，无法进货辣条。", "错误");
                    }
                    else
                    {
                        _money -= spicyCost;
                        AddWarehouseItems("辣条", 1m, 2.5m, 100);
                        ShowInfo("你进货了100件辣条，销售情况非常好。", "辣条热卖");
                    }
                }
                else
                {
                    ShowInfo("你尝试进货50件辣条，但反响平平。", "销售冷淡");
                }
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    ShowInfo("你委婉拒绝了销售，继续平凡生活。", "选择保守");
                }
            }

            return true;
        }
    }
}
