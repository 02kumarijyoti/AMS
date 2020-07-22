using API.Dtos;
using API.Entintes;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IGenericRepository<Project> _projectRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProjectsController(IGenericRepository<Project> projectRepo, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _projectRepo = projectRepo;
        }
        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetProject(){
            var projects =await _projectRepo.ListAllAsync();
            return Ok(projects);
        }
         [HttpGet("{id}")]
         public async Task<ActionResult<List<Project>>> GetProjectById(int id){
            var project =await _projectRepo.GetByIdAsync(id);
            return Ok(project);
        }
         [HttpPost]
         public async Task<ActionResult<List<Project>>> createProject(ProjectDto projectDto){
            var project = _mapper.Map<ProjectDto,Project>(projectDto);
            _unitOfWork.Repository<Project>().Add(project);
            await _unitOfWork.Complete();
            return Ok(projectDto);
        }
        [HttpPut]
         public async Task<ActionResult<List<Project>>> UpdateProject(Project project){
            var projectResult= await _projectRepo.GetByIdAsync(project.Id);
             if(projectResult==null)   return BadRequest("Task not found");
                projectResult.Name=project.Name;
                projectResult.Status= project.Status;
              _unitOfWork.Repository<Project>().Update(projectResult);
                await _unitOfWork.Complete();
              return Ok(projectResult);
        }
 
         [HttpDelete("{id}")]
         public async Task<ActionResult<string>> DeleteProject(int id){
            var project = await _projectRepo.GetByIdAsync(id);
            if(project==null)  return BadRequest("Task not found");
            _unitOfWork.Repository<Project>().Delete(project);
            await _unitOfWork.Complete();
            return Ok();
        }
    }
}