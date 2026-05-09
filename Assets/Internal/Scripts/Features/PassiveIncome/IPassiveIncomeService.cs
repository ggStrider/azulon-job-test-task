using System;

namespace Internal.Scripts.Features.PassiveIncome
{
    public interface IPassiveIncomeService : IDisposable
    {
        public float IncomePerSecond { get; }
        public void Recalculate();
    }
}