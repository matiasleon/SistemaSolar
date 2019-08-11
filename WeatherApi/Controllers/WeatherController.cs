using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Dtos;
using WeatherApi.Weathers;

namespace WeatherApi.Controllers
{
    [Route("api/clima")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherMachine weatherMachine;

        public WeatherController(WeatherMachine weatherMachine)
        {
            this.weatherMachine = weatherMachine;
        }

        [HttpGet]
        [Route("prediction")]
        public ActionResult<IEnumerable<PredictionDto>> Get()
        {
            try
            {
                var prediction = weatherMachine.Predict();
                return Ok(prediction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error en el pedido");
            }
        }

        [HttpGet]
        public ActionResult<WeatherPredictionDto> Get(int dia)
        {
            if (dia < 0)
            {
                return BadRequest("Dia incorrecto");
            }
           
            var weather = weatherMachine.PredictBy(dia);
            try
            {
                var prediction = new WeatherPredictionDto() { Clima = weather.Name, Dia = dia };

                return Ok(prediction);
            }
            catch (Exception)
            {;
                return StatusCode(500, "Error en el pedido");
            }
        }
    }
}