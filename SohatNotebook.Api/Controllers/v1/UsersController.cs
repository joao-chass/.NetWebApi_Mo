using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SohatNotebook.DataService.Data;
using SohatNotebook.DataService.IConfiguratrionm;
using SohatNotebook.Entities.DbSet;
using SohatNotebook.Entities.Dtos.Incoming;

namespace SohatNotebook.Api.Controllers.v1
{
    public class UsersController : BaseController
    {
        public UsersController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        //Get
        [HttpGet]
        public async Task<IActionResult> Getusers()
        {
            var users = await _unitOfWork.Users.All();
            return Ok(users);
        }
        //Post
         [HttpPost]
        public async Task<IActionResult> AddUser(UserDto user)
        {
            var _user = new User();
            _user.LastName = user.LastName;
            _user.FirstName = user.FirstName;
            _user.Phone = user.Phone;
            _user.Email = user.Email;
            _user.DateOfBirth = Convert.ToDateTime(user.DateOfBirth);
            _user.Status = 1;
            _user.Country = user.Country;


            await _unitOfWork.Users.Add(_user);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetUser", new { id = _user.Id } , user); // return a 201
        }
       //Get by Id
       [HttpGet]
       [Route("GetUser", Name = "GetUser")]
       public async Task<IActionResult> GetUser(Guid id)
       {
        var user = await _unitOfWork.Users.GetById(id);
        return Ok(user);
       }
    }
}