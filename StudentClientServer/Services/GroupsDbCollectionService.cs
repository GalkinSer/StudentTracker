using Microsoft.EntityFrameworkCore;
using StudentTrackerLib.Models;
using StudentTrackerServer.DbServices;

namespace StudentTrackerServer.Services
{
    public class GroupsDbCollectionService
    {
        private readonly STDbContext _dbContext;

        public GroupsDbCollectionService(STDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Group>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Groups
                .Include(x => x.Subjects).Include(x => x.Students).AsEnumerable());
        }
        public Task<Group?> GetAsync(int id, CancellationToken cancellationToken)
        {
            return _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public Task<IEnumerable<Group>> GetBySubjectIdAsync(int subjectId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Groups
                .Where(x => x.Subjects.Any(x => x.Id == subjectId))
                .OrderBy(x => x.Name)
                .AsEnumerable());
        }
        public async Task<Group?> AddAsync(Group item, CancellationToken cancellationToken)
        {
            await _dbContext.Groups.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return await _dbContext.Groups.Include(x => x.Subjects).Include(x => x.Students)
                .FirstOrDefaultAsync(x => x.Id == item.Id, cancellationToken);
        }
        public async Task<Group?> EditAsync(int id, Group item, CancellationToken cancellationToken)
        {
            var itemToUpdate = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (itemToUpdate == null)
                throw new ArgumentException($"Group with id {id} does not exist");
            itemToUpdate.Name = item.Name;
            _dbContext.Groups.Update(itemToUpdate);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return itemToUpdate;
        }
        public async Task RemoveAsync(int id, CancellationToken cancellationToken)
        {
            Group? itemToRemove = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (itemToRemove == null)
            {
                throw new ArgumentException($"Group with id {id} was not found");
            }
            _dbContext.Groups.Remove(itemToRemove);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }
        public async Task<Group?> AssignSubjectAsync(int groupId, int subjectId, CancellationToken cancellationToken)
        {
            Group? itemToEdit;
            if (groupId == -1)
            {
                itemToEdit = await _dbContext.Groups.OrderByDescending(x => x.Id).FirstOrDefaultAsync(cancellationToken);
                if (itemToEdit == null)
                {
                    throw new ArgumentException("No teachers were not found");
                }
            }
            else
            {
                itemToEdit = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == groupId, cancellationToken);
                if (itemToEdit == null)
                {
                    throw new ArgumentException($"Group with id {groupId} was not found");
                }
            }
            Subject? itemToAdd = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == subjectId, cancellationToken);
            if (itemToAdd == null)
            {
                throw new ArgumentException($"Subject with id {subjectId} was not found");
            }
            itemToEdit.Subjects.Add(itemToAdd);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return itemToEdit;
        }
        public async Task<Group?> DeassignSubjectAsync(int groupId, int subjectId, CancellationToken cancellationToken)
        {
            Group? itemToEdit = await _dbContext.Groups.Include(x => x.Subjects).FirstOrDefaultAsync(x => x.Id == groupId, cancellationToken);
            if (itemToEdit == null)
            {
                throw new ArgumentException($"Group with id {groupId} was not found");
            }
            Subject? itemToDelete = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == subjectId, cancellationToken);
            if (itemToDelete == null)
            {
                throw new ArgumentException($"Subject with id {subjectId} was not found");
            }
            itemToEdit.Subjects.Remove(itemToDelete);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return itemToEdit;
        }

    }
}
