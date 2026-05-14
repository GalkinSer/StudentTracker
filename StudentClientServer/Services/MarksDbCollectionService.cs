using Microsoft.EntityFrameworkCore;
using StudentTrackerLib.Models;
using StudentTrackerServer.DbServices;

namespace StudentTrackerServer.Services
{
    public class MarksDbCollectionService
    {
        private readonly STDbContext _dbContext;

        public MarksDbCollectionService(STDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Mark>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Marks.Include(x => x.Student)
                .Include(x => x.Student.Group).AsEnumerable());
        }
        public Task<Mark?> GetAsync(int id, CancellationToken cancellationToken)
        {
            return _dbContext.Marks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public Task<IEnumerable<Mark>> GetByHeaderIdAsync(int headerId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Marks.Include(x => x.Student)
                .Where(x => x.HeaderId == headerId)
                .OrderBy(x => x.Student)
                /*.Include(x => x.Student.Group)*/
                .AsEnumerable());
        }

        public async Task<Mark?> AddAsync(Mark item, CancellationToken cancellationToken)
        {
            var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == item.StudentId, cancellationToken: cancellationToken);
            if (student == null)
                throw new ArgumentException($"Student with id {item.StudentId} does not exist");
            Header? header;
            if (item.HeaderId == -1)
            {
                header = await _dbContext.Headers.OrderByDescending(x => x.Id).FirstOrDefaultAsync(cancellationToken);
                if (header == null)
                    throw new ArgumentException("No headers were found");
            }
            else
            {
                header = await _dbContext.Headers.FirstOrDefaultAsync(x => x.Id == item.HeaderId, cancellationToken);
                if (header == null)
                    throw new ArgumentException($"Header with id {item.HeaderId} does not exist");
            }
            item.Header = header;
            item.Student = student;
            await _dbContext.Marks.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return item;
        }
        public async Task<Mark?> EditAsync(int id, Mark item, CancellationToken cancellationToken)
        {
            var itemToUpdate = await _dbContext.Marks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (itemToUpdate == null)
                throw new ArgumentException($"Mark with id {id} does not exist");
            itemToUpdate.Content = item.Content;
            _dbContext.Marks.Update(itemToUpdate);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return itemToUpdate;
        }
        public async Task RemoveAsync(int id, CancellationToken cancellationToken)
        {
            Mark? itemToRemove = await _dbContext.Marks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (itemToRemove == null)
            {
                throw new ArgumentException($"Group with id {id} was not found");
            }
            _dbContext.Marks.Remove(itemToRemove);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }
    }
}
