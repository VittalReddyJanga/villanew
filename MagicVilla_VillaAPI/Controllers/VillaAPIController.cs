using AutoMapper;
using MagicVilla_VillaAPI.DATA;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOS;
using MagicVilla_VillaAPI.Repository.IReposository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/VillaAPI")]
    //[Route("api/[Controller]")]
    public class VillaAPIController : ControllerBase
    {
        // private readonly ILogger<VillaAPIController> _logger;
        // private readonly ILogging _logging;
        //private readonly ApplicationDbContect _db;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _repository;
        private readonly APIResponse _response;

        public VillaAPIController( IMapper  mapper, IVillaRepository repository) //ILogging logging, ApplicationDbContect db) //ILogger<VillaAPIController> logger) 
        {
            _mapper = mapper;
            _repository = repository;
            _response = new();

            //_logging = logging;
            //_db = db;
            //_logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            // _logger.LogInformation("Getting all villas");
            // _logging.Log("Getting all villas", "");
            // return Ok(VillaStore.VillaLists);
            //IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();

            try
            {
                IEnumerable<Villa> villaList = await _repository.GetAll();


                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<List<VillaDto>>(villaList);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    //  _logger.LogError("get vill error with id " +  id);
                    //_logging.Log("get vill error with id " +  id, "error");

                    return BadRequest();
                }
                //var villa = VillaStore.VillaLists.FirstOrDefault(u => u.Id == id);
                //var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);

                var villa = await _repository.Get(u => u.Id == id);

                if (villa == null)
                {
                    return NotFound();
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<VillaDto>(villa);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost( Name = "CreateVilla")]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDto villaCreateDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if(_db.Villas.FirstOrDefault(u=>u.Name.ToLower() == villaCreateDto.Name.ToLower()) != null)
            //{
            //    ModelState.AddModelError("customerror", "villa already exist!");
            //    return BadRequest(ModelState);
            //}

            try
            {
                if (_repository.Get(u => u.Name.ToLower() == villaCreateDto.Name.ToLower()).Result != null)
                {
                    ModelState.AddModelError("customerror", "villa already exist!");
                    return BadRequest(ModelState);
                }

                if (villaCreateDto == null)
                {
                    return BadRequest(villaCreateDto);
                }

                var villa = _mapper.Map<Villa>(villaCreateDto);
                //Villa villa = new()
                //{
                //    Amenity = villaCreateDto.Amenity,
                //    Details = villaCreateDto.Details,
                //    Name = villaCreateDto.Name,
                //    ImageUrl = villaCreateDto.ImageUrl,
                //    Occupancy = villaCreateDto.Occupancy,
                //    Rate = villaCreateDto.Rate,
                //    Sqft = villaCreateDto.Sqft
                //};

                // _db.Villas.Add(modal);
                await _repository.Create(villa);

                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = _mapper.Map<VillaDto>(villa);
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                //var villa = await _db.Villas.FirstOrDefaultAsync(u=>u.Id == id);
                var villa = await _repository.Get(u => u.Id == id);

                if (villa == null)
                {
                    return NotFound();
                }
                //_db.Villas.Remove(villa);
                //await _db.SaveChangesAsync();

                await _repository.Remove(villa);

                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name ="UpdateVilla")]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto)
        {
            try
            {
                if (villaUpdateDto == null || id != villaUpdateDto.Id)
                {
                    return BadRequest();
                }

                //Villa villa = new()
                //{
                //    Amenity = villaUpdateDto.Amenity,
                //    Details = villaUpdateDto.Details,
                //    Id = villaUpdateDto.Id,
                //    Name = villaUpdateDto.Name,
                //    ImageUrl = villaUpdateDto.ImageUrl,
                //    Occupancy = villaUpdateDto.Occupancy,
                //    Rate = villaUpdateDto.Rate,
                //    Sqft = villaUpdateDto.Sqft
                //};

                var villa = _mapper.Map<Villa>(villaUpdateDto);

                //_db.Villas.Update(villa);
                await _repository.Update(villa);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
