using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StudentTrackerLib.Models;
using StudentTrackerServer.DbServices;

namespace StudentTrackerServer.Services
{
    public class HeadersDbCollectionService
    {
        private readonly STDbContext _dbContext;

        public HeadersDbCollectionService(STDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Header>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Headers
                .Include(x => x.Group)
                .Include(x => x.Subject)
                .Include(x => x.Teacher)
                .AsEnumerable());
        }
        public Task<Header?> GetAsync(int id, CancellationToken cancellationToken)
        {
            return _dbContext.Headers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public Task<IEnumerable<Header>> GetByIdsAsync(int teacherId, int subjectId, int groupId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Headers.Where(x => 
                x.TeacherId == teacherId && x.SubjectId == subjectId && x.GroupId == groupId)
                .AsEnumerable());
        }
        public async Task<Header?> AddAsync(Header item, CancellationToken cancellationToken)
        {
            var subject = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == item.SubjectId, cancellationToken);
            if (subject == null)
                throw new ArgumentException($"Subject with id {item.SubjectId} does not exist");
            var group = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == item.GroupId, cancellationToken);
            if (group == null)
                throw new ArgumentException($"Group with id {item.GroupId} does not exist");
            var teacher = await _dbContext.Teachers.FirstOrDefaultAsync(x => x.Id == item.TeacherId, cancellationToken);
            if (teacher == null)
                throw new ArgumentException($"Teacher with id {item.TeacherId} does not exist");
            item.Subject = subject;
            item.Group = group;
            item.Teacher = teacher;
            await _dbContext.Headers.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return item;
        }
        public async Task<Header?> EditAsync(int id, Header item, CancellationToken cancellationToken)
        {
            Header? itemToUpdate = await _dbContext.Headers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (itemToUpdate == null)
                throw new ArgumentException($"Header with id {id} does not exist");
            itemToUpdate.Title = item.Title;
            _dbContext.Headers.Update(itemToUpdate);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return itemToUpdate;
        }
        public async Task RemoveAsync(int id, CancellationToken cancellationToken)
        {
            Header? itemToRemove = await _dbContext.Headers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (itemToRemove == null)
            {
                throw new ArgumentException($"Header with id {id} was not found");
            }
            _dbContext.Headers.Remove(itemToRemove);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }
    }
}
