using System;
using System.Collections.Generic;
using System.Linq;

namespace KP.GmailApi.UnitTests
{
    public class CleanupHelper<T, TParam>
    {
        private readonly Func<TParam, T> _createAction;
        private readonly Action<T> _deleteAction;
        private readonly List<T> _createdItems = new List<T>();

        public CleanupHelper(Func<TParam, T> createAction, Action<T> deleteAction)
        {
            _createAction = createAction;
            _deleteAction = deleteAction;
        }

        public T Create(TParam createParam)
        {
            T createdItem = _createAction(createParam);
            _createdItems.Add(createdItem);

            return createdItem;
        }

        public void Add(T item)
        {
            _createdItems.Add(item);
        }

        public void Remove(T item)
        {
            _createdItems.Remove(item);
        }

        public void Cleanup()
        {
            List<Exception> exceptions = new List<Exception>();

            foreach (T item in _createdItems)
            {
                try
                {
                    _deleteAction(item);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
                throw new AggregateException("Error(s) occured during cleanup", exceptions);
        }
    }
}
