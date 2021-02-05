using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TagSDK.Models.Receivable.Reconciliation;
using TagSDK.Services.Receivable.Reconciliation;

namespace Testes
{
    [TestClass]
    [Ignore]
    public class ReconciliationServiceTest : BaseTest
    {
        private IReconciliationService rService = null;
        public ReconciliationServiceTest()
        {
            Init();
            rService = fac.ReconciliationService;
        }


        [TestMethod]
        public async Task insertReconciliation()
        {
            ReconciliationRequest rInput = new ReconciliationRequest();
            rInput.ReconciliationDate = DateTime.Now;
            var result = await rService.InsertConciliation(rInput);
            Print(result);

        }

        [TestMethod]
        public async Task getReconciliationWithKey()
        {
            var result = await rService.GetReconciliationWithKey("42ea18fd-a5d9-4bf9-a9c9-bc0515edb615");
            Print(result);
        }

        [TestMethod]
        public async Task confirmReconciliation()
        {
            ReconciliationRequest rInput = new ReconciliationRequest();
            rInput.ReconciliationDate = DateTime.Now;

            var response = await rService.InsertConciliation(rInput);

            var result = await rService.ConfirmReconciliation(response.ReconciliationKey, new ReconciliationConfirmationRequest { ReconciliationKey = response.ReconciliationKey });

            Print(result);
        }


    }
}
