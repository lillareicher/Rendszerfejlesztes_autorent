using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.DataContext.Model
{
    public class UserModel
    {
        public UserModel(string _id, string _username, string _name, string _password) 
        {
            Id = _id; UserName = _username; Name = _name; Password = _password;
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
    

}
