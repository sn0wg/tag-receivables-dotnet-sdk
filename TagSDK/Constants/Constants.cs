﻿namespace TagSDK.Constants
{
    internal class Constants
    {
        public class Authentication
        {
            public const string BasePath = "token";
        }

        public class Consent
        {
            public const string BasePath = "receivable/consent";

            public const string BasePathOptIn = "receivable/consent/optin";

            public const string BasePathOptInReject = "receivable/consent/optin/reject";

            public const string BasePathOptOut = "receivable/consent/optout";

            public const string BasePathOptInParams = "receivable/consent/optin/parameters";
        }
        public class Endorsement
        {
            public class Contract
            {
                public const string BasePath = "receivable/contract";
                public const string BasePathHistory = "receivable/contract/history";
                public const string BasePathReference = "receivable/contract/reference";
                public const string BasePathKey = "receivable/contract/key";
                public const string BasePathProcessKey = "receivable/contract/processkey";
            }

            public class Advancement
            {
                public const string BasePath = "receivable/advancement";
            }

        }

        public class CommercialEstablishment
        {
            public const string BasePath = "customer/commercialEstablishment";
        }

        public class Notification
        {
            public const string BasePath = "receivable/notification";
            public const string BasePathProcessKey = "receivable/notification/processKey";
            public const string BasePathKey = "receivable/notification/key";
        }

        public class Receivable
        {
            public const string BasePath = "receivable";
        }

        public class Settlement
        {
            public const string BasePath = "receivable/settlement";
            public const string BasePathReject = "receivable/settlement/reject";
            public const string BasePathProcessKey = "receivable/settlement/processKey";
            public const string BasePathKey = "receivable/settlement/key";
            public const string BasePathReference = "receivable/settlement/reference";
        }

        public class Transaction
        {
            public const string BasePathTransaction = "receivable/transaction";

            public const string BasePathTransactionKey = "receivable/transaction/";
        }

        public class Reconciliation
        {
            public const string BasePath = "receivable/reconciliation";

            public const string BasePathKey = "receivable/reconciliation/key";
        }

        public class Position
        {
            public const string BasePath = "receivable/position";
            public const string BasePathKey = "receivable/position/key";
            public const string BasePathReference = "receivable/position/reference";
            public const string BasePathProcessReference = "receivable/position/processreference";
            public const string BasePathOriginalAssetHolder = "receivable/position/originalAssetHolder";
            public const string BasePathAssetHolder = "receivable/position/assetHolder";
        }
    }
}
