using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IMapper mapper;

        public CommandsController(
            ICommanderRepo commanderRepo,
            IMapper mapper)
        {
            repository = commanderRepo;
            this.mapper = mapper;
        }

        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            // var commandItems = repository.GetAllCommands();

            var mockRepo = new MockCommanderRepo();
            var commandItems = mockRepo.GetAllCommands();

            var commandReadDtos = mapper.Map<IEnumerable<CommandReadDto>>(commandItems);

            return Ok(commandReadDtos);
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = repository.GetCommandById(id);

            if (commandItem != null)
            {
                var commandReadDto = mapper.Map<CommandReadDto>(commandItem);

                return Ok(commandReadDto);
            }

            return NotFound();

        }

        //POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = mapper.Map<Command>(commandCreateDto);

            repository.CreateCommand(commandModel);
            repository.SaveChanges();

            var commandReadDto = mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { id = commandReadDto.Id }, commandReadDto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModel = repository.GetCommandById(id);

            if (commandModel == null)
            {
                return NotFound();
            }

            mapper.Map(commandUpdateDto, commandModel);

            repository.UpdateCommand(commandModel);
            repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModel = repository.GetCommandById(id);

            if (commandModel == null)
            {
                return NotFound();
            }

            var commandToPatch = mapper.Map<CommandUpdateDto>(commandModel);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            mapper.Map(commandToPatch, commandModel);

            repository.UpdateCommand(commandModel);
            repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModel = repository.GetCommandById(id);

            if (commandModel == null)
            {
                return NotFound();
            }

            repository.DeleteCommand(commandModel);
            repository.SaveChanges();

            return NoContent();
        }

    }
}
