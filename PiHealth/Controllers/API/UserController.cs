using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PiHealth.Web.Model;
using PiHealth.Service.UserAccounts;
using PiHealth.Web.MappingExtention;
using Microsoft.AspNetCore.Authorization;
using PiHealth.Controllers;
using PiHealth.DataModel.Entity;
using PiHealth.DataModel.Options;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using PiHealth.Services.UserAccounts;
using PiHealth.Web.Helper;
using PiHealth.Services;
using PiHealth.Web.Filters;

namespace PiHealth.Web.Controllers.API
{
    [Authorize]
    [Route("Api/User")]
    [Produces("application/json")]
    public class UserController : BaseApiController
    {
        private readonly IAppUserService _userService;
        private readonly SecurityService _securityService;
        private readonly AuditLogServices _auditLogService;
        private readonly ITokenService _tokenService;


        public UserController(
            IAppUserService userService,
            SecurityService securityService,
            AuditLogServices auditLogServices,
            ITokenService tokenService)
        {
            _userService = userService;
            _securityService = securityService;
            _auditLogService = auditLogServices;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GeAll([FromQuery] UserQueryModel model)
        {
            var user = _userService.GetAll(name: model?.name);
            var total = user.Count();
            var orderBy = string.IsNullOrEmpty(model?.order_by) ? "CreatedDate" : model.order_by;
            user = user.OrderByDescending(a => a.CreatedDate).Skip(model.skip);

            if (model.take > 0)
            {
                user = user.Take(model.take);
            }

            var result = user.Select(a => a.ToModel()).ToList();
            return Ok(new { result, total});
        }

        [HttpGet]
        [Route("Get/{id}")]        
        public IActionResult Get(long id)
        {
            var user = _userService.GetByID(id);
            var response = user?.ToModel(new UserModel());
            return Ok(response);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id == 0)
                return BadRequest();

            var entity = _userService.GetByID(id);
            entity.IsActive = false;
            await _userService.Update(entity);

            //get deleted user token
            var token = _tokenService.Get(id);

            //invalidate user token
            if (token != null)
                _tokenService.Delete(token);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");
            return Ok();
        }

        [HttpGet]
        [Route("GetUserTypes")]
        public IActionResult GetUserTypes(string status = null)
        {
            var usertypes = typeof(UserType).ToSelectListItems(selected: status).Select(a => new SelectListItem() { Selected = a.Selected, Text = a.Text, Value = a.Value }).ToList();

            return Ok(usertypes);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] UserModel model)
        {
            if (model == null)
                return BadRequest();

            var emailExist = _userService.EmailAlreadyExit(model.email);

            if (emailExist)
            {
                return BadRequest("Email");
            }

            var user = model.ToEntity(new AppUser());
            user.SerialNumber = new Guid().ToString();
            user.CreatedDate = DateTime.UtcNow;
            user.Password = _securityService.GetSha256Hash(model.password);
            user = await _userService.Create(user);
            user.IsActive = true;
            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success", value2: "Create");
            model = user.ToModel(new UserModel());
            return Ok(model);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UserModel model)
        {
            if (model == null)
                return BadRequest();

            var emailExist = _userService.EmailAlreadyExit(model.email, model.id);

            if (emailExist)
            {
                return BadRequest("Email");
            }

            var user = _userService.GetByID(model.id);
            user.Name = model.name;
            user.PhoneNo = model.phoneNo;
            user.Email = model.email;

            if (!string.IsNullOrEmpty(model.password))
            {
                user.Password = _securityService.GetSha256Hash(model.password);
            }

            user.Username = model.userName;
            user.UserType = model.userType;
            user.Gender = model.gender;
            user.Address = model.address;
            user.IsActive = true;
            await _userService.Update(user);
            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success", value2: "Update");
            return Ok(model);
        }

    }
}