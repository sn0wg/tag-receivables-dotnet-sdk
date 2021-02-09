using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TagSDK.Models.Enums;
using TagSDK.Models.Receivable.Notification;
using TagSDK.Services.Receivable.Notification;

namespace Testes
{
    [TestClass]
    [Ignore]
    public class NotificationServiceTest : BaseTest
    {

        DateTime date = DateTime.Parse("2020-12-10");
        INotificationService nS;
        private Profile profile = Profile.CREDITOR;

        public NotificationServiceTest()
        {
            Init();
            nS = fac.NotificationService;
        }

        [TestMethod]
        public async Task ListNotificationSettlementTest()
        {
            var result = await nS.GetSettlementNotification(date, profile);

            Print(result);
        }
        [TestMethod]
        public async Task ListNotificationSettlementRejectTest()
        {
            var result = await nS.GetSettlementRejectNotification(DateTime.Parse("2020-12-10"), profile);

            Print(result);
        }
        [TestMethod]
        [Ignore]
        public async Task ListNotificationAdvancementTest()
        {
            var result = await nS.GetAdvancementNotification(date, profile);

            Print(result);
        }
        [TestMethod]
        public async Task ListContractNotificationTest()
        {
            var result = await nS.GetContractNotification(DateTime.Parse("2020-12-09"), profile);

            Print(result);
        }
        [TestMethod]
        public async Task ListNotificationConsentTest()
        {
            var result = await nS.GetConsentNotification(date, profile);

            Print(result);
        }
        [TestMethod]
        public async Task ListNotificationProcessKeyTest()
        {
            var result = await nS.GetNotificationKey("2c76b0f6-513a-4c07-a17a-d37b9b6484f9", profile);

            Print(result);
        }
        [TestMethod]
        public async Task ListNotificationKeyTest()
        {
            var result = await nS.GetNotificationProcessKey("826392f6-d199-4e2c-b557-f4ae3fb871b3", profile);

            Print(result);
        }

    }
}
