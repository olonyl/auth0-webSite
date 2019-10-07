using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth0WebSite.ViewModels
{
    public class EmploymentViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Job")]
        public string Name { get; set; }

        [Display(Name = "Title")]
        public string Description { get; set; }
        public string Year { get; set; }
    }
}
