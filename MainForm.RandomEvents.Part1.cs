using System.Collections.Generic;
using System.Linq;

namespace YouthMeadowGeneralStore
{
    public partial class MainForm
    {
        private bool HandleEvent00()
        {
            if (AskYesNo("遇到纠纷处理", "早上店里来了一位大爷，为等你开口，他却先开口质疑你的商品存在过期现象，并威胁将会向市场监督管理总局进行投诉。而你深信自己售卖的商品一定是保质期内的。你们之间爆发了激烈的争论。最后他提出要么私聊，要么就投诉。作为老板的你是选择？选“是”私聊，选“否”投诉"))
            {
                if (_random.Next(0, 2) == 0)
                {
                    _money -= 200m;
                    ShowInfo("你选择私聊处理，赔偿￥200。", "处理结果");
                }
                else
                {
                    ShowInfo("你选择私聊处理，但大爷依然不满。大爷愤愤不平的走了", "处理结果");
                }
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    ShowInfo("你选择投诉处理，原来是他想敲诈你，最后啥事没有。", "处理结果");
                }
                else
                {
                    ShowWarning("你选择投诉处理，但引发更大麻烦，店铺形象受损。", "处理结果");
                }
            }

            return true;
        }

        private bool HandleEvent01()
        {
            _soundManager.PlayEffect("result_c");
            ShowInfo("今天是美好的一天，什么都没发生。", "信息");
            return true;
        }

        private bool HandleEvent02()
        {
            _soundManager.PlayEffect("result_b");
            ShowWarning("出门时，你居然忘记锁上小卖部的大门了。正好给路过的一个小孩看见，于是他便通知了村里的小伙伴一起来给你的店铺来了一次声势浩大的零元购。", "店铺消息");
            var foodList = new HashSet<string> { "雪碧", "可乐", "旺仔牛奶", "苏打饼", "乐视薯片", "白象方便面", "早餐面包", "果粒橙", "欧泡", "好丽友派", "辣条" };
            var originalTotal = 0;
            foreach (var slot in _shelfSlots.Where(item => item.Unlocked && item.Product != null && foodList.Contains(item.Product.Name)))
            {
                originalTotal += slot.Quantity;
                slot.Product = null;
                slot.Quantity = 0;
            }

            if (_random.NextDouble() < 0.5)
            {
                _money += 1500m;
                _soundManager.PlayEffect("result_a");
                ShowInfo("你发现食品被清空，愤怒报警。警察通过走访村民和排查线索，成功追回了全部物品并教育了涉事小孩。现金增加1500元。", "店铺失窃");
            }
            else
            {
                _soundManager.PlayEffect("result_b");
                ShowWarning($"你选择报警，但由于监控损坏，警察未能锁定小偷，只能自认倒霉。损失食品{originalTotal}件。", "店铺失窃");
            }

            return true;
        }

        private bool HandleEvent03()
        {
            _soundManager.PlayEffect("result_b");
            ShowInfo("下班后吃了路边烤土豆，结果跑了10趟厕所，只能在家休息。", "提示");
            return true;
        }

        private bool HandleEvent04()
        {
            _soundManager.PlayEffect("select_b");
            if (AskYesNo("救援", "今天在做生意时，你听见急促的呼喊声，发现几个小孩围着水塘，一个小孩在水中挣扎。你要去救他吗？"))
            {
                switch (_random.Next(1, 4))
                {
                    case 1:
                        ShowInfo("你成功救起小孩，小孩与伙伴们感激后扬长而去。", "救援成功");
                        break;
                    case 2:
                        _money -= 500m;
                        ShowWarning("你在水中操作失误，与小孩一起漂离岸边，最终被救起，但你被呛了不少水，被送进了医院，损失￥500。这下几天算是白忙活了。", "救援失败");
                        break;
                    default:
                        _money += 380m;
                        ShowInfo("你跳入水中奋力救起了小孩，小孩家长感激地送上红包，得到￥380。", "救援成功");
                        break;
                }
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    ShowInfo("你看见此情，不敢贸然下水，便打电话通知了消防队。过了一会，消防队成功就起了水中的小孩。", "结果");
                }
                else
                {
                    ShowWarning("你不敢下水，水中的小孩却离岸边越来越远。在千钧一发之际，村里一位大哥赶紧跳进水里就起了小孩。而一直在旁边吃瓜的你却被扣上了见死不救的帽子。从此生意大受影响。", "结果消息");
                }
            }

