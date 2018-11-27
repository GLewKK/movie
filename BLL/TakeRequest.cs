using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.UnitOfWork;
namespace BLL
{
   

    public class TakeRequest : ITakeRequest
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private readonly IUnitOfWork _unit = new UnitOfWork();

        public void SetCredit(string  id,decimal sum,int months)
        {
            if (sum <= 0) throw new ArgumentOutOfRangeException(nameof(sum));
            var dataextraid = _context.DataExtra.AsNoTracking().FirstOrDefault(x => x.UserId == id).Id;
            var credit = new Credit
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                DataExtraId = dataextraid,
                IsDeleted = false,
                Sum = sum,
                Months = months
            };

            _unit.CreditRepository.Add(credit);

            var request = new Request
            {
                Id = Guid.NewGuid(),
                UserId = id,
                StatusId = _context.Statuses.FirstOrDefault(x => x.Name == "pending for validation").Id,
                CreditId = credit.Id,
                IsAccepted = null,
                Text = "New request for " +credit.Sum + "$",
                Date = DateTime.Now,
                Seen = false
            };
            _unit.RequestRepository.Add(request);

        }

        public void AddExtraData( DataExtra data, string id)
        {
            var dataextra = _context.DataExtra.AsNoTracking().FirstOrDefault(x => x.UserId == id);

            if (dataextra != null)
            {
                dataextra.FirstName = data.FirstName;
                dataextra.LastName = data.LastName;
                dataextra.Telephone = data.Telephone;
                dataextra.IDNP = data.IDNP;
                dataextra.IsMarried = data.IsMarried;
                dataextra.OfficialVenit = data.OfficialVenit;
                dataextra.OfficialVenitHusbandWife = data.OfficialVenitHusbandWife;
                dataextra.TelephoneWorkSpace = data.TelephoneWorkSpace;
                dataextra.WorkSpace = data.WorkSpace;
                dataextra.WorkSpaceHusbandWife = data.WorkSpaceHusbandWife;
                dataextra.DateOfBirth = data.DateOfBirth;


                _unit.DataExtraRepository.Edit(dataextra);
            }
        }

        public List<Request> GetNotificationList(string role)
        {
            List<Request> result;
            switch (role)
            {
                case UserRoles.Editor:
                    result = _context.Requests.Where(x => x.Status.Name == DbStatus.UserToEditor)
                        .Where(x => x.Seen == false).Where(x => x.CreditorId == null).ToList();
                    return result;
                case UserRoles.Moderator:
                    result = _context.Requests.Where(x => x.Status.Name == DbStatus.EditorToManager)
                        .Where(x => x.Seen == false).Where(x => x.ModeratorId == null).ToList();
                    return result;
                case UserRoles.Admin:
                    result = _context.Requests.ToList();
                    return result;
            }

            return null;
        }


        public void SetCreditorModeratorId(string userid, Guid requestid)
        {
            //var request = _unit.RequestRepository.Get(requestid);
            var request = _context.Requests.AsNoTracking().FirstOrDefault(x => x.Id == requestid);
            var role = _context.DataExtra.AsNoTracking().FirstOrDefault(x => x.UserId == userid).Role;
            if (request != null)
            {
                switch (role)
                {
                    case UserRoles.Editor:
                        request.CreditorId = userid;
                        _unit.RequestRepository.Edit(request);
                        break;
                    case UserRoles.Moderator:
                        request.ModeratorId = userid;
                        _unit.RequestRepository.Edit(request);
                        break;
                }
            }
        }

        public Credit FindRequests(string userid, string role)
        {
            var id = new Guid();
            var credit = new Credit();
            try
            {

                switch (role)
                {
                    case "Editor":
                        id = _context.Requests.FirstOrDefault(x => x.CreditorId == userid).CreditId;

                        credit = _context.Credits.Find(id);
                        break;
                    case "Moderator":
                        id = _context.Requests.FirstOrDefault(x => x.ModeratorId == userid).CreditId;
                        credit = _context.Credits.Find(id);
                        break;
                }
            }
            catch
            {
                return null;
            }

            return credit;
        }

        public void SetDataExtra(string userid, string role)
        {
            var dataExtra = new DataExtra
            {
                Id = Guid.NewGuid(),
                UserId = userid,
                Role= role,
                PersonType = false,
                CreationDate = DateTime.Now,
                ImagesId = _context.Image.Where(x => x.FileName == "default-user.png").Select(x => x.Id)
                    .FirstOrDefault()
            };
            _unit.DataExtraRepository.Add(dataExtra);



        }

        public IEnumerable<DataExtra> GetDataExtras()
        {
            var list = _unit.DataExtraRepository.GetAll();
            return list;
        }
        public DataExtra GetDataExtras(Guid id)
        {
            var dataExtra = _unit.DataExtraRepository.Get(id);
            return dataExtra;
        }

        public Request GetRequest(string userId)
        {
            if (userId == null) return null;
            var result = _unit.RequestRepository.GetBy(x => x.UserId == userId && x.IsAccepted == null);
            if (result == null)
            {
                result = _unit.RequestRepository.GetBy(x => x.CreditorId == userId && x.IsAccepted == null);
            }

            if (result == null)
            {
                result = _unit.RequestRepository.GetBy(x => x.ModeratorId == userId && x.IsAccepted == null);
            }

            return result;

        }public List<Request> GetRequestByData(string userId, DateTime date) 
        {
            var result = _context.Requests.Where(x => x.CreditorId == userId).Where(x => x.IsAccepted !=null).Where(x => x.Status.Name != "cancelled").Where(x => x.Date.Day == date.Day && x.Date.Month == date.Month && x.Date.Year == date.Year).ToList();
            if (result.Count == 0)
            {
                  result = _context.Requests.Where(x => x.ModeratorId == userId).Where(x => x.IsAccepted != null).Where(x=>x.Status.Name != "cancelled").Where(x => x.Date.Day == date.Day && x.Date.Month == date.Month && x.Date.Year == date.Year).ToList();
            }
            return result;

        }
        public string GetRequestStatus(Guid requestId)
        {
           // var result = _unit.RequestRepository.GetBy(x => x.Id == requestId);
            var request = _unit.RequestRepository.Get(requestId);
            string result = null;
            if (request != null)
            {
                 result = request.Status.Name;
            }
            return result;

        }

        public void ChangeStatus(Guid requestId, Guid statusId)
        {
            var request = _context.Requests.AsNoTracking().FirstOrDefault(x => x.Id == requestId);
            if (request != null)
            {
                request.StatusId = statusId;
                _unit.RequestRepository.Edit(request);
            }
        }

        public void ChangeStatus(Request model, Guid statusId)
        {
            var request = _context.Requests.AsNoTracking().FirstOrDefault(x => x.Id == model.Id);
            if (request != null)
            {
                request.StatusId = GetStatusByString(DbStatus.UserToEditor);
                _unit.RequestRepository.Edit(request);

                var dataextra = _context.DataExtra.AsNoTracking().FirstOrDefault(x => x.Id == request.Credit.DataExtraId);
                if (dataextra != null)
                {
                    dataextra.FirstName = model.Credit.DataExtra.FirstName;
                    dataextra.LastName = model.Credit.DataExtra.LastName;
                    dataextra.Telephone = model.Credit.DataExtra.Telephone;
                    dataextra.IDNP = model.Credit.DataExtra.IDNP;
                    dataextra.DateOfBirth = model.Credit.DataExtra.DateOfBirth;
                    dataextra.IsMarried = model.Credit.DataExtra.IsMarried;
                    dataextra.WorkSpace = model.Credit.DataExtra.WorkSpace;
                    dataextra.WorkSpaceHusbandWife = model.Credit.DataExtra.WorkSpaceHusbandWife;
                    dataextra.TelephoneWorkSpace = model.Credit.DataExtra.TelephoneWorkSpace;
                    dataextra.OfficialVenit = model.Credit.DataExtra.OfficialVenit;
                    dataextra.OfficialVenitHusbandWife = model.Credit.DataExtra.OfficialVenitHusbandWife;
                    _unit.DataExtraRepository.Edit(dataextra);
                }


                var formError = new FormErrors
                {
                    Request = new Request()
                };
                var list = _context.FormErrorses.AsNoTracking().Where(x => x.RequestId == request.Id).ToList();

                foreach (var item in list)
                {
                    _unit.FormErrorsRepository.Remove(item);
                }
                

            }

        }

        public Guid GetStatusByString(string status)
        {
            var newstatusId = _unit.StatusRepository.GetBy(x => x.Name == status).Id;
            return newstatusId;
        }

        public void SetErrorForms(Guid RequestId, string[] list, Guid statusId)
        {
            string listofId = string.Join(",", list);

            var requestId = _context.Requests.FirstOrDefault(x => x.Id == RequestId && x.StatusId == statusId).Id;
            var form = new FormErrors
            {
                RequestId = requestId,
                FormId = listofId
            };
            _unit.FormErrorsRepository.Add(form);
        }

        public FormErrors FindFormModelErrors(Guid requestId)
        {
            //trebu de adaugat cazu cand Moderator -> Editor status name
            var result = _context.FormErrorses.OrderByDescending(x => x.Id)
                .FirstOrDefault(x => x.RequestId == requestId && x.Request.Status.Name == "rejected");
            return result;
        }

        public int AdminRequests(string status, string id, string role)
        {
            int result=0;
            if(role==UserRoles.Editor)
            {
                switch (status)
                {
                    case "cancelled":
                        result = _context.Requests.Where(x => x.CreditorId == id).Where(x => x.Status.Name == "cancelled").Count();
                        break;
                    case "rejected":
                        result = _context.Requests.Where(x => x.CreditorId == id).Where(x => x.Status.Name == "rejected").Count();
                        break;
                    case "approved":
                        result = _context.Requests.Where(x => x.CreditorId == id).Where(x => x.Status.Name == "approved").Count();
                        break;
                    case "pending":
                        result = _context.Requests.Where(x => x.CreditorId == id).Where(x => x.Status.Name == "pending for approval" || x.Status.Name == "pending for validation").Count();
                        break;
                }
            }
            else if(role==UserRoles.Moderator)
            {
                switch (status)
                {
                    case "cancelled":
                        result = _context.Requests.Where(x=>x.ModeratorId==id).Where(x => x.Status.Name == "cancelled").Count();
                        break;
                    case "rejected":
                        result = _context.Requests.Where(x => x.ModeratorId == id).Where(x => x.Status.Name == "rejected").Count();
                        break;
                    case "approved":
                        result = _context.Requests.Where(x => x.ModeratorId == id).Where(x => x.Status.Name == "approved").Count();
                        break;
                    case "pending":
                        result = _context.Requests.Where(x => x.ModeratorId == id).Where(x => x.Status.Name == "pending for approval" || x.Status.Name == "pending for validation").Count();
                        break;
                }
            }
            else
            {
                switch (status)
                {
                    case "cancelled":
                        result = _context.Requests.Where(x => x.Status.Name == "cancelled").Count();
                        break;
                    case "rejected":
                        result = _context.Requests.Where(x => x.Status.Name == "rejected").Count();
                        break;
                    case "approved":
                        result = _context.Requests.Where(x => x.Status.Name == "approved").Count();
                        break;
                    case "pending":
                        result = _context.Requests.Where(x => x.Status.Name == "pending for approval" || x.Status.Name == "pending for validation").Count();
                        break;
                }
            }
            
         
            return result;
        }



        public List<Request> GetMyRequests(string id, bool isCompleted, string role)
        {
            List<Request> result = new List<Request>();

            switch (role)
            {
                case UserRoles.Editor:
                    if (!isCompleted)
                    {
                        result = _context.Requests.Where(x => x.CreditorId == id).Where(x => x.IsAccepted == null).ToList();

                    }
                    else
                    {
                        result = _context.Requests.Where(x => x.CreditorId == id).Where(x => x.IsAccepted != null).ToList();
                    }
                    break;

                case UserRoles.Moderator:

                    if (!isCompleted)
                    {
                        result = _context.Requests.Where(x => x.ModeratorId == id).Where(x => x.IsAccepted == null).ToList();

                    }
                    else
                    {
                        result = _context.Requests.Where(x => x.ModeratorId == id).Where(x => x.IsAccepted != null).ToList();
                    }
                    break;
                case UserRoles.Admin:
                    if(!isCompleted)
                    {
                        result = _context.Requests.Where(x => x.IsAccepted == null).ToList();
                    }
                    else
                    {
                        result = _context.Requests.Where(x => x.IsAccepted != null).ToList();

                    }
                    break;
            }
            return result;

        }

        public bool CheckCredit(string userid)
        {
            bool result = false;
            if (_context.Requests.Where(x=>x.Credit.DataExtra.UserId==userid).Where(x=>x.IsAccepted==null).Any())
            { result = true; }
            return result;
        }

        public void SetRequestResult(Guid requestId,bool isAccepted)
        {
            var result = _context.Requests.AsNoTracking().FirstOrDefault(x => x.Id == requestId);
            if (result != null)
            {
                result.IsAccepted = isAccepted;
                result.Date = DateTime.Now;
            }


            _unit.RequestRepository.Edit(result);

        }

        public List<DateTime> GetCountCompletedRequest(string id)
        {
            var result = _context.Requests.Where(x => x.CreditorId == id).Where(x => x.IsAccepted != null).Where(x => x.Status.Name == DbStatus.ManagerToUserGood || x.Status.Name == DbStatus.ManagerToUserBad)
                .Select(x => x.Date).ToList();
            if (result.Count == 0)
            {
                result = _context.Requests.Where(x => x.ModeratorId == id).Where(x => x.IsAccepted != null).Where(x => x.Status.Name == DbStatus.ManagerToUserGood || x.Status.Name == DbStatus.ManagerToUserBad)
                .Select(x => x.Date).ToList();
            }

          
           // var count = result.Count(x => x.Date.Day == date.Day && x.Date.Month == date.Month && x.Date.Year == date.Year && x.IsAccepted != null && x.Name == DbStatus.ManagerToUserGood || x.Name == DbStatus.ManagerToUserBad);

            return result;
        }
        public void SetRoleByUserId(string userId, string role)
        {
            var model = _context.DataExtra.AsNoTracking().FirstOrDefault(x => x.UserId == userId);
            model.Role = role;
            _unit.DataExtraRepository.Edit(model);
        }
        public void SetCreditorIdToNull(string userId, string role)
        {
            if (role == UserRoles.Editor)
            {
                var result = _context.Requests.AsNoTracking().Where(x => x.CreditorId == userId).Where(x => x.IsAccepted == null).ToList();
                foreach (var item in result)
                {
                    item.CreditorId = null;
                    _unit.RequestRepository.Edit(item);
                }


            }
            if (role == UserRoles.Moderator)
            {
                var result = _context.Requests.AsNoTracking().Where(x => x.ModeratorId == userId).Where(x => x.IsAccepted == null).ToList();
                foreach(var item in result)
                {
                    item.ModeratorId = null;
                    _unit.RequestRepository.Edit(item);
                }
            }


        }
    }
}
