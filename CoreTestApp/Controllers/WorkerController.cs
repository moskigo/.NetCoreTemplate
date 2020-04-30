using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreTestApp.Extensions;
using CoreTestApp.Model;
using CoreTestApp.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly TestAppDbContext context;
        private SignInManager<Worker> _signManager;
        private UserManager<Worker> _userManager;
        private CacheService _cacheService;
        private GenericCacheService<Worker> _gservice;

        public WorkerController(TestAppDbContext context, UserManager<Worker> userManager, SignInManager<Worker> signManager, CacheService cacheService, GenericCacheService<Worker> gservice)
        {
            this.context = context;
            _signManager = signManager;
            _userManager = userManager;
            _cacheService = cacheService;
            _gservice = gservice;
        }

        [HttpGet("current")]
        public IActionResult GetCurrentUser()
        {
            return Ok();
        }

        [HttpGet("oall")]
        [EnableQuery]
        public IEnumerable<WorkerViewModel> GetODataWorkers()
        {
            return _gservice.GetAll(true).GetViewModel();
            //return context.Workers.GetViewModel();
        }


        [HttpGet("all")]
        public IActionResult GetWorkers()
        {
            //return Ok(_gservice.GetAll().GetViewModel());
            //return Ok(context.Workers.GetViewModel());
            return Ok(context.Workers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkerById(string id)
        {
            //var worker = _cacheService.GetWorkerById(id);
            //return Ok(worker);

            var worker = await _gservice.GetById(id);
            return Ok(worker);
        }

        [HttpPost("create")]
        public async Task<ActionResult> AddWorker(Worker worker)
        {
            var user = new Worker
            {
                UserName = worker.Name.Split(" ")[0],
                Name = worker.Name,
                CompanyId = worker.CompanyId,
                Email = worker.Email,
            };

            var result = await _userManager.CreateAsync(user, worker.Password);

            return Ok(result.Succeeded);

        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn (WorkerLogin worker)
        {
            var res = await _signManager.PasswordSignInAsync(worker.Login, worker.Password, true, false);

            if (res.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Login or password invalid");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
             await _signManager.SignOutAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Worker worker)
        {
            var work = context.Users.FirstOrDefault(a => a.Id == worker.Id);

            if (work != null)
            {
                work.Name = worker.Name;
                work.Email = worker.Email;
                work.CompanyId = worker.CompanyId;
            }

            var res = await _userManager.UpdateAsync(work);

            return Ok(res.Succeeded);
           
        }
    }
}
