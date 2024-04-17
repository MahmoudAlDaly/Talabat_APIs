using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications.EmplyeeSpecs;

namespace Talabat.APIs.Controllers
{
	
	public class EmployeeController : BaseAPIsController
	{
		private readonly IGenericRepository<Employee> EmployeeRepo;

		public EmployeeController(IGenericRepository<Employee> employeeRepo)
        {
			EmployeeRepo = employeeRepo;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
		{
			var spec = new EmployeeWithDepartmentSpecifications();

			var emp = await EmployeeRepo.GetWithSpecAsync(spec);

			return Ok(emp);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Employee>> GetEmployee(int id)
		{
			var spec = new EmployeeWithDepartmentSpecifications(id);

			var emp = await EmployeeRepo.GetWithSpecAsync(spec);

			if (emp is null)
			{
				return NotFound();
			}

			return Ok(emp);
		}
    }
}
