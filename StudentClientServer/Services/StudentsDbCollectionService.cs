using Microsoft.EntityFrameworkCore;
using StudentTrackerLib.Models;
using StudentTrackerServer.DbServices;

namespace StudentTrackerServer.Services
{
    public class StudentsDbCollectionService
    {
        private readonly STDbContext _dbContext;

        public StudentsDbCollectionService(STDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Students.Include("Group").AsEnumerable());
        }
        public Task<Student?> GetAsync(int id, CancellationToken cancellationToken)
        {
            return _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public Task<IEnumerable<Student>> GetByGroupIdAsync(int groupId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Students
                .Where(x => x.GroupId == groupId)
                .OrderBy(x => x.Name)
                .AsEnumerable());
        }
        public async Task<Student?> AddAsync(Student item, CancellationToken cancellationToken)
        {
            Group? group = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == item.GroupId, cancellationToken);
            if (group == null)
                throw new ArgumentException($"Group with id {item.GroupId} does not exist");
            item.Group = group;
            await _dbContext.Students.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return item;
        }
        public async Task<Student?> EditAsync(int id, Student item, CancellationToken cancellationToken)
        {
            var itemToUpdate = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (itemToUpdate == null)
                throw new ArgumentException($"Student with id {id} does not exist");
            Group? group = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == item.GroupId, cancellationToken);
            if (group == null)
                throw new ArgumentException($"Group with id {item.GroupId} does not exist");
            itemToUpdate.Name = item.Name;
            itemToUpdate.IsRepresentative = item.IsRepresentative;
            itemToUpdate.Group = group;
            _dbContext.Students.Update(itemToUpdate);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return itemToUpdate;
        }
        public async Task RemoveAsync(int id, CancellationToken cancellationToken)
        {
            Student? itemToRemove = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (itemToRemove == null)
            {
                throw new ArgumentException($"Student with id {id} was not found");
            }
            _dbContext.Students.Remove(itemToRemove);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }
    }
}
