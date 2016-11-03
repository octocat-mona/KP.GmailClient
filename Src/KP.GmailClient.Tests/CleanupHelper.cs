using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KP.GmailClient.Tests
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

        /*public void Remove(T item)
        {
            _createdItems.Remove(item);
        }*/

        public void Cleanup()
        {
            var deleteTasks = _createdItems.Select(async item => await _deleteFunc(item));
            Task.WhenAll(deleteTasks).GetAwaiter().GetResult();
            _createdItems.Clear();
        }
    }
}
