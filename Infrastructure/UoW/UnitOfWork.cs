using UStart.Domain.UoW;
using UStart.Infrastructure.Context;

namespace UStart.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UStartContext _context;

        public UnitOfWork(UStartContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
