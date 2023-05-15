using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace F1Pulse.Pages.Stats;

public class LapTimesModel : PageModel
{
    public LapTimesModel()
    {
        List<SelectListItem> yearList = new List<SelectListItem>();
        for (int i = 2023; i >= 1996; i--)
        {
            yearList.Add(new SelectListItem{Value = i.ToString(), Text = i.ToString()});
        }

        YearList = yearList;
        Input = new InputModel();
    }
    
    public InputModel Input { get; set; }

    public List<SelectListItem> YearList { get; set; }
    public List<SelectListItem> RoundList { get; set; }

    public void OnGet()
    {
        
    }

    public class InputModel
    {
        public string Year { get; set; }
        public string Round { get; set; }
    }
}