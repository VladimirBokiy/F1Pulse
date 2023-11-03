using Microsoft.AspNetCore.Mvc.Rendering;

namespace F1Pulse.Models.Stats;

public class LapTimesModel
{
    public List<SelectListItem> YearList { get; set; }
    public List<SelectListItem> RoundList { get; set; }
    public int Year { get; set; }
    public int Round { get; set; }
    
    public LapTimesModel()
    {
        List<SelectListItem> yearList = new List<SelectListItem>();
        for (int i = DateTime.Today.Year; i >= 1996; i--)
        {
            yearList.Add(new SelectListItem{Value = i.ToString(), Text = i.ToString()});
        }

        YearList = yearList;
    }
}