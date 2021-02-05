using Microsoft.VisualStudio.TestTools.UnitTesting;

using TagSDK.Models;
using TagSDK.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using TagSDK.Models.Request;
using TagSDK.Models.Enums;
using TagSDK.Models.Customer;

namespace Testes
{
    [TestClass]
    [Ignore]
    public class CommercialEstablishmentServiceTest : BaseTest
    {
        ICommercialEstablishmentService cEstablishmentService;
        public CommercialEstablishmentServiceTest()
        {
            Init();
            cEstablishmentService = fac.CommercialEstablishmentService;
        }


        [TestMethod]
        public async Task insertCommercialEstablishmentTest()
        {
            List<string> paymentSchemes = new List<string>();
            paymentSchemes.Add("VCC");
            paymentSchemes.Add("MCC");

            string cnpj = this.GeraCNPJ();

            BankAccount bankAccount = new BankAccount
            {
                Branch = "1144",
                Account = "13341",
                AccountDigit = "X5",
                AccountType = BankAccountType.CC,
                Ispb = "12345678",
                DocumentType = DocumentType.CNPJ,
                DocumentNumber = cnpj
            };

            CommercialEstablishment cmEstab = new CommercialEstablishment()
            {
                DocumentType = DocumentType.CNPJ,
                DocumentNumber = cnpj,
                PaymentSchemes = paymentSchemes,
                BankAccount = bankAccount
            };

            List<CommercialEstablishment> commercialEstablishments = new List<CommercialEstablishment>();
            commercialEstablishments.Add(cmEstab);

            CommercialEstablishmentRequest cmEstabReq = new CommercialEstablishmentRequest()
            {
                CommercialEstablishments = commercialEstablishments
            };

            var result = await cEstablishmentService.RegisterCommercialEstablishments(cmEstabReq);

            Print(result);
        }


        [TestMethod]
        [Ignore]
        public async Task updateCommercialEstablishmentTest()
        {
            List<string> paymentSchemes = new List<string>();
            paymentSchemes.Add("VCC");
            string cnpj = "54762153000103";
            BankAccount bankAccount = new BankAccount
            {
                Branch = "1144",
                Account = "13341",
                AccountDigit = "X5",
                AccountType = BankAccountType.CC,
                Ispb = "12345678",
                DocumentType = DocumentType.CNPJ,
                DocumentNumber = cnpj
            };
            CommercialEstablishmentUpdateInput cmEstab = new CommercialEstablishmentUpdateInput();
            cmEstab.PaymentSchemes = (paymentSchemes);
            cmEstab.BankAccount = (bankAccount);
            cmEstab.Enabled = (true);
            CommercialEstablishmentUpdateRequest cmEstabReq = new CommercialEstablishmentUpdateRequest();
            cmEstabReq.CommercialEstablishment = (cmEstab);

            var result = await cEstablishmentService.UpdateCommercialEstablishments(cnpj, cmEstabReq);

            Print(result);
        }


        [TestMethod]
        public async Task listCommercialEstablishmentWithPaginationTest()
        {
            Pagination pag = new Pagination
            {
                Limit = 1,
                Page = 2
            };
            var result = await cEstablishmentService.GetCommercialEstablishmentsWithPagination(pag);

            Print(result);
        }


        [TestMethod]
        public async Task listCommercialEstablishmentWithDocumentNumberTest()
        {
            var result = await cEstablishmentService.GetCommercialEstablishmentsWithDocumentNumber("62661584000101");

            Print(result);
        }
    }
}
