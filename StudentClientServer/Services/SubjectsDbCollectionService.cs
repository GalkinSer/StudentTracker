using Microsoft.EntityFrameworkCore;
using StudentTrackerLib.Models;
using StudentTrackerServer.DbServices;

namespace StudentTrackerServer.Services
{
    public class SubjectsDbCollectionService
    {
        private readonly STDbContext _dbContext;

        public SubjectsDbCollectionService(STDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Subject>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Subjects.Include(x => x.Teachers).AsEnumerable());
        }
        public Task<IEnumerable<Subject>> GetByTeacherIdAsync(int teacherId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Subjects
                .Where(x => x.Teachers.Any(x => x.Id == teacherId))
                .OrderBy(x => x.Name)
                .AsEnumerable());
        }
        public Task<Subject?> GetAsync(int id, CancellationToken cancellationToken)
        {
            return _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<Subject?> AddAsync(Subject item, CancellationToken cancellationToken)
        {
            await _dbContext.Subjects.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return item;
        }
        public async Task<Subject?> EditAsync(int id, Subject item, CancellationToken cancellationToken)
        {
            var itemToUpdate = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (itemToUpdate == null)
                throw new ArgumentException($"Subject with id {id} does not exist");
            itemToUpdate.Name = item.Name;
            _dbContext.Subjects.Update(itemToUpdate);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return itemToUpdate;
        }
        public async Task RemoveAsync(int id, CancellationToken cancellationToken)
        {
            Subject? itemToRemove = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (itemToRemove == null)
            {
                throw new ArgumentException($"Subject with id {id} was not found");
            }
            _dbContext.Subjects.Remove(itemToRemove);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }
    }
}
