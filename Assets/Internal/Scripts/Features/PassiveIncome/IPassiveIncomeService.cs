namespace Internal.Scripts.Features.PassiveIncome
{
    public interface IPassiveIncomeService
    {
        public float IncomePerSecond { get; }
        public void Recalculate();
    }
}