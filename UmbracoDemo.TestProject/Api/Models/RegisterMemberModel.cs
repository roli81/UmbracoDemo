using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoDemo.TestProject.Api.Models;

public class RegisterMemberModel
{

	public string Email { get; set; }

	public string Password { get; set; }

	public bool HasAPetUnicorn { get; set; }

	public string FirstName { get; set; }

	public string LastName { get; set; }

}

