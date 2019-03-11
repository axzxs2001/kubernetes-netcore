using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webcore001.Respostory;

namespace webcore001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IUserRepository _userRepository;
        public HomeController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var osName = Environment.MachineName;
            var version = Environment.OSVersion.VersionString;
            return new JsonResult(new { OsName = osName, Version = version });
        }
        [HttpGet("/users")]
        public IActionResult Users()
        {
            return new JsonResult(_userRepository.GetUsers());
        }

        [HttpPost("/addval")]
        public async Task<string> AddVal([FromBody]InData inData)
        {
            try
            {
                var result = await _userRepository.AddVal(inData.Val);
                return result.ToString();
            }
            catch (Exception exc)
            {
                return "";
            }
        }
        
            
        [HttpGet("/health")]
        public IActionResult Health()
        {
            return Ok();
        }
        /// <summary>
        /// 等待时间
        /// </summary>
        /// <param name="second">秒</param>
        /// <returns></returns>
        [HttpGet("/waittime")]
        public IActionResult WaitTime(int second)
        {
            System.Threading.Thread.Sleep(second);
            return Ok(Environment.MachineName);
        }

        public class InData
        {
            public string Val { get; set; }
        }


       
    }
}
