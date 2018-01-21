using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OazachaosuCore.Services;
using Repository;
using System;
using System.Net;
using System.Threading.Tasks;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Controllers
{
    [Route("[controller]")]
    public class UsersController : ApiControllerBase
    {

        private readonly IUserService userService;
        private IWordkiRepo Repository { get; set; }
        private readonly IMapper mapper;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{name}/{password}")]
        public async Task<IActionResult> Get(string name, string password)
        {
            UserDTO user = await userService.GetAsync(name, password);
            if(user == null)
            {
                return NotFound();
            }
            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO userDto)
        {
            if (string.IsNullOrEmpty(userDto.Name))
            {
                return StatusCode((int)HttpStatusCode.UnsupportedMediaType, $"Property: {nameof(userDto.Name)} is empty.");
            }
            if (string.IsNullOrEmpty(userDto.Password))
            {
                return StatusCode((int)HttpStatusCode.UnsupportedMediaType, $"Property: {nameof(userDto.Password)} is empty.");
            }
            try
            {
                await userService.RegisterAsync(userDto);
            }catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.Found);
            }
            return await Get(userDto.Name, userDto.Password);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDTO userDto)
        {
            try
            {
            await userService.UpdateAsync(userDto.ApiKey, userDto.Password, "newpassword");

            }catch(Exception e)
            {

            }
            return Json(await Get(userDto.Name, "newpassword"));
        }
    }
}
