using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.EmplyeeSpecs
{
	public class EmployeeWithDepartmentSpecifications : BaseSpecifications<Employee>
	{
        public EmployeeWithDepartmentSpecifications() : base()
        {
            Includes.Add(e=> e.department);
        }

		public EmployeeWithDepartmentSpecifications(int id) : base(e=> e.ID == id)
		{
			Includes.Add(e => e.department);
		}

	}
}
