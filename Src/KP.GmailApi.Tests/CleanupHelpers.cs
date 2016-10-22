using System;
using System.Threading.Tasks;
using KP.GmailApi.Common;
using KP.GmailApi.Models;
using KP.GmailApi.Services;
using KP.GmailApi.Tests.IntegrationTests;

namespace KP.GmailApi.Tests
{
    public class CleanupHelpers
    {
        public static CleanupHelper<Draft, Draft> GetDraftServiceCleanupHelper()
        {
            GmailProxy proxy = SettingsManager.GetGmailProxy();
            var service = new DraftService(proxy);

            return GetDraftServiceCleanupHelper(service);
        }

        public static CleanupHelper<Draft, Draft> GetDraftServiceCleanupHelper(DraftService service)
        {
            Func<Draft, Task> deleteAction = async label => await service.DeleteAsync(label.Id);
            Func<Draft, Task<Draft>> createAction = service.CreateAsync;
            return new CleanupHelper<Draft, Draft>(createAction, deleteAction);
        }
    }
}
