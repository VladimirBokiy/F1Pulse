using F1Pulse.Models.Stats;
using Microsoft.AspNetCore.Mvc;

namespace F1Pulse.Controllers;

[Route("Stats/")]
public class StatsController : Controller
{
    [HttpGet]
    [Route("")]
    [Route("Menu")]
    public IActionResult Menu()
    {
        MenuModel model = new MenuModel();
        return View(model);
    }
    
    [HttpGet]
    [Route("ConstructorStandings")]
    public IActionResult ConstructorStandings()
    {
        ConstructorStandingsModel model = new ConstructorStandingsModel();
        return View(model);
    }
    
    [HttpGet]
    [Route("DriverStandings")]
    public IActionResult DriverStandings()
    {
        DriverStandingsModel model = new DriverStandingsModel();
        return View(model);
    }
    
    [HttpGet]
    [Route("LapPositions")]
    public IActionResult LapPositions()
    {
        LapPositionsModel model = new LapPositionsModel();
        return View(model);
    }

    [HttpGet]
    [Route("LapTimes")]
    public IActionResult LapTimes()
    {
        LapTimesModel model = new LapTimesModel();
        return View(model);
    }
    
    [HttpGet]
    [Route("QualifyingResults")]
    public IActionResult QualifyingResults()
    {
        QualifyingResultsModel model = new QualifyingResultsModel();
        return View(model);
    }
    
    [HttpGet]
    [Route("RaceResults")]
    public IActionResult RaceResults()
    {
        RaceResultsModel model = new RaceResultsModel();
        return View(model);
    }
}