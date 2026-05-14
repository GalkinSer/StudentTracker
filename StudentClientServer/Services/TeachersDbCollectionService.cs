using Microsoft.EntityFrameworkCore;
using StudentTrackerLib.Models;
using StudentTrackerServer.DbServices;
using StudentTrackerServer.Exceptions;

namespace StudentTrackerServer.Services
{
    public class TeachersDbCollectionService
    {
        private readonly STDbContext _dbContext;

        public TeachersDbCollectionService(STDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Teacher>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Teachers.Include(x => x.Subjects).AsEnumerable());
        }
        public Task<Teacher?> GetAsync(int id, CancellationToken cancellationToken)
        {
            return _dbContext.Teachers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<Teacher?> AddAsync(Teacher item, CancellationToken cancellationToken)
        {
            await _dbContext.Teachers.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return await _dbContext.Teachers.Include(x => x.Subjects)
                .FirstOrDefaultAsync(x => x.Id == item.Id, cancellationToken);
        }
        public async Task<Teacher?> EditAsync(int id, Teacher item, CancellationToken cancellationToken)
        {
            var itemToUpdate = await _dbContext.Teachers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (itemToUpdate == null)
                throw new ArgumentException($"Teacher with id {id} does not exist");
            itemToUpdate.Name = item.Name;
            //_dbContext.Teachers.Update(itemToUpdate);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return itemToUpdate;
        }
        public async Task RemoveAsync(int id, CancellationToken cancellationToken)
        {
            Teacher? itemToRemove = await _dbContext.Teachers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (itemToRemove == null)
            {
                throw new ArgumentException($"Teacher with id {id} was not found");
            }
            _dbContext.Teachers.Remove(itemToRemove);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }
        public async Task<Teacher?> AssignSubjectAsync(int teacherId, int subjectId, CancellationToken cancellationToken)
        {
            Teacher? itemToEdit;
            if (teacherId == -1)
            {
                itemToEdit = await _dbContext.Teachers.OrderByDescending(x => x.Id).FirstOrDefaultAsync(cancellationToken);
                if (itemToEdit == null)
                {
                    throw new ArgumentException("No teachers were not found");
                }
            }
            else
            {
                itemToEdit = await _dbContext.Teachers.FirstOrDefaultAsync(x => x.Id == teacherId, cancellationToken);
                if (itemToEdit == null)
                {
                    throw new ArgumentException($"Teacher with id {teacherId} was not found");
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
        public async Task<Teacher?> DeassignSubjectAsync(int teacherId, int subjectId, CancellationToken cancellationToken)
        {
            Teacher? itemToEdit = await _dbContext.Teachers.Include(x => x.Subjects).FirstOrDefaultAsync(x => x.Id == teacherId, cancellationToken);
            if (itemToEdit == null)
            {
                throw new ArgumentException($"Teacher with id {teacherId} was not found");
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
        public async Task<Teacher?> Authenticate(Teacher teacher, CancellationToken cancellationToken)
        {
            Teacher? result = await _dbContext.Teachers
                .FirstOrDefaultAsync(x => x.Name.Equals(teacher.Name), cancellationToken);
            if (result == null)
            {
                throw new ArgumentException($"Teacher with name {teacher.Name} was not found");
            }
            if (!result.PasswordHash.Equals(teacher.PasswordHash))
            {
                throw new AuthenticationException($"The password is incorrect");
            }

            return result;
        }
    }
}
