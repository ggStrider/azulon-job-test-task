using System;
using System.Collections.Generic;
using Internal.Scripts.Core.Reactive.Readonly;
using UnityEngine;

namespace Internal.Scripts.Core.Reactive
{
    [Serializable]
    public class ReactiveList<T>
    {
        public ReactiveList(bool setSilent = false)
        {
            _list = new List<T>();
        }

        public ReactiveList(List<T> startList, bool setSilent = false)
        {
            _list = startList ?? new List<T>();
        }

        [SerializeField] private List<T> _list;

        private ReadOnlyReactiveList<T> _readonly;

        public int Count => _list.Count;

        public T this[int index]
        {
            get => _list[index];
            set => SetValue(index, value);
        }

        /// <summary>
        /// index, oldValue, newValue
        /// </summary>
        public event Action<int, T, T> OnValueChanged;

        /// <summary>
        /// index, value
        /// </summary>
        public event Action<int, T> OnItemAdded;

        /// <summary>
        /// index, value
        /// </summary>
        public event Action<int, T> OnItemRemoved;

        public event Action OnListChanged;
        
        /// <summary>
        /// Invoked when the list is cleared
        /// Provides a snapshot of the list before clearing
        /// </summary>
        public event Action<List<T>> OnListCleared;

        public event Action<List<T>> OnListInitialized;

        #region Core

        public void Add(T item, bool setSilent = false)
        {
            _list.Add(item);

            if (!setSilent)
            {
                OnItemAdded?.Invoke(_list.Count - 1, item);
                OnListChanged?.Invoke();
            }
        }

        public void RemoveAt(int index, bool setSilent = false)
        {
            var oldItem = _list[index];
            _list.RemoveAt(index);

            if (!setSilent)
            {
                OnItemRemoved?.Invoke(index, oldItem);
                OnListChanged?.Invoke();
            }
        }

        public bool Remove(T item, bool setSilent = false)
        {
            var index = _list.IndexOf(item);
            if (index < 0) return false;

            RemoveAt(index, setSilent);
            return true;
        }

        public void Clear(bool setSilent = false)
        {
            if (_list.Count == 0) return;

            if (!setSilent)
            {
                var snapshot = new List<T>(_list);
                _list.Clear();

                OnListCleared?.Invoke(snapshot);
                OnListChanged?.Invoke();
            }
            else
            {
                _list.Clear();
            }
        }

        public void SetValue(int index, T newValue, bool force = false, bool setSilent = false)
        {
            if (!force)
            {
                if (EqualityComparer<T>.Default.Equals(_list[index], newValue))
                    return;
            }

            var oldValue = _list[index];
            _list[index] = newValue;

            if (!setSilent)
            {
                OnValueChanged?.Invoke(index, oldValue, newValue);
                OnListChanged?.Invoke();
            }
        }

        public void SetList(List<T> newList, bool setSilent = false)
        {
            _list = newList ?? new List<T>();

            if (!setSilent)
            {
                OnListInitialized?.Invoke(_list);
                OnListChanged?.Invoke();
            }
        }

        public ReadOnlyReactiveList<T> AsReadOnly()
        {
            _readonly ??= new ReadOnlyReactiveList<T>(this);
            return _readonly;
        }

        #endregion

        #region Utils

        public bool Contains(T item) =>
            _list.Contains(item);

        public int IndexOf(T item) =>
            _list.IndexOf(item);

        public List<T> GetRawList() =>
            _list;

        #endregion
    }
}