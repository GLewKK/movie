using System;
using System.Collections.Generic;
using DAL.Models;

namespace BLL
{
    public interface ITakeRequest
    {
        void SetCredit(string  id,decimal sum,int months);
        void AddExtraData(DataExtra data, string id);
        List<Request> GetNotificationList(string role);
        void SetCreditorModeratorId(string userid, Guid requestid);
        Credit FindRequests(string userid, string role);
        void SetDataExtra(string userid, string role);
        IEnumerable<DataExtra> GetDataExtras();
        DataExtra GetDataExtras(Guid id);
        Request GetRequest(string userId);
        string GetRequestStatus(Guid requestId);
        int AdminRequests(string status, string id, string role);
        List<Request> GetRequestByData(string userId, DateTime date);

        void ChangeStatus(Guid requestId, Guid statusId);
        void ChangeStatus(Request model, Guid statusId);

        Guid GetStatusByString(string status);
        void SetErrorForms(Guid RequestId, string[] list, Guid statusId);
        FormErrors FindFormModelErrors(Guid requestId);
        List<Request> GetMyRequests(string creditorid, bool isCompleted, string role);
        bool CheckCredit(string userid);
        void SetRequestResult(Guid requestId, bool isAccepted);
        List<DateTime> GetCountCompletedRequest(string userId);

        void SetRoleByUserId(string userId, string role);

        void SetCreditorIdToNull(string userId, string role);




    }
}