using API.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Dtos;
using System.Reflection.Metadata.Ecma335;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineStatusCatalogsController : ControllerBase
    {
        // we create this sh**t to have access to all methods of IMachineStatusCatalog we'd created.
        private readonly IMachineStatusCatalog _context;
        private readonly IMapper _mapper;

        public MachineStatusCatalogsController(IMachineStatusCatalog machine, IMapper mapper)
        {
            _context = machine;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMachineStatusCatalogs()
        {
            var listMachine = _context.GetAll();
            var listMachineDto = new List<MachineStatusCatalogDto>();
            foreach (var list in listMachine)
            {
                listMachineDto.Add(_mapper.Map<MachineStatusCatalogDto>(list));
            }
            return Ok(listMachineDto);
        }

        [HttpGet("{id:int}", Name = "GetMachine")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMachine(int id)
        {
            var machine = _context.GetMachine(id);
            var machineDto = _mapper.Map<MachineStatusCatalogDto>(machine);
            return machineDto == null ? NotFound() : Ok(machine);

        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(MachineStatusCatalogDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateMachine([FromBody] MachineStatusCatalogDto machineDto)
        {
            // This s**t goes to the Dto and check out all the requirements are satified.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (machineDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_context.ExistMachineStatusCatalog(machineDto.Description))
            {
                ModelState.AddModelError("","The description already exists.");
                return StatusCode(404,ModelState);
            }
            var machine = _mapper.Map<MachineStatusCatalog>(machineDto);
            if (!_context.CreateMachineStatusCatalog(machine))
            {
                ModelState.AddModelError("", "Something went wrong when trying to create a new registry for machine status catalog.");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetMachine", new { id = machine.Id }, machine);
        }


        [HttpPatch(Name = "UpdatePatchMachine")]
        [ProducesResponseType(204, Type = typeof(MachineStatusCatalogDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePatchMachine( int machineId, [FromBody] MachineStatusCatalogIdDto machineIdDto)
        {
            // This s**t goes to the Dto and check out all the requirements are satified.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (machineIdDto == null || machineId != machineIdDto.Id)
            {
                return BadRequest(ModelState);
            }

            var machine = _mapper.Map<MachineStatusCatalog>(machineIdDto);
            if (!_context.UpdateMachineStatusCatalog(machine))
            {
                ModelState.AddModelError("", "Something went wrong when trying to update a new registry for machine status catalog.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete(Name = "DeleteMachine")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteMachine(int machineId)
        {
            if (!_context.ExistsMachineStatusCatalog(machineId))
            {
                return NotFound();
            }


            var machine = _context.GetMachine(machineId);

            if (!_context.DeleteMachineStatusCatalog(machine))
            {
                ModelState.AddModelError("", "Something went wrong when trying to delete machine instance.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
