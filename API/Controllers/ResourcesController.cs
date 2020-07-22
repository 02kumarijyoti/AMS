using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entintes;
using API.Interfaces;
using API.Specifications;
using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        // private readonly DataContext _context;
        private readonly IGenericRepository<Resource> _resourcesRepo;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ResourcesController(IGenericRepository<Resource> resourcesRepo, 
        IMapper mapper, IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
            _resourcesRepo = resourcesRepo;
        }
      
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Resource>>> GetResources()
        {
            var resources = await _resourcesRepo.ListAllAsync();
            return Ok(resources);
        }

         [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> GetResource(int id)
        {
            var resource = await _resourcesRepo.GetByIdAsync(id);
            return resource;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody]LoginDto loginDto)
        {
            var spec = new ResourceWithEmailAndPasswordSpecification(loginDto.Email);
            var  resource = await _resourcesRepo.GetEntityWithSpec(spec);
            if (resource==null) return Unauthorized("Unauthorize user");
            if(loginDto.Password != resource.Password) return BadRequest("Password not match");
            return new UserDto
            {
                Token = _tokenService.CreateToken(resource),
                Email = resource.Email,
                DisplayName =resource.Name
            };
         
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Resource>> Resource([FromBody]ResourceDto resourceDto)
        {
            var resource = _mapper.Map<ResourceDto,Resource>(resourceDto);
            _unitOfWork.Repository<Resource>().Add(resource);
            await _unitOfWork.Complete();
            return Ok(resourceDto);
        }

        [Authorize]
        [HttpPost("changepassword")]
        public async Task<ActionResult<Resource>> ChangePassword([FromBody]ChangePasswordDto changePassword)
        {
            var spec = new ResourceWithEmailAndPasswordSpecification(changePassword.Email,changePassword.Password);
            var  resource = await _resourcesRepo.GetEntityWithSpec(spec);
            if(resource == null) return BadRequest("Old password not match");
            resource.Password=changePassword.NewPassword;
            _unitOfWork.Repository<Resource>().Update(resource);
            await _unitOfWork.Complete();
            return Ok(resource);
        }
    }
}