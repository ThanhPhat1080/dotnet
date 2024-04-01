using Employee.Models;

namespace Employee.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeModel CreateNewEmployee(EmployeeModel newEmployee)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            IEnumerable<EmployeeModel> employees =
            [
                new()
                {
                    Id = 1,
                    Name = "Phat",
                    Department = new DepartmentModel() {Id = 1, Name="Danang", Location= "Danang"},
                    Email = "Phat.truong@asnet.com.vn",
                    Role="Admin"
                }
            ];

            return employees;
        }

        public EmployeeModel GetEmployeeById(int id)
        {
            throw new NotImplementedException();
        }

        public EmployeeModel UpdateEmployee(EmployeeModel updateEmployee)
        {
            throw new NotImplementedException();
        }
    }
}
