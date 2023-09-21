using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Models
{
	public class employees
	{
		[Key]
		public int emp_no { get; set; }

		public DateTime dob { get; set; }

		public string first_name { get; set; }
		public string last_name { get; set; }
		public string gender { get; set; }
		public DateTime joining { get; set; }
		public string dept { get; set; }
	}

	[Keyless]
	public class dept_emp
	{
		
		public int emp_no { get; set; }

		public DateTime from_date { get; set; }

		public string dept_no { get; set; }
		public DateTime to_date { get; set; }
	}
}
