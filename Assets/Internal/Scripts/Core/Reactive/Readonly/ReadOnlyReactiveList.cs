using System;
using System.Collections.Generic;

namespace Internal.Scripts.Core.Reactive.Readonly
{
    public class ReadOnlyReactiveList<T>
    {
        private readonly ReactiveList<T> _source;

        public ReadOnlyReactiveList(ReactiveList<T> source)
        {
            _source = source;
        }

        public int Count => _source.Count;

        public T this[int index] => _source[index];

        public event Action<int, T, T> OnValueChanged
        {
            add => _source.OnValueChanged += value;
            remove => _source.OnValueChanged -= value;
        }

        public event Action<int, T> OnItemAdded
        {
            add => _source.OnItemAdded += value;
            remove => _source.OnItemAdded -= value;
        }

        public event Action<int, T> OnItemRemoved
        {
            add => _source.OnItemRemoved += value;
            remove => _source.OnItemRemoved -= value;
        }

        public event Action OnListChanged
        {
            add => _source.OnListChanged += value;
            remove => _source.OnListChanged -= value;
        }

        public event Action<List<T>> OnListInitialized
        {
            add => _source.OnListInitialized += value;
            remove => _source.OnListInitialized -= value;
        }

        public bool Contains(T item) =>
            _source.Contains(item);
        
        public int IndexOf(T item) => 
            _source.IndexOf(item);

        public IReadOnlyList<T> AsReadOnly() => _source.GetRawList();
    }
}