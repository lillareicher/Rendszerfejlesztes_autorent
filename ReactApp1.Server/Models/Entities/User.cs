namespace ReactApp1.Server.Models.Entities
{
    public class User
    {
        public Role Role { get; set; }
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }


}
