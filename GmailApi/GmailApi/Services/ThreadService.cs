using System;
using GmailApi.Builders;
using GmailApi.DTO;
using GmailApi.Models;

namespace GmailApi.Services
{
    public class ThreadService
    {
        private readonly GmailClient _client;

        public ThreadService(GmailClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The ID of the thread to retrieve.</param>
        /// <returns></returns>
        public Thread Get(string id)
        {
            string queryString = new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.Get, id)
                .Build();

            return _client.Get<Thread>(queryString);
        }

        public ThreadList ListIds()
        {
            throw new NotImplementedException();
        }

        public Thread Modify()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ID of the Thread to delete.</param>
        /// <returns></returns>
        public void Delete(string id)
        {
            string queryString = new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.Delete, id)
                .Build();

            _client.Delete(queryString);
        }

        public Thread Trash()
        {
            throw new NotImplementedException();
        }

        public Thread Untrash()
        {
            throw new NotImplementedException();
        }
    }
}
