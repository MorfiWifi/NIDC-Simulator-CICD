using System;
using System.ComponentModel;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services.Identity;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    [EnableCors("AllowAll")]
    public class BaseController : ControllerBase
    {
        private readonly AutoMapper.IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private IMapper mapper;

        public BaseController(IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
           
        }
        public BaseController()
        {
           
        }


        public IMapper Mapper => _mapper;
        private T GetClaim<T>(string type)
        {
            var value = User.FindFirstValue(type) ?? "";

            var converter = TypeDescriptor.GetConverter(typeof(T));

            try
            {
                return (T)converter.ConvertFromString(value);
            }
            catch
            {
                return default;
            }


        }
        public string WebRootPath => _hostingEnvironment.ContentRootPath + "/MyStaticFiles";
        private CustomUser GetUser()
        {
            return new CustomUser
            {
                FirstName = GetClaim<string>(ITokenIssuer.Claims.Firstname),
                Id = GetClaim<Guid>(ITokenIssuer.Claims.Id),
                LastName = GetClaim<string>(ITokenIssuer.Claims.Lastname),
                Username = GetClaim<string>(ITokenIssuer.Claims.Username),
                Roles = GetClaim<string>(ClaimTypes.Role).Split(',')
            };
        }

        public CustomUser CustomUser => GetUser();
    }

    public class CustomUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Guid Id { get; set; }
        public string[] Roles { get; set; }
    }
}
