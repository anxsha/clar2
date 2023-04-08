using clar2.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using Microsoft.AspNetCore.Mvc;

namespace clar2.WebUI.Controllers;

public class WeatherForecastController : ApiControllerBase {
  [HttpGet]
  public async Task<IEnumerable<WeatherForecast>> Get() {
    return await Mediator.Send(new GetWeatherForecastsQuery());
  }
}
