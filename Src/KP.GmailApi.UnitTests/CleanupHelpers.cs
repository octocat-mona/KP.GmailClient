using System;
using GmailApi;
using GmailApi.Models;
using GmailApi.Services;
using UnitTests.IntegrationTests;

namespace UnitTests
{
    public class CleanupHelpers
    {
        public static CleanupHelper<Draft, Draft> GetDraftServiceCleanupHelper()
        {
            GmailClient client = SettingsManager.GetGmailClient();
            var service = new DraftService(client);

            return GetDraftServiceCleanupHelper(service);
        }

        public static CleanupHelper<Draft, Draft> GetDraftServiceCleanupHelper(DraftService service)
        {
            Action<Draft> deleteAction = label => service.Delete(label.Id);
            Func<Draft, Draft> createAction = service.Create;
            return new CleanupHelper<Draft, Draft>(createAction, deleteAction);
        }
    }
}
