using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOS;
using MagicVilla_VillaAPI.Repository.IReposository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/UserAuth")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly APIResponse _response;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
            _response = new();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _repository.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { "UserName or password is invalid" };
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        {
            bool ifUserNameUnique = _repository.IsuniqueUSer(model.UserName);
            if(!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { "UserName already exists" };
                return BadRequest(_response);
            }
            var user = await _repository.Register(model);
            if(user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest; 
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { "Error in while registering" };
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return BadRequest(_response);
        }
    }
}
