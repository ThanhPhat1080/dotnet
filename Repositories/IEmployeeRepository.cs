using Employee.Models;

namespace Employee.Repositories
{
    public interface IEmployeeRepository
    {
        public IEnumerable<EmployeeModel> GetAllEmployees();
        public EmployeeModel GetEmployeeById(int id);
        public EmployeeModel CreateNewEmployee(EmployeeModel newEmployee);
        public EmployeeModel UpdateEmployee(EmployeeModel updateEmployee);
        public bool Delete(int Id);
    }
}
