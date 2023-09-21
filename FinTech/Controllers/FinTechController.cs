using FinTech.Interfaces;
using FinTech.Models;
using FinTech.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace FinTech.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // // Defining the route for the controller


    public class FinTechController : ControllerBase //Defining the FinTechsController class which inherits from ControllerBase
    {


        private readonly ILogger<FinTechController> _logger;
        private readonly IFinTechRepository _fintechRepository;

        public FinTechController(ILogger<FinTechController> logger, IFinTechRepository fintechRepository) //// Defining the constructor for the class and injecting dependencies
        {

            _logger = logger;
            _fintechRepository = fintechRepository;
        }

        
        /// <summary>
        /// //This is a get request to Analyse mean, median and mode.
        /// </summary>
        /// <returns></returns>

        [HttpGet("Analyse")]
        public IActionResult Analyse()
        {
            return Ok(_fintechRepository.analyzeBill());

        }
    }
}