            return true;
        }

        private bool HandleEvent05()
        {
            _soundManager.PlayEffect("select_b");
            _money -= 1000m;
            if (AskYesNo("询问", "村里的狗蛋在你的小卖部疯狂购物，要求赊账，你同意吗？"))
            {
                switch (_random.Next(1, 6))
                {
                    case 1:
                        _soundManager.PlayEffect("result_c");
                        ShowInfo("狗蛋对你的行为十分满意，要求他的小弟不再来你这砸场子。", "结果");
                        break;
                    case 2:
                        _soundManager.PlayEffect("result_a");
                        _money += 700m;
                        ShowInfo("王寡妇帮你报了警，警察对狗蛋他们十分愤怒，狗蛋等人获15天拘留，村里为赔偿退回你7天房租。", "结果");
                        break;
                    case 3:
                        _soundManager.PlayEffect("result_b");
                        _money -= 2000m;
                        ShowWarning("王寡妇报了警，但狗蛋二叔是所长，狗蛋冲进小卖部暴打你，还拿走全部商品，损失￥2000。", "结果");
                        break;
                    case 4:
                        _soundManager.PlayEffect("result_b");
                        _money += 500m;
                        ShowWarning("狗蛋赊账后，过了几天带了一群朋友来你的店里消费，不仅还清了欠款，还让你的生意多赚了￥500。", "结果");
                        break;
                    default:
                        _soundManager.PlayEffect("result_b");
                        _money += 1000m;
                        ShowWarning("你同意赊账后，狗蛋到处宣扬你人好，村里很多人都来你店里消费，虽然狗蛋没还钱，但店里生意变好，一个月多赚了￥1000。", "结果");
                        break;
                }
            }
            else
            {
                switch (_random.Next(1, 5))
                {
                    case 1:
                        _soundManager.PlayEffect("result_c");
                        ShowInfo("狗蛋看你如此强硬，也没有说什么，带着小弟愤怒的离开了", "结果");
                        break;
                    case 2:
                        _soundManager.PlayEffect("result_b");
                        _money -= 1000m;
                        ShowWarning("狗蛋对你的态度感到十分愤怒，带着他的小弟把你的小店砸了个稀巴烂，这回可完了，现金减少 1000 元", "结果");
                        break;
                    case 3:
                        _soundManager.PlayEffect("result_b");
                        _money += 400m;
                        ShowWarning("狗蛋虽然生气走了，但后来他的一个朋友觉得你坚持原则很对，反而来你店里消费，还介绍了一些生意给你，一个月多赚了￥400。", "结果");
                        break;
                    default:
                        _soundManager.PlayEffect("result_b");
                        _money += 300m;
                        ShowWarning("狗蛋走后，村里的一位热心大叔知道了这件事，觉得你做得对，帮你在村里宣传，让你的口碑更好了，生意也有所提升，一个月多赚了￥300。", "结果");
                        break;
                }
            }

            return true;
        }

        private bool HandleEvent06()
        {
            _soundManager.PlayEffect("result_b");
            _money -= 1000m;
            ShowWarning("电话响起，父母突发疾病需要你给钱治病，损失￥1000。", "突发情况");
            return true;
        }

        private bool HandleEvent07()
        {
            _soundManager.PlayEffect("select_a");
            if (AskYesNo("街上英雄救美", "下班后，你走在街上，突然看到一名男士的手正伸向一个小姐姐的包包。你会想去阻止这个小偷吗？"))
            {
                switch (_random.Next(1, 4))
                {
                    case 1:
                        _soundManager.PlayEffect("result_a");
                        _money += 100m;
                        ShowInfo("你冲上去，在小偷即将得手的时候及时制止了他的行为。小姐姐被你的英勇所感动。像你表示感谢后，给了你￥100", "恭喜");
                        break;
                    case 2:
                        _soundManager.PlayEffect("result_b");
                        _money -= 200m;
                        ShowWarning("小姐姐对于你的突然介入感到非常疑惑，原来这个所谓小偷也只是这个小姐姐的男友。这位男士以为你和这个小姐姐有不正常关系变开始质问你。为了消除误会，你变给了些小钱来化解这件事。处理完误会后，你尴尬的离开了。现金减少￥200", "尴尬");
                        break;
                    default:
                        _soundManager.PlayEffect("result_c");
                        _money -= 100m;
                        ShowInfo("你成功的阻止了小偷的行为，小姐姐连忙对你表示感谢。并决定和你去共进晚餐。虽然花了点小钱，但成功加上了小姐姐的微信也是不错的。现金减少￥100", "恭喜");
                        break;
                }
            }
            else
            {
                if (_random.Next(0, 2) == 0)
                {
                    _soundManager.PlayEffect("result_c");
                    ShowInfo("你什么也没做，原来看错了。", "可惜");
                }
                else
                {
                    _soundManager.PlayEffect("result_b");
                    _money -= 300m;
                    ShowWarning("正当你聚精会神的观察时，没想到这个小偷的同伙已经盯上了你腰间的钱包。并娴熟的将其夺走。现金减少￥300", "结果");
                }
            }

            return true;
        }

        private bool HandleEvent08()
        {
            _soundManager.PlayEffect("select_a");
            if (AskYesNo("特色旅游投资", "绿水青山就是金山银山，为响应国家号召，村里计划打造特色旅游区并邀请村民入股，你有兴趣花费5000元投资吗？"))
            {
                if (_money < 5000m)
                {
                    _soundManager.PlayEffect("result_b");
                    ShowWarning("你有心参与，但资金不足，只能作罢", "资金不足");
                }
                else
                {
                    _money -= 5000m;
                    if (_random.Next(0, 2) == 0)
                    {
                        _soundManager.PlayEffect("result_a");
                        _money += 10000m;
                        ShowInfo("度假村开的很成功，广受好评，在抖音、小红书、bilibili上都有很高的曝光率，一批一批的游客纷至沓来，这回可赚大了，现金增加￥10000。", "投资成功");
                    }
                    else
                    {
                        _soundManager.PlayEffect("result_a");
                        ShowWarning("新开的度假区不温不火，并没有多少人来，这回可全砸手里了。", "投资结果");
                    }
                }
            }
            else
            {
                switch (_random.Next(1, 4))
                {
                    case 1:
                        ShowInfo("听说投资了度假区项目的村民都发达了，这回肠子都悔青了。", "后悔");
                        break;
                    case 2:
                        ShowInfo("度假区项目亏损严重，原来只不过是面子工程罢了，幸亏没有投资", "躲过一劫");
                        break;
                    default:
                        ShowInfo("你未参与投资，生意照旧。", "平稳过渡");
                        break;
                }
            }

            return true;
        }
    }
}
