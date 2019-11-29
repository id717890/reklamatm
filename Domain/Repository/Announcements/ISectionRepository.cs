using System.Linq;
using Domain.Entity.Announcements;
using Domain.Repository.Shared;

namespace Domain.Repository.Announcements
{
    public interface ISectionRepository : IRepository<Section>
    {
        Section ReadByName(string name);
        IQueryable<Section> GetAsQueryable();
        Section GetById(int id);
    }
}
