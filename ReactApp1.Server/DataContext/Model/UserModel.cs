using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.DataContext.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class UserList
    {
        public List<UserModel> Users { get; set; }
    }
}
