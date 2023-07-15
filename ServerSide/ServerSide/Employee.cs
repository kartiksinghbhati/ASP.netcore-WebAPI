namespace ServerSide
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; }
        public string? PhotoBase64 { get; set; }
        public string Gender { get; set; }
        public string[]? Hobbies { get; set; }
        public string ActiveStatus { get; set; }
    }
}
