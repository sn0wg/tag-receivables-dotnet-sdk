using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TagSDK.Models.Enums;
using TagSDK.Models.Receivable.Notification;

namespace TagSDK.Services.Receivable.Notification
{
    public interface INotificationService
    {

        Task<List<NotificationDefaultResponse<NotificationSettlement>>> getSettlementNotification(DateTime date, Profile profile);
        Task<List<NotificationDefaultResponse<NotificationSettlementReject>>> getSettlementRejectNotification(DateTime date, Profile profile);
        Task<List<NotificationDefaultResponse<NotificationAdvancement>>> getAdvancementNotification(DateTime date, Profile profile);
        Task<List<NotificationDefaultResponse<NotificationContract>>> getContractNotification(DateTime date, Profile profile);
        Task<List<NotificationDefaultResponse<NotificationConsentWrapper>>> getConsentNotification(DateTime date, Profile profile);
        Task<List<NotificationDefaultResponse<NotificationContract>>> getNotificationProcessKey(string pkey, Profile profile);
        Task<List<NotificationDefaultResponse<NotificationContract>>> getNotificationKey(string key, Profile profile);
    }
}
