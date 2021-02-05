using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TagSDK.Models.Receivable.Transaction;
using TagSDK.Services.Receivable.Transaction;

namespace Testes
{
    [TestClass]
    [Ignore]
    public class TransactionServiceTest : BaseTest
    {
        private ITransactionService srv;
        public TransactionServiceTest()
        {
            Init();
            srv = fac.TransactionService;
        }


        [TestMethod]
        public async Task createTransactedUnitsReceivables()
        {
            Transaction item = new Transaction();
            item.Identifier = "0125048520200513";
            item.TransactionDate = DateTime.Parse("2022-05-15");
            item.Amount = (500000);
            item.Reference = ("TR-488");
            item.DueDate = DateTime.Parse("2020-06-15");
            List<Transaction> itemList = new List<Transaction>();
            itemList.Add(item);
            TransactionReceivable inputItem = new TransactionReceivable();
            inputItem.Key = ("e9773677dcf7c64e8d30ba551be3c436");
            inputItem.Transactions = (itemList);
            List<TransactionReceivable> inputItemList = new List<TransactionReceivable>();
            inputItemList.Add(inputItem);
            TransactionRequest input = new TransactionRequest();
            input.Receivables = inputItemList;
            var result = await srv.CreateTransactedUnitsReceivables(input);

            Print(result);
        }


        [TestMethod]
        [Ignore]
        public async Task getTransactionTest()
        {
            var result = await srv.GetTransaction("e9773677dcf7c64e8d30ba551be3c436");

            Print(result);
        }


        [TestMethod]
        public async Task rectifyTransactedUnitsReceivables()
        {
            Transaction item = new Transaction();
            item.Identifier = "0125048520200513";
            item.TransactionDate = DateTime.Parse("2022-05-15");
            item.Amount = 500000;
            item.Reference = "TR-488";
            item.DueDate = DateTime.Parse("2020-06-15");
            List<Transaction> itemList = new List<Transaction>();
            itemList.Add(item);
            TransactionReceivable inputItem = new TransactionReceivable();
            inputItem.Key = "e9773677dcf7c64e8d30ba551be3c436";
            inputItem.Transactions = itemList;
            List<TransactionReceivable> inputItemList = new List<TransactionReceivable>();
            inputItemList.Add(inputItem);
            TransactionRequest input = new TransactionRequest();
            input.Receivables = inputItemList;
            var result = await srv.RectifyTransactedUnitsReceivables(input);

            Print(result);
        }
    }
}
