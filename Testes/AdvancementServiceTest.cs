using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TagSDK.Models.Receivable.Advancement;
using TagSDK.Models.Enums;
using TagSDK.Services.Receivable.Advancement;

namespace Testes
{
    [TestClass]
    [Ignore]
    public class AdvancementServiceTest : BaseTest
    {
        IAdvancementService _advacementService;
        public AdvancementServiceTest()
        {
            Init();
            _advacementService = fac.AdvancementService;
        }

        [TestMethod]
        public async Task insertAdvancementVanillaTest()
        {
            AdvancedReceivable adReceivable = new AdvancedReceivable();
            adReceivable.AdvancedAmount = 100L;
            adReceivable.PaymentScheme = "VCC";
            adReceivable.SettlementObligationDate = DateTime.Now;
            List<AdvancedReceivable> adReceivables = new List<AdvancedReceivable>();
            adReceivables.Add(adReceivable);
            Advancement adItem = new Advancement();
            adItem.AssetHolderDocumentType = DocumentType.CNPJ;
            adItem.AssetHolder = "74190072000185";
            adItem.OperationValue = 100L;
            adItem.OperationExpectedSettlementDate = DateTime.Now;
            adItem.Reference = "RF_01";
            adItem.AdvancedReceivables = adReceivables;
            List<Advancement> adItems = new List<Advancement>();
            adItems.Add(adItem);
            AdvancementRequest adInput = new AdvancementRequest();
            adInput.Advancements = adItems;
            var result = await _advacementService.InsertAdvancements(adInput);

            Print(result);
        }
    }
}
