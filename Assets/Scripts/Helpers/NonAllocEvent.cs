using System;
using System.Collections.Generic;

namespace PesPatron.Helpers
{
    public class NonAllocEvent
    {
        private List<Action> _actions = new List<Action>();

        public void ClearAllActions()
        {
            _actions.Clear();
        }

        public void Invoke()
        {
            for (int i = _actions.Count - 1; i >= 0; i--)
                _actions[i]();
        }

        public void Add(Action action) => _actions.Add(action);

        public void Remove(Action action) => _actions.Remove(action);
    }

    public class NonAllocEvent<T1>
    {
        private List<Action<T1>> _actions = new List<Action<T1>>();

        public void ClearAllActions()
        {
            _actions.Clear();
        }

        public void Invoke(T1 value)
        {
            for (int i = _actions.Count - 1; i >= 0; i--)
                _actions[i](value);
        }

        public void Add(Action<T1> action) => _actions.Add(action);

        public void Remove(Action<T1> action) => _actions.Remove(action);
    }

    public class NonAllocEvent<T1, T2>
    {
        private List<Action<T1, T2>> _actions = new List<Action<T1, T2>>();

        public void ClearAllActions()
        {
            _actions.Clear();
        }

        public void Invoke(T1 value1, T2 value2)
        {
            for (int i = _actions.Count - 1; i >= 0; i--)
                _actions[i](value1, value2);
        }

        public void Add(Action<T1, T2> action) => _actions.Add(action);

        public void Remove(Action<T1, T2> action) => _actions.Remove(action);
    }

    public class NonAllocEvent<T1, T2, T3>
    {
        private List<Action<T1, T2, T3>> _actions = new List<Action<T1, T2, T3>>();

        public void ClearAllActions()
        {
            _actions.Clear();
        }

        public void Invoke(T1 value1, T2 value2, T3 value3)
        {
            for (int i = _actions.Count - 1; i >= 0; i--)
                _actions[i](value1, value2, value3);
        }

        public void Add(Action<T1, T2, T3> action) => _actions.Add(action);

        public void Remove(Action<T1, T2, T3> action) => _actions.Remove(action);
    }

    public class NonAllocEvent<T1, T2, T3, T4>
    {
        private List<Action<T1, T2, T3, T4>> _actions = new List<Action<T1, T2, T3, T4>>();

        public void ClearAllActions()
        {
            _actions.Clear();
        }

        public void Invoke(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            for (int i = _actions.Count - 1; i >= 0; i--)
                _actions[i](value1, value2, value3, value4);
        }

        public void Add(Action<T1, T2, T3, T4> action) => _actions.Add(action);

        public void Remove(Action<T1, T2, T3, T4> action) => _actions.Remove(action);
    }

    public class NonAllocEvent<T1, T2, T3, T4, T5>
    {
        private List<Action<T1, T2, T3, T4, T5>> _actions = new List<Action<T1, T2, T3, T4, T5>>();

        public void ClearAllActions()
        {
            _actions.Clear();
        }

        public void Invoke(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            for (int i = _actions.Count - 1; i >= 0; i--)
                _actions[i](value1, value2, value3, value4, value5);
        }

        public void Add(Action<T1, T2, T3, T4, T5> action) => _actions.Add(action);

        public void Remove(Action<T1, T2, T3, T4, T5> action) => _actions.Remove(action);
    }
}