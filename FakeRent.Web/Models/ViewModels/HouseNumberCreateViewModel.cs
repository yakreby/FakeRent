using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FakeRent.Web.Models.ViewModels
{
	public class HouseNumberCreateViewModel
	{
		public HouseNumberCreateViewModel()
		{
			HouseNumber = new HouseNumberCreateDTO();
		}

		public HouseNumberCreateDTO HouseNumber { get; set; }
		[ValidateNever]
        public IEnumerable<SelectListItem> HouseList { get; set; }
    }
}
