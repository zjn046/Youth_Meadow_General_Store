namespace YouthMeadowGeneralStore
{
    public partial class MainForm
    {
        private bool HandleEvent18()
        {
            if (AskYesNo("暴雨预警", "天气预报显示，接下来一周连续暴雨，你需要提前准备吗？"))
            {
                switch (_random.Next(1, 5))
                {
                    case 1:
                        _money += 600m;
                        ShowInfo("你提前采购雨具，暴雨期间生意火爆，赚￥600。", "暴雨中见机遇");
                        break;
                    case 2:
                        _money -= 200m;
                        ShowInfo("你加固了店铺的门窗，还准备了防水沙袋，虽然花费了￥200，但成功避免了店铺被雨水浸泡，没有损失货物。", "防患未然");
                        break;
                    case 3:
                        _money += 350m;
                        ShowInfo("你提前联系了供应商，确保暴雨期间货物供应不受影响，还因为及时供货，获得了供应商的额外折扣，节省了￥350 的进货成本。", "精打细算");
                        break;
                    default:
                        _money -= 150m;
                        ShowInfo("你购买了商业保险，预防暴雨可能带来的损失。暴雨期间店铺虽然进水，但保险公司进行了赔偿，除了保费支出￥150，没有其他损失。", "保险救急");
                        break;
                }
            }
            else
            {
                switch (_random.Next(1, 4))
                {
                    case 1:
                        _money -= 800m;
                        ShowWarning("你没做准备，暴雨导致店铺进水，损失了￥800。", "惨重损失");
                        break;
                    case 2:
                        _money -= 250m;
                        ShowWarning("没准备雨具，顾客流失，生意减少，损失约￥250。", "错失良机");
                        break;
                    default:
                        _money -= 400m;
                        ShowWarning("没有提前联系供应商，暴雨期间货物供应中断，少赚了￥400。", "供应中断");
                        break;
                }
            }

            return true;
        }

        private bool HandleEvent19()
        {
            if (AskYesNo("税收优惠", "政府出台了一项新的针对小微企业的税收优惠政策，但需要提交一系列资料申请，你要申请吗？"))
            {
                switch (_random.Next(1, 5))
                {
                    case 1:
                        _money += 1000m;
                        ShowInfo("你花时间准备资料并成功申请到优惠政策，本季度节省了￥1000 的税款。", "政策红利");
                        break;
                    case 2:
                        _money += 1500m;
                        ShowInfo("申请过程中，你结识了一位税务专家，他给你提供了一些合理避税的建议，一年下来帮你节省了约￥1500 的税款。", "税务高手");
                        break;
                    case 3:
                        _money -= 200m;
                        ShowWarning("申请时因为资料填写有误，被驳回重新提交，浪费了时间和精力，还耽误了一些生意，损失约￥200。", "小失误");
                        break;
                    default:
                        _money += 600m;
                        ShowInfo("成功申请后，政府相关部门对你的企业进行了表彰，这提升了你的店铺知名度，吸引了更多顾客，一个月多赚了￥600。", "光荣加身");
                        break;
                }
            }
            else
            {
                switch (_random.Next(1, 4))
                {
                    case 1:
                        _money -= 400m;
                        ShowWarning("你没申请，后来得知同类型企业享受优惠政策后降低了成本，在市场竞争中更有优势，你的生意受到冲击，一个月少赚了￥400。", "错失良机");
                        break;
                    case 2:
                        ShowInfo("没申请优惠政策，虽然没额外收益，但也没因为申请手续繁琐而烦恼，一切照旧。", "平稳过渡");
                        break;
                    default:
                        ShowInfo("你没申请，后来政策发生变化，申请难度加大，你庆幸自己没花时间在这上面。", "无妨");
                        break;
                }
            }

            return true;
        }

        private bool HandleEvent20()
        {
            _soundManager.PlayEffect("select_a");
            if (AskYesNo("品牌代理", "一家新的食品厂商找到你，希望你能代理他们新研发的特色零食。厂商承诺给予丰厚的销售返点，但这款零食在市场上知名度较低，你会接受代理吗？"))
            {
                switch (_random.Next(1, 4))
                {
                    case 1:
                        _money += 1800m;
                        ShowInfo("你接受代理后，通过社交媒体和线下活动大力推广，零食销量大增，不仅获得了丰厚的返点，还额外盈利￥1800。", "代理成功");
                        break;
                    case 2:
                        _money -= 1000m;
                        ShowWarning("零食口味不符合当地消费者习惯，投入的推广成本也打了水漂，亏损￥1000。", "代理失败");
                        break;
                    default:
                        _money += 3000m;
                        ShowInfo("在推广过程中，你与当地一家网红合作，网红的推荐让这款零食一夜爆火，你不仅收获了大量订单，还与厂商达成长期合作，盈利￥3000。", "大赚特赚");
                        break;
                }
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    ShowInfo("你拒绝代理，不久后这款零食在其他地区爆火，你为错过商机而懊恼。", "遗憾");
                }
                else
                {
                    ShowInfo("后来得知这款零食被曝光存在食品安全问题，你庆幸当初没有接受代理，避免了信誉受损和经济损失。", "明智选择");
                }
            }

            return true;
        }

        private bool HandleEvent21()
        {
            _soundManager.PlayEffect("select_a");
            if (AskYesNo("网红效应", "一位知名美食博主在直播间推荐了一款进口巧克力，瞬间引发抢购热潮，市场需求大增。有供应商联系你，表示可以提供货源，你要卖这款巧克力吗？"))
            {
                switch (_random.Next(1, 4))
                {
                    case 1:
                        _money += 2500m;
                        ShowInfo("你迅速进货并上架销售，借助网红的影响力，巧克力销量火爆，很快售罄，盈利￥2500 。", "爆款");
                        break;
                    case 2:
                        _money += 1500m;
                        ShowInfo("由于订单量过大，物流配送出现延误，部分顾客不满要求退款，你积极协调物流并给予一定补偿，最终盈利￥1500 。", "小波折");
                        break;
                    default:
                        _money += 4500m;
                        ShowInfo("你与美食博主合作，邀请他到店进行直播，进一步提升了店铺知名度，巧克力和其他商品的销售额大幅增长，盈利￥4500 。", "大卖");
                        break;
                }
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    ShowInfo("你没有抓住这个机会，看着其他商家因销售这款巧克力生意火爆，你后悔不已。", "遗憾");
                }
                else
                {
                    ShowInfo("不久后，这款巧克力被爆出含有违禁添加剂，许多销售的商家被要求下架并受到处罚，你庆幸自己没有进货，避免了损失。", "明智放弃");
                }
            }

            return true;
        }

        private bool HandleEvent22()
        {
            if (AskYesNo("手机热卖", "新出的欧泡手机和威偶手机销量极高，你要趁机进购一批吗？进购100台，进货价1500元，总计150000元，售价2000元每台。"))
            {
                if (_money < 150000m)
                {
                    ShowWarning("金钱不足，进货失败。", "错误");
                }
                else
                {
                    _money -= 150000m;
                    AddWarehouseItems("欧泡手机", 1500m, 2000m, 100);
                    ShowInfo("手机进货成功，商品已存入仓库。", "进货完成");
                }
            }
            else
            {
                ShowInfo("你没有抓住手机热卖的机会。", "提示");
                _money -= 100m;
            }

            return true;
        }

        private bool HandleEvent23()
        {
            if (AskYesNo("好丽友派进货", "随机商品，好丽友，好朋友，好丽友派来袭，你要趁机进购一批吗？\n每包进货价2元，售价3元，进购100包，共200元。"))
            {
                if (_money >= 200m)
                {
                    _money -= 200m;
                    AddWarehouseItems("好丽友派", 2m, 3m, 100);
                    ShowInfo("成功进购100包好丽友派，已存入仓库。", "进货成功");
                }
                else
                {
                    ShowWarning("钱不够了，无法进货。", "资金不足");
                }
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    ShowInfo("你没有进货，却也没啥事发生。", "结果");
                }
                else
                {
                    ShowInfo("你的大爷收到了风声，火速与村长达成了合作，看着你的大爷赚了大钱，你眼红不以。", "结果");
                }
            }

            return true;
        }

        private bool HandleEvent24()
        {
            if (AskYesNo("茅台进货", "随机事件，村长儿子结婚，打算大办宴席需要大量的茅台，你要进购一批吗？\n进购50瓶，共50000元，每瓶1000元，售价3000元。"))
            {
                if (_money >= 50000m)
                {
                    _money -= 50000m;
                    AddWarehouseItems("茅台", 1000m, 3000m, 50);
                    switch (_random.Next(1, 4))
                    {
                        case 1:
                            _money += 150000m;
                            ShowInfo("酒席办的很成功，你进购的茅台被抢购一空，这回可赚大了，现金增加150000元。", "结果");
                            break;
                        case 2:
                            ShowWarning("你的大爷也收到了风声，抢先在你之前与村长达成了合作，这回可全砸手里了。", "结果");
                            break;
                        default:
                            _money -= 10000m;
                            ShowWarning("喝了你的酒，一大堆人上吐下泻，更有甚者住进了医院，家属要求赔偿，原来你卖的是假酒，现金减少10000元。", "结果");
                            break;
                    }
                }
                else
                {
                    ShowWarning("钱不够了。", "资金不足");
                }
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    ShowInfo("你没有进货，却也没啥事发生。", "结果");
                }
                else
                {
                    ShowInfo("你的大爷收到了风声，火速与村长达成了合作，看着你的大爷赚了大钱，你眼红不以。", "结果");
                }
            }

            return true;
        }

        private bool HandleEvent25()
        {
            _money -= 1000m;
            ShowWarning("突发事件，上级检查发现商品质量不合格，没收商品并处罚金1000元，现金减少1000元。", "检查事件");
            return true;
        }

        private bool HandleEvent26()
        {
            if (AskYesNo("社交聚会", "村里举办了一场大型的社交聚会，你要去参加吗？"))
            {
                switch (_random.Next(1, 5))
                {
                    case 1:
                        _money += 1200m;
                        _salesModifier = 1.5m;
                        ShowInfo("在聚会上，你结识了一位城里的商人，他对你的生意很感兴趣，和你达成了合作意向，后续合作让你盈利￥1200。 +￥1200", "合作成功");
                        break;
                    case 2:
                        _money += 200m;
                        _salesModifier = 1.3m;
                        ShowInfo("你在聚会上玩游戏赢得了比赛，获得了一张价值￥200 的购物券，可以在村里的商店使用。购物券已自动替换为￥200", "好运连连");
                        break;
                    case 3:
                        _money -= 150m;
                        _salesModifier = 0.8m;
                        ShowWarning("你在聚会上和一位村民发生了小摩擦，虽然最后和解了，但大家对你的印象变差，导致你的生意在接下来的一周有所下滑，损失约￥150。 ", "小摩擦");
                        break;
                    default:
                        _money += 700m;
                        _salesModifier = 1.4m;
                        ShowInfo("参加聚会时，你发现了一个新的商机，回来后调整经营方向，第一个月就多赚了￥700。", "商业直觉");
                        break;
                }
            }
            else
            {
                switch (_random.Next(1, 4))
                {
                    case 1:
                        ShowInfo("你没去参加，错过和那位城里商人结识的机会，后来听说和他合作的村民赚了不少，你有点后悔。", "机会溜走");
                        break;
                    case 2:
                        _money -= 300m;
                        _salesModifier = 0.8m;
                        ShowWarning("你没参加聚会，村里流传了一些对你不利的谣言，一个月内少赚了￥300。", "生意受损");
                        break;
                    default:
                        ShowInfo("因为没去聚会，你躲过了一场聚会上突发的争吵冲突，没有被卷入麻烦，也没什么损失。", "逃过一劫");
                        break;
                }
            }

            return true;
        }

        private bool HandleEvent28()
        {
            _tobaccoOffenseCount++;
            ConfiscateAllCigarettes();
            if (_tobaccoOffenseCount == 1)
            {
                _money -= 5000m;
                ShowWarning("突发事件：烟草局来人调查，发现你囤积了大量香烟，超过规定上限。\n执法人员没收全部香烟，并处罚金10000元。", "烟草局警告");
                return true;
            }

            _money -= 50000m;
            ShowError("突发事件：烟草局再次调查，发现你所售香烟是假冒伪劣。\n执法人员判处你半年有期徒刑，并处罚金50000元。\n游戏结束。", "烟草局判刑");
            CheckBankruptcy();
            if (_bankruptcyTriggered || IsDisposed || Disposing)
            {
                return false;
            }

            Close();
            return false;
        }
    }
}
