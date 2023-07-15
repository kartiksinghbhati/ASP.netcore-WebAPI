using Microsoft.AspNetCore.Mvc;
using ServerSide;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private static List<Employee> employees = new List<Employee>();
    private static int nextId = 1;

    [HttpGet]
    [Route("GetAll")]
    public IActionResult GetAll()
    {
        return Ok(employees);
    }

    [HttpGet]
    [Route("GetById/{id}")]
    public IActionResult GetById(int id)
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
            return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    [Route("Create")]
    public IActionResult Create(Employee employee)
    {
        try
        {
            ValidateEmployee(employee);
            employee.Id = nextId++;
            employees.Add(employee);
            return Ok(employee.Id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("Update/{id}")]
    public IActionResult Update(int id, Employee employee)
    {
        try
        {
            var existingEmployee = employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
                return NotFound();

            ValidateEmployee(employee);
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.DateOfBirth = employee.DateOfBirth;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.PhotoBase64 = employee.PhotoBase64 ?? existingEmployee.PhotoBase64;

            existingEmployee.Gender = employee.Gender;
            existingEmployee.Hobbies = employee.Hobbies;
            existingEmployee.ActiveStatus = employee.ActiveStatus;

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("Delete/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();

            employees.Remove(employee);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    private void ValidateEmployee(Employee employee)
    {
        if (string.IsNullOrWhiteSpace(employee.FirstName) || employee.FirstName.Length > 50)
            throw new Exception("First name is required and must be a maximum of 50 characters.");

        if (string.IsNullOrWhiteSpace(employee.LastName) || employee.LastName.Length > 50)
            throw new Exception("Last name is required and must be a maximum of 50 characters.");

        if (DateTime.Now.Subtract(employee.DateOfBirth).TotalDays / 365 < 18)
            throw new Exception("Employee must be at least 18 years old.");

        if (employee.Salary <= 0)
            throw new Exception("Salary must be a positive value.");

        if (employee.Gender != "Male" && employee.Gender != "Female" && employee.Gender != "Other")
            throw new Exception("Invalid gender selected.");

        if (employee.ActiveStatus != "Active" && employee.ActiveStatus != "Inactive")
            throw new Exception("Invalid active status selected.");
    }
}


//using Microsoft.AspNetCore.Mvc;
//using ServerSide;

//[Route("api/[controller]")]
//[ApiController]
//public class EmployeeController : ControllerBase
//{
//    private static List<Employee> employees = new List<Employee>();
//    private static int nextId = 1;

//    [HttpGet]
//    [Route("GetAll")]
//    public IActionResult Get()
//    {
//        return Ok(employees);
//    }

//    [HttpGet]
//    [Route("GetById/{id:int}")]
//    public IActionResult Getbyid(int id)
//    {
//        var employee = employees.FirstOrDefault(e => e.Id == id);
//        if (employee == null)
//            return NotFound();

//        return Ok(employee);
//    }

//    [HttpPost]
//    [Route("Create")]
//    public IActionResult Create(Employee employee)
//    {
//        try
//        {
//            ValidateEmployee(employee);
//            employee.Id = nextId++;
//            employees.Add(employee);
//            return Ok(employee.Id);
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    [HttpPut("{id}")]
//    public IActionResult Update(int id, Employee employee)
//    {
//        try
//        {
//            var existingEmployee = employees.FirstOrDefault(e => e.Id == id);
//            if (existingEmployee == null)
//                return NotFound();

//            ValidateEmployee(employee);
//            existingEmployee.FirstName = employee.FirstName;
//            existingEmployee.LastName = employee.LastName;
//            existingEmployee.DateOfBirth = employee.DateOfBirth;
//            existingEmployee.Salary = employee.Salary;
//            existingEmployee.PhotoBase64 = employee.PhotoBase64 ?? existingEmployee.PhotoBase64;

//            existingEmployee.Gender = employee.Gender;
//            existingEmployee.Hobbies = employee.Hobbies;
//            existingEmployee.ActiveStatus = employee.ActiveStatus;

//            return Ok();
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    [HttpDelete("{id}")]
//    public IActionResult Delete(int id)
//    {
//        var employee = employees.FirstOrDefault(e => e.Id == id);
//        if (employee == null)
//            return NotFound();

//        employees.Remove(employee);
//        return Ok();
//    }

//    private void ValidateEmployee(Employee employee)
//    {
//        if (string.IsNullOrWhiteSpace(employee.FirstName) || employee.FirstName.Length > 50)
//            throw new Exception("First name is required and must be a maximum of 50 characters.");

//        if (string.IsNullOrWhiteSpace(employee.LastName) || employee.LastName.Length > 50)
//            throw new Exception("Last name is required and must be a maximum of 50 characters.");

//        if (DateTime.Now.Subtract(employee.DateOfBirth).TotalDays / 365 < 18)
//            throw new Exception("Employee must be at least 18 years old.");

//        if (employee.Salary <= 0)
//            throw new Exception("Salary must be a positive value.");

//        if (employee.Gender != "Male" && employee.Gender != "Female" && employee.Gender != "Other")
//            throw new Exception("Invalid gender selected.");

//        if (employee.ActiveStatus != "Active" && employee.ActiveStatus != "Inactive")
//            throw new Exception("Invalid active status selected.");
//    }
//}

