using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOS;
using MagicVilla_VillaAPI.Repository.IReposository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillNumberAPI")]
    [ApiController]
    public class VillNumberController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IVillaNumberRepository _repository;
        private readonly IVillaRepository _villaRepository;

        public VillNumberController(IMapper mapper, IVillaNumberRepository repository, IVillaRepository villaRepository)
        {
            _response = new();
            _mapper = mapper;
            _repository = repository;
            _villaRepository = villaRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAllVillaNumbers() 
        {
            try
            {
                IEnumerable<VillaNumber> villanumberlist = await _repository.GetAll(includeproperty:"Villa");
                if(villanumberlist == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return _response;
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<List<VillaNumberDto>>(villanumberlist);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                _response.IsSuccess = false;
            }
            return _response;
            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}", Name = "GetVillNumbers")]
        public async Task<ActionResult<APIResponse>> GetVillNumbers(int id)
        {
            try
            {
                if(id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villanumber = await _repository.Get(u => u.VillaNo == id, includeproperty: "Villa");
                if(villanumber == null)
                {
                    _response.StatusCode=HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<VillaNumberDto>(villanumber);
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess=false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost(Name = "CreateVillNumber")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVillNumber([FromBody]VillaNumberCreateDto villaNumberCreateDto)
        {
            try
            {
                if(await _repository.Get(U=>U.VillaNo == villaNumberCreateDto.VillaNo) !=null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa Number already Exists!");
                    return BadRequest(ModelState);
                }               
                if (villaNumberCreateDto == null)
                {
                    return BadRequest(villaNumberCreateDto);
                }
                if(await _villaRepository.Get(u=>u.Id == villaNumberCreateDto.VillaID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "VillaID not Exists!");
                    return BadRequest(ModelState);
                }

                var villaNumber = _mapper.Map<VillaNumber>(villaNumberCreateDto);

                await _repository.Create(villaNumber);
                _response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillNumbers", new { id = villaNumber.VillaNo }, _response);

            }
            catch(Exception ex)
            {
                _response.IsSuccess=false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateVillNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVillNumber([FromBody]VillaNumberUpdateDto villaNumberUpdateDto,  int id)
        {
            try
            {
                if(villaNumberUpdateDto == null || id != villaNumberUpdateDto.VillaNo) 
                {
                  return BadRequest();
                }

                if (await _villaRepository.Get(u => u.Id == villaNumberUpdateDto.VillaID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "VillaID not Exists!");
                    return BadRequest(ModelState);
                }

                VillaNumber villaNumber = _mapper.Map<VillaNumber>(villaNumberUpdateDto);
                await _repository.Update(villaNumber);
                return Ok(_response);

            }
            catch(Exception ex)
            {
                _response.IsSuccess=false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }           
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteVillNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVillNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villaNumber = await _repository.Get(u => u.VillaNo == id);
                if (villaNumber == null)
                {
                    return NotFound();
                }
                await _repository.Remove(villaNumber);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess=false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            
            return _response;
        }


    }
}
