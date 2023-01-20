using Google.Cloud.Firestore;
using OwnerMicrosservice.Enums;
using OwnerMicrosservice.Interfaces;

namespace OwnerMicrosservice.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        private readonly Collection _collection;
        public FirestoreDb _firestoreDb;

        public BaseRepository(Collection collection)
        {
            _collection = collection;
            var filePath = @"carboncreditsfiap-firebase-adminsdk-dn8z4-e0bbfbd629.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
            _firestoreDb = FirestoreDb.Create("carboncreditsfiap");
        }

        public async Task<List<T>> GetAllAsync<T>() where T : IBaseFirestoreData
        {
            Query query = _firestoreDb.Collection(_collection.ToString());
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<T>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (!documentSnapshot.Exists) continue;
                var data = documentSnapshot.ConvertTo<T>();
                if (data == null) continue;
                data.Id = documentSnapshot.Id;
                list.Add(data);
            }

            return list;
        }

        public async Task<object> GetAsync<T>(T entity) where T : IBaseFirestoreData
        {
            var docRef = _firestoreDb.Collection(_collection.ToString()).Document(entity.Id);
            var snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                var obj = snapshot.ConvertTo<T>();
                obj.Id = snapshot.Id;
                return obj;
            }
            return null;
        }

        public async Task<T> AddAsync<T>(T entity) where T : IBaseFirestoreData
        {
            var colRef = _firestoreDb.Collection(_collection.ToString());
            var doc = await colRef.Document(entity.Id).CreateAsync(entity);
            // GO GET RECORD FROM DATABASE:
            // return (T) await GetAsync(entity); 
            return entity;
        }

        /// <inheritdoc />
        public async Task<T> UpdateAsync<T>(T entity) where T : IBaseFirestoreData
        {
            var recordRef = _firestoreDb.Collection(_collection.ToString()).Document(entity.Id);
            await recordRef.SetAsync(entity, SetOptions.MergeAll);
            // GO GET RECORD FROM DATABASE:
            // return (T)await GetAsync(entity);
            return entity;
        }

        /// <inheritdoc />
        public async Task DeleteAsync<T>(T entity) where T : IBaseFirestoreData
        {
            var recordRef = _firestoreDb.Collection(_collection.ToString()).Document(entity.Id);
            await recordRef.DeleteAsync();
        }

        /// <inheritdoc />
        public async Task<List<T>> QueryRecordsAsync<T>(Query query) where T : IBaseFirestoreData
        {
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<T>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (!documentSnapshot.Exists) continue;
                var data = documentSnapshot.ConvertTo<T>();
                if (data == null) continue;
                data.Id = documentSnapshot.Id;
                list.Add(data);
            }

            return list;
        }

    }
}
