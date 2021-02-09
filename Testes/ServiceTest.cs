using Microsoft.VisualStudio.TestTools.UnitTesting;

using TagSDK.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TagSDK.Models.Receivable.Register;
using TagSDK.Models.Enums;
using TagSDK.Services.Receivable.Register;

namespace Testes
{
    [TestClass]
    [Ignore]
    public class ServiceTest : BaseTest
    {
        private IReceivableService tService = null;
        public ServiceTest()
        {
            Init();
            tService = Fac.ReceivableService;
        }

        [TestMethod]
        public async Task insertReceivableTest()
        {
            BankAccount bankAccount = new BankAccount
            {
                Branch = "1144",
                Account = "13341",
                AccountDigit = "X5",
                AccountType = BankAccountType.CC,
                Ispb = "12345678",
                DocumentType = DocumentType.CNPJ,
                DocumentNumber = "34144310000100"
            };

            ReceivableSettlement settlement = new ReceivableSettlement()
            {
                Reference = "L_1875",
                AssetHolder = "34144310000100",
                AssetHolderDocumentType = DocumentType.CPF,
                SettlementDate = DateTime.Parse("2020-02-02"),
                SettlementObligationDate = DateTime.Parse("2021-11-01"),
                Amount = 100.00M,
                BankAccount = bankAccount
            };

            List<ReceivableSettlement> settlements = new List<ReceivableSettlement>();
            settlements.Add(settlement);

            Receivable receivable = new Receivable()
            {
                Reference = "UR_450",
                DueDate = DateTime.Now.AddDays(1),
                PaymentScheme = "VCC",
                OriginalAssetHolderDocumentType = DocumentType.CPF,
                OriginalAssetHolder = "34144310000100",
                Amount = 50000.00M,
                PrePaidAmount = 0,
                BankAccount = bankAccount,
                Settlements = settlements
            };

            string processReference = "PR_550";
            List<Receivable> receivables = new List<Receivable>();
            receivables.Add(receivable);
            ReceivableRequest rI = new ReceivableRequest
            {
                ProcessReference = processReference,
                Receivables = receivables
            };

            var result = await this.tService.RegisterReceivable(rI);

            Print(result);
        }
    }
}
