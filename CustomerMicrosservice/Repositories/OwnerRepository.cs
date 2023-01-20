using OwnerMicrosservice.Model;
using Google.Cloud.Firestore;
using OwnerMicrosservice.Enums;

namespace OwnerMicrosservice.Repositories
{
    public class OwnerRepository
    {
        private readonly BaseRepository<Owner> _repository;
        public OwnerRepository()
        {
            // This should be injected - This is just an example.
            _repository = new BaseRepository<Owner>(Collection.owners);
        }

        public async Task<List<Owner>> GetAllAsync() => await _repository.GetAllAsync<Owner>();

        public async Task<Owner?> GetAsync(Owner entity) => (Owner?)await _repository.GetAsync(entity);

        public async Task<Owner> AddAsync(Owner entity) => await _repository.AddAsync(entity);

        public async Task<Owner> UpdateAsync(Owner entity) => await _repository.UpdateAsync(entity);

        public async Task DeleteAsync(Owner entity) => await _repository.DeleteAsync(entity);

        public async Task<List<Owner>> QueryRecordsAsync(Query query) => await _repository.QueryRecordsAsync<Owner>(query);
    }
}
