using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace ITSolution.Framework.Core.Server.BaseClasses
{
    public class ParameterList : IList<CustomDbParameter>
    {
        private List<CustomDbParameter> _parameters;


        public ParameterList()
        {
            this._parameters = new List<CustomDbParameter>();
        }

        public ParameterList(int capacity)
        {
            this._parameters = new List<CustomDbParameter>(capacity);
            IsFixedSize = true;
        }

        IEnumerator<CustomDbParameter> IEnumerable<CustomDbParameter>.GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }

        public bool Contains(CustomDbParameter item)
        {
            return _parameters.Contains(item);
        }

        public void CopyTo(CustomDbParameter[] array, int index)
        {
            _parameters.CopyTo(array, index);
        }

        public bool Remove(CustomDbParameter item)
        {
            return _parameters.Remove(item);
        }

        public int Count
        {
            get { return _parameters.Count; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        public object SyncRoot
        {
            get { return new object(); }
        }

        public void Add(CustomDbParameter value)
        {
            _parameters.Add(value);
        }

        public void Add(string parameterName, Operator @operator, object value)
        {
            InternalAdd(parameterName, @operator, value);
        }
        public void Add(string parameterName, Operator @operator, object value, Condition condition)
        {
            InternalAdd(parameterName, @operator, value, condition);
        }

        private void InternalAdd(string parameterName, Operator @operator, object value,
            Condition condition = Condition.None)
        {
            CustomDbParameter customDbParameter = new CustomDbParameter(parameterName, value);
            customDbParameter.Operator = @operator;
            customDbParameter.Condition = condition;
            Add(customDbParameter);
        }

        public void Clear()
        {
            _parameters.Clear();
        }

        public int IndexOf(CustomDbParameter item)
        {
            return _parameters.IndexOf(item);
        }

        public void Insert(int index, CustomDbParameter item)
        {
            _parameters.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _parameters.RemoveAt(index);
        }

        public bool IsFixedSize { get; private set; }
        public bool IsReadOnly { get; }

        public CustomDbParameter this[int index]
        {
            get => _parameters[index];
            set => _parameters[index] = value;
        }

        public CustomDbParameter this[string parameterName]
        {
            get => _parameters.FirstOrDefault(p => p.ParameterName == parameterName);

            set
            {
                CustomDbParameter pr = _parameters.FirstOrDefault(p => p.ParameterName == parameterName);
                pr = value;
            }
        }
    }
}