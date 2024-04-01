namespace Employee.Models
{
    public class EmployeeModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required DepartmentModel Department { get; set; }
    }
}
