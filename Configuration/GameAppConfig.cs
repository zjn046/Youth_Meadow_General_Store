using System;
using System.Collections.Generic;

namespace YouthMeadowGeneralStore.Configuration
{
    public static class GameAppConfig
    {
        public const string DefaultCurrentDate = "7月1日";
        public const string DefaultBackgroundTrack = "背景0.mp3";
        public const string FirstBusinessBackgroundTrack = "背景1.mp3";
        public const string SoundsDirectoryName = "sounds";
        public const string SaveFileName = "save.dat";
        public const string SaveEncryptionKey = "mysecretkey";

        public static IReadOnlyDictionary<int, int> MonthDays { get; } = new Dictionary<int, int>
        {
            [7] = 31,
            [8] = 31,
            [9] = 30,
            [10] = 31,
            [11] = 30,
            [12] = 31,
            [1] = 31,
            [2] = 28,
            [3] = 31,
            [4] = 30,
            [5] = 31,
            [6] = 30
        };

        public static IReadOnlyDictionary<string, string> SoundEffectFiles { get; } =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["startup"] = "开业.wav",
                ["purchase"] = "购入.wav",
                ["shelf"] = "上架.wav",
                ["Discard"] = "丢弃.wav",
                ["business"] = "营业.wav",
                ["result_a"] = "结果a.wav",
                ["result_b"] = "结果b.wav",
                ["result_c"] = "结果c.wav",
                ["select_a"] = "选择a.wav",
                ["select_b"] = "选择b.wav"
            };

        public static IReadOnlyDictionary<MonthDay, string> BackgroundTrackSchedule { get; } =
            new Dictionary<MonthDay, string>
            {
                [new MonthDay(8, 6)] = "背景2.mp3",
                [new MonthDay(9, 16)] = "背景3.mp3",
                [new MonthDay(10, 8)] = "背景4.mp3",
                [new MonthDay(10, 13)] = "背景5.mp3",
                [new MonthDay(11, 3)] = "背景6.mp3",
                [new MonthDay(12, 27)] = "背景5.mp3",
                [new MonthDay(2, 16)] = "背景1.mp3",
                [new MonthDay(3, 8)] = "背景2.mp3",
                [new MonthDay(5, 13)] = "背景3.mp3",
                [new MonthDay(6, 3)] = "背景7.mp3",
                [new MonthDay(6, 19)] = "背景5.mp3"
            };
    }

    public struct MonthDay : IEquatable<MonthDay>
    {
        public MonthDay(int month, int day)
        {
            Month = month;
            Day = day;
        }

        public int Month { get; }

        public int Day { get; }

        public MonthDay NextDay(IReadOnlyDictionary<int, int> monthDays)
        {
            var nextMonth = Month;
            var nextDay = Day + 1;
            if (nextDay > monthDays[nextMonth])
            {
                nextDay = 1;
                nextMonth = nextMonth == 12 ? 1 : nextMonth + 1;
            }

            return new MonthDay(nextMonth, nextDay);
        }

        public string ToDisplayString()
        {
            return string.Format("{0}月{1}日", Month, Day);
        }

        public bool Equals(MonthDay other)
        {
            return Month == other.Month && Day == other.Day;
        }

        public override bool Equals(object obj)
        {
            return obj is MonthDay other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Month * 397) ^ Day;
        }

        public static bool TryParse(string value, out MonthDay result)
        {
            result = new MonthDay(7, 1);
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            try
            {
                var parts = value
                    .Replace("月", " ")
                    .Replace("日", string.Empty)
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                result = new MonthDay(int.Parse(parts[0]), int.Parse(parts[1]));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
