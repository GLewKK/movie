using DAL.Models;
using DAL.Repository;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<DataExtra> DataExtraRepository => new GenericRepository<DataExtra>();
        public IGenericRepository<Image> ImageRepository => new GenericRepository<Image>();
        public IGenericRepository<Credit> CreditRepository => new GenericRepository<Credit>();
        public IGenericRepository<Request> RequestRepository => new GenericRepository<Request>();
        public IGenericRepository<Status> StatusRepository => new GenericRepository<Status>();
        public IGenericRepository<Telephone> TelephoneRepository => new GenericRepository<Telephone>();
        public IGenericRepository<FormErrors> FormErrorsRepository => new GenericRepository<FormErrors>();


        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
        }

        protected virtual void Dispose(bool Disposing)
        {
            _context.Dispose();
        }
    }
}
