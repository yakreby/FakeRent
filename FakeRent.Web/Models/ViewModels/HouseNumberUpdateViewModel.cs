using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FakeRent.Web.Models.ViewModels
{
    public class HouseNumberUpdateViewModel
    {
        public HouseNumberUpdateViewModel()
        {
            HouseNumber = new HouseNumberUpdateDTO();
        }

        public HouseNumberUpdateDTO HouseNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> HouseList { get; set; }
    }
}
