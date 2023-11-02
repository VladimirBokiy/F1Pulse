using Microsoft.AspNetCore.Mvc.Rendering;

namespace F1Pulse.Models.Stats;

public class ConstructorStandingsModel
{
    public int Year { get; set; }
    public List<SelectListItem> YearList { get; set; }

    public ConstructorStandingsModel()
    {
        List<SelectListItem> yearList = new List<SelectListItem>();
        for (int i = DateTime.Today.Year; i >= 1996; i--)
        {
            yearList.Add(new SelectListItem{Value = i.ToString(), Text = i.ToString()});
        }

        YearList = yearList;
    }
}