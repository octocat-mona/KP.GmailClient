using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class ServiceItemHelper<T>
    {
        private readonly List<T> _createdItems = new List<T>();

        public void Add(T item)
        {
            _createdItems.Add(item);
        }

        public void Cleanup(Action<T> deleteAction)
        {
            List<Exception> exceptions = new List<Exception>();

            foreach (T item in _createdItems)
            {
                try
                {
                    deleteAction(item);
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
