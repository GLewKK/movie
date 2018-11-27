using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repository;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<DataExtra> DataExtraRepository { get; }
        IGenericRepository<Image> ImageRepository { get; }
        IGenericRepository<Credit> CreditRepository { get; }
        IGenericRepository<Request> RequestRepository { get; }
        IGenericRepository<Status> StatusRepository { get; }
        IGenericRepository<Telephone> TelephoneRepository { get; }
        IGenericRepository<FormErrors> FormErrorsRepository { get; }




    }
}
