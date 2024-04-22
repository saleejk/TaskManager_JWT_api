using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskManager_JWT.Models;

namespace TaskManager_JWT.Services
{
    public interface IUser
    {
        public User LogIn(Login login);
        public List<User>getAllUser();
        public List<User> createUser(User user);
    }
    public class UserServices : IUser
    {
        public static List<User> users = new List<User> {
            new User { Id=1,Username="saleejn",Password="1234",Role="admin"},
        new User{Id=2,Username="anas",Password="4321",Role="user"}};

        public List<User> createUser(User user)
        {
            var newUser = users.LastOrDefault().Id + 1;
            user.Id = newUser;
            users.Add(user);
            return users;
        }
        public List<User> getAllUser()
        {
            return users;
        }
        
        public User LogIn(Login login)
        {
            var person = users.FirstOrDefault(val => val.Username == login.Username && val.Password == login.Password);
            return person;
        }
    }
}
