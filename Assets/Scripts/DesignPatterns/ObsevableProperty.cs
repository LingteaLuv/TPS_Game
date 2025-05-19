using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace DesignPatterns
{
    public class ObservableProperty<T>
    { 
        [Header("Drag&Drop")]
        [SerializeField] private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (_value.Equals(value)) return;
                _value = value;
                Notify();
            }
        }
    
        private UnityEvent<T> _onValueChanged = new ();

        public ObservableProperty(T value = default)
        {
            _value = value;
        }

        public void Subscribe(UnityAction<T> action)
        {
            _onValueChanged.AddListener(action);
        }

        public void Unsubscribe(UnityAction<T> action)
        {
            _onValueChanged.RemoveListener(action);
        }

        public void UnsubscribeAll()
        {
            _onValueChanged.RemoveAllListeners();
        }

        private void Notify()
        {
            _onValueChanged?.Invoke(Value);
        }
    }
}

