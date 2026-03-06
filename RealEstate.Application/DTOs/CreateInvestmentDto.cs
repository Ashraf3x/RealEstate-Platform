using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.DTOs
{
    public class CreateInvestmentDto
    {
        public int UserId {  get; set; }
        public int PropertyId { get; set; }
        public int ShareCount { get; set; }
    }
}
