using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TagSDK.Models.Receivable.Consent;
using TagSDK.Models.Enums;
using TagSDK.Services.Receivable.Consent;

namespace Testes
{
    [TestClass]
    [Ignore]
    public class ConsentServiceTest : BaseTest
    {
        private IConsentService cService = null;
        private Profile profile = Profile.ACQUIRER;
        public ConsentServiceTest()
        {
            Init();
            cService = fac.ConsentService;
        }

        [TestMethod]
        public async Task insertOptInsTest()
        {
            List<Consent> optIns = new List<Consent>();
            Consent optIn = new Consent();
            optIn.Beneficiary = "63878277000131";
            optIn.PaymentScheme = "VCC";
            optIn.Acquirer = "58094131000165";
            optIn.AssetHolder = "86501627000141";
            optIn.SignatureDate = DateTime.Now;
            optIn.StartDate = DateTime.Now;
            optIn.EndDate = DateTime.Now;
            optIns.Add(optIn);

            ConsentRequest consentRequest = new ConsentRequest();
            consentRequest.OptIns = (optIns);

            var result = await cService.InsertConsents(consentRequest, profile);

            Print(result);
        }


        [TestMethod]
        public async Task rejectOptInsTest()
        {
            List<ConsentReject> optIns = new List<ConsentReject>();
            ConsentRejectRequest optRejInput = new ConsentRejectRequest();
            ConsentReject optRejItem = new ConsentReject();
            optRejItem.Reason = "carlao";
            optRejItem.Key = "4e00e0e0-74e0-4997-8f01-e3f546d1e791";
            optIns.Add(optRejItem);
            optRejInput.OptIns = optIns;
            var result = await cService.RejectOptIn(optRejInput, profile);

            Print(result);
        }


        [TestMethod]
        public async Task patchOptOutTest()
        {
            var result = await cService.PatchOptOut("98487a34-cf87-4f78-8f85-eebf50354c6b");

            Print(result);
        }


        [TestMethod]
        public async Task optInChangeValidity()
        {
            ConsentValidityChangeRequest optInVChangeInput = new ConsentValidityChangeRequest();
            optInVChangeInput.StartDate = DateTime.Parse("2020-12-16");
            optInVChangeInput.EndDate = DateTime.Parse("2020-12-21");
            var result = await cService.ChangeOptInValidityDate("98487a34-cf87-4f78-8f85-eebf50354c6b", optInVChangeInput, profile);

            Print(result);
        }


        [TestMethod]
        public async Task getOptInWithKey()
        {
            var result = await cService.GetOptInWithKey("98487a34-cf87-4f78-8f85-eebf50354c6b");

            Print(result);
        }


        [TestMethod]
        public async Task getOptInWithParams()
        {
            ConsentQueryFilter consentQueryFilter = new ConsentQueryFilter()
            {
                Acquirer = "58094131000165",
                Beneficiary = "63878277000131",
                PaymentScheme = "VCC",
                InitialDate = DateTime.Parse("2020-12-17"),
                FinalDate = DateTime.Parse("2020-12-17"),
                AssetHolder = "86501627000141"
            };

            var result = await cService.GetOptInWithParams(consentQueryFilter);

            Print(result);
        }
    }
}
