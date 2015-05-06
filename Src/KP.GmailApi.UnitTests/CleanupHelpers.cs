using System;
using KP.GmailApi.Models;
using KP.GmailApi.Services;
using KP.GmailApi.UnitTests.IntegrationTests;

namespace KP.GmailApi.UnitTests
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
