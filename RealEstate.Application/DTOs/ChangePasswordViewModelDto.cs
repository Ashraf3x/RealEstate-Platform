using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.DTOs
{
    public class ChangePasswordViewModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
