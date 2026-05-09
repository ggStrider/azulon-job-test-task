using Internal.Scripts.Core.Reactive.Readonly;

namespace Internal.Scripts.Features.Currency
{
    public interface ICurrencyService
    {
        public ReadOnlyReactiveVariable<int> Currency { get; }

        public void Add(int amount);
        public bool TrySpend(int amount);
    }
}