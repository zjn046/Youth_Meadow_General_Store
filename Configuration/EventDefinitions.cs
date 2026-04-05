using System;
using System.Threading.Tasks;

namespace YouthMeadowGeneralStore.Configuration
{
    public sealed class RandomEventDefinition
    {
        public RandomEventDefinition(int id, Func<MonthDay, bool> isAvailable, Func<bool> execute, double? triggerChance = null)
        {
            Id = id;
            IsAvailable = isAvailable;
            Execute = execute;
            TriggerChance = triggerChance;
        }

        public int Id { get; }

        public Func<MonthDay, bool> IsAvailable { get; }

        public Func<bool> Execute { get; }

        public double? TriggerChance { get; }
    }

    public sealed class SpecialDateEventDefinition
    {
        public SpecialDateEventDefinition(string name, Func<MonthDay, bool> shouldExecute, Func<Task<bool>> executeAsync)
        {
            Name = name;
            ShouldExecute = shouldExecute;
            ExecuteAsync = executeAsync;
        }

        public string Name { get; }

        public Func<MonthDay, bool> ShouldExecute { get; }

        public Func<Task<bool>> ExecuteAsync { get; }
    }
}
