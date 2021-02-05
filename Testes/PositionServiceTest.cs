using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TagSDK.Models.Receivable.Position;
using TagSDK.Services.Receivable.Position;

namespace Testes
{
    [TestClass]
    [Ignore]
    public class PositionServiceTest : BaseTest
    {
        IPositionService receivablePositionService;
        public PositionServiceTest()
        {
            Init();
            receivablePositionService = fac.PositionService;
        }
        [TestMethod]
        public async Task AgendaPositionWithKeyTest()
        {
            var result = await receivablePositionService.GetPositionsWithKey("48ad40ab594434f916e497da745efa0c");

            Print(result);
        }
        [TestMethod]
        public async Task AgendaPositionWithReferenceTest()
        {
            var result = await receivablePositionService.GetPositionsWithReference("UR_450");

            Print(result);
        }
        [TestMethod]
        public async Task AgendaPositionWithProcessReferenceTest()
        {
            var result = await receivablePositionService.GetPositionsWithProcessReference("PR_550");

            Print(result);
        }

        [TestMethod]
        public async Task AgendaPositionWithOriginalAssetHolder()
        {
            PositionQueryFilter rPositionParams = new PositionQueryFilter
            {
                PaymentScheme = "VCC",
                InitialExpectedSettlementDate = DateTime.Parse("2020-12-01"),

                FinalExpectedSettlementDate = DateTime.Parse("2020-12-31")

            };
            var result = await receivablePositionService.GetPositionsWithOriginalAssetHolder("61821451000184", rPositionParams);

            Print(result);
        }

        [TestMethod]
        public async Task AgendaPositionWithAssetHolder()
        {
            PositionQueryFilter rPositionParams = new PositionQueryFilter
            {
                PaymentScheme = "VCC",
                InitialExpectedSettlementDate = DateTime.Parse("2020-12-01"),

                FinalExpectedSettlementDate = DateTime.Parse("2020-12-31")

            };

            var result = await receivablePositionService.GetPositionsWithAssetHolder("61821451000184", rPositionParams);

            Print(result);

        }


    }
}
