using Microsoft.VisualStudio.TestTools.UnitTesting;

using TagSDK.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TagSDK.Models.Receivable.Contract;
using TagSDK.Models.Request;
using TagSDK.Models.Enums;
using TagSDK.Services.Receivable.Contract;

namespace Testes
{
    [TestClass]
    [Ignore]
    public class ContractServiceTest : BaseTest
    {
        private IContractService contractService = null;
        public ContractServiceTest()
        {
            Init();
            contractService = fac.ContractService;
        }

        [TestMethod]
        public async Task insertContract()
        {
            BankAccount bankAccount = new BankAccount
            {
                Branch = "1144",
                Account = "13341",
                AccountDigit = "X5",
                AccountType = BankAccountType.CC,
                Ispb = "12345678",
                DocumentType = DocumentType.CNPJ,
                DocumentNumber = "39624514000153"
            };
            ContractSpecification ctEspecification = new ContractSpecification();
            ctEspecification.ExpectedSettlementDate = DateTime.Parse("2021-01-01T05:00:00Z");
            ctEspecification.OriginalAssetHolderDocumentType = DocumentType.CNPJ;
            ctEspecification.OriginalAssetHolder = "39624514000153";
            ctEspecification.ReceivableDebtor = "74190072000185";
            ctEspecification.PaymentScheme = "VCC";
            ctEspecification.EffectValue = 100L;
            List<ContractSpecification> ctEspecifications = new List<ContractSpecification>();
            ctEspecifications.Add(ctEspecification);
            Contract ctItem = new Contract();
            ctItem.Reference = "CT_01";
            ctItem.ContractDueDate = DateTime.Parse("2021-01-01T05:00:00Z");
            ctItem.AssetHolderDocumentType = DocumentType.CNPJ;
            ctItem.AssetHolder = "39624514000153";
            ctItem.ContractUniqueIdentifier = "CT_01";
            ctItem.SignatureDate = DateTime.Parse("2021-01-01T05:00:00Z");
            ctItem.EffectType = EffectType.WARRANTY;
            ctItem.WarrantyType = WarrantyType.FIDUCIARY;
            ctItem.WarrantyAmount = 100L;
            ctItem.BalanceDue = 100L;
            ctItem.DivisionMethod = DivisionMethodType.FIXEDAMOUNT;
            ctItem.EffectStrategy = EffectStrategy.SPECIFIC;
            ctItem.PercentageValue = 10;
            ctItem.BankAccount = bankAccount;
            ctItem.ContractSpecifications = ctEspecifications;
            List<Contract> ctItems = new List<Contract>();
            ctItems.Add(ctItem);
            ContractRequest contractInput = new ContractRequest();
            contractInput.Contracts = ctItems;
            var result = await this.contractService.InsertContract(contractInput);

            Print(result);
        }


        [TestMethod]
        public async Task listContractsByReference()
        {
            Pagination page = new Pagination()
            {
                Page = 1,
                Limit = 10
            };
            var result = await contractService.ListContractsByReference("CT_01", page);

            Print(result);
        }


        [TestMethod]
        public async Task listContractsByKey()
        {
            var result = await contractService.ListContractsByKey("dda6c698-705d-49b5-a84c-c0a2d605b77c");

            Print(result);
        }


        [TestMethod]
        public async Task listContractsByProcessKey()
        {

            Pagination page = new Pagination()
            {
                Page = 1,
                Limit = 10
            };

            var result = await contractService.ListContractsByProcessKey("7c4f2552-4dcf-4320-a460-8fd6fd8ff09b", page);

            Print(result);
        }


        [TestMethod]
        public async Task listContractsWithParams()
        {
            ContractQueryFilter cInput = new ContractQueryFilter();
            cInput.StartContractDueDate = DateTime.Parse("2020-01-01");
            cInput.EndContractDueDate = DateTime.Parse("2020-12-31");
            cInput.AssetHolder = "61821451000184";
            // cInput.StartSignatureDate = null;
            // cInput.endSignatureDate = DateTime.Parse("2021-01-01T05:00:00Z");
            // cInput.startCreatedAt = DateTime.Parse("2021-01-01T05:00:00Z");
            // cInput.endCreatedAt = DateTime.Parse("2021-01-01T05:00:00Z");

            Pagination page = new Pagination()
            {
                Page = 1,
                Limit = 10
            };


            var result = await contractService.ListContractsWithParams(cInput, page);

            Print(result);
        }


        [TestMethod]
        [Ignore]
        public async Task getContractHistoryTest()
        {
            var result = await contractService.GetContractHistoryWithKey("CT_01");
            Print(result);
        }

    }
}
