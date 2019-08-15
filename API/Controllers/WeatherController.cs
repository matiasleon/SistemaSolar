using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using API.Dtos;
using API.Weathers;
using API.Business.Weathers.Results;

namespace API.Controllers
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
        [Route("prediccion")]
        public ActionResult<IEnumerable<Prediction>> Get()
        {
            try
            {
                var prediction = weatherMachine.Predict();
                return Ok(prediction);
            }
            catch (Exception)
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
           
            try
            {
                var weather = weatherMachine.Predict(dia);
                var prediction = new WeatherPredictionDto() { Clima = weather.Name, Dia = dia };

                return Ok(prediction);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error en el pedido");
            }
        }
    }
}