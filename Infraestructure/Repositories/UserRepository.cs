using Core.Entities;
using Core.Interfaces.Repositories;
using Infraestructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDBContext _mongoDBContext;
        private readonly IMongoCollection<User> _userCollection;
        private const string collectionName = "users";

        public UserRepository(MongoDBContext mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
            _userCollection = _mongoDBContext.GetConn().GetCollection<User>(collectionName);
        }

        public async Task<List<User>> GetAll()
        {
            var allUsers = await _userCollection.Find(EmptyFilter()).ToListAsync();

            return allUsers;
        }

        public async Task<User> FindByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq(nameof(User.email), email);

            var entity = await _userCollection.Find(filter).FirstOrDefaultAsync();

            return entity;
        }

        public async Task<User> InsertOne(User User)
        {
            await _userCollection.InsertOneAsync(User);
            return User;
        }

        private static FilterDefinition<User> FilterByObjectId(string objectId)
        {
            return Builders<User>.Filter.Eq(x => x.id!.ToString(), ObjectId.Parse(objectId).ToString());
        }

        private static FilterDefinition<User> EmptyFilter()
        {
            return Builders<User>.Filter.Empty;
        }
    }
}
