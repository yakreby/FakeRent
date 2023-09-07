using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FakeRent.Web.Models.ViewModels
{
    public class HouseNumberDeleteViewModel
    {
        public HouseNumberDeleteViewModel()
        {
            HouseNumber = new HouseNumberDTO();
        }

        public HouseNumberDTO HouseNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> HouseList { get; set; }
    }
}
