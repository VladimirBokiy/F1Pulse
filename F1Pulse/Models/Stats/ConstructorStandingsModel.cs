using Microsoft.AspNetCore.Mvc.Rendering;

namespace F1Pulse.Models.Stats;

public class ConstructorStandingsModel
{
    public int Year { get; set; }
    public List<SelectListItem> YearList { get; set; }
}