using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PreTaobao.Pages
{
    public class Test2 : PageModel
    {
        //[BindProperty]
        public List<string> Animals = new List<string>();
        public string Name { get; set; }
        public void OnGet()
        {
            Animals.AddRange(new[] { "Antelope", "Badger", "Cat", "Dog" });
        }
    }
}
