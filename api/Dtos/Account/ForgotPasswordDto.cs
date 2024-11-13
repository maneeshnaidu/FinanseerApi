using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class ForgotPasswordDto
    {
        [EmailAddress]
        public required string Email { get; set; }
    }
}