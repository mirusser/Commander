using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Controllers
{
    //[Route("api/[controller]")] 
    //api/commands - with this route template, route would change with the change of the controller name

    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo repository;

        public CommandsController(ICommanderRepo commanderRepo)
        {
            repository = commanderRepo;
        }

        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = repository.GetAllCommands();

            return Ok(commandItems);
        }

        //GET api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var commandItem = repository.GetCommandById(id);

            return Ok(commandItem);
        }
    }
}
