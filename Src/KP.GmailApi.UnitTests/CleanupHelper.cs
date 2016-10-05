using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KP.GmailApi.UnitTests
{
    public class CleanupHelper<T, TParam>
    {
        private readonly Func<TParam, Task<T>> _createFunc;
        private readonly Func<T, Task> _deleteFunc;
        private readonly List<T> _createdItems = new List<T>();

        public CleanupHelper(Func<TParam, Task<T>> createFunc, Func<T, Task> deleteFunc)
        {
            _createFunc = createFunc;
            _deleteFunc = deleteFunc;
        }

        public async Task<T> CreateAsync(TParam createParam)
        {
            T createdItem = await _createFunc(createParam);
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
                    //TODO: use Task.WhenAll instead of loop
                    _deleteFunc(item).Wait();
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
