namespace TestApp_Repository.Entities
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int PocketMoney { get; set; }

        public string Password { get; set; }

        public bool IsDeleted { get; set; }
    }
}
