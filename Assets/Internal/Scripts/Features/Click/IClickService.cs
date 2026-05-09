using System;
using Internal.Scripts.Core.Reactive.Readonly;

namespace Internal.Scripts.Features.Click
{
    public interface IClickService : IDisposable
    {
        public ReadOnlyReactiveVariable<int> ClickValue { get; }
        public void Click();
        public void RecalculateClickValue();
    }
}