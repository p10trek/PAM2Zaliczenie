using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using PAM2Zaliczenie.DAL;
using PAM2Zaliczenie.Models;

namespace PAM2Zaliczenie.Controllers
{
    [ApiController]
    [Route("api/{userid?}")]
    public class TasksApiController : ControllerBase
    {

        private readonly ILogger<TasksApi> _logger;
        private readonly PAM_KillersDBContext _context;
        public TasksApiController(ILogger<TasksApi> logger, PAM_KillersDBContext _KillersDBContext)
        {
            _context = _KillersDBContext;
            _logger = logger;
           
        }

        [HttpGet]
        public IList<TasksApi> Get(string? userid)
        {
            if(String.IsNullOrEmpty(userid))
            {

                try
                {
                    var cookie = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                    var query = (from tasks in _context.Tasks.Where(row => row.UserId == new Guid(cookie))
                                 join employee in _context.Employee on tasks.EmployeeId equals employee.Id
                                 join taskType in _context.TaskType on tasks.TaskTypeId equals taskType.Id
                                 select new TasksApi
                                 {
                                     TaskName = taskType.Name,
                                     Employee = $"{employee.Name} {employee.Surname}",
                                     StartTime = tasks.StartTime,
                                     HoursToEnd = (tasks.StartTime - DateTime.Now).TotalHours,
                                     IsDone = (tasks.StartTime < DateTime.Now)? true:false,
                                     IsError = false,
                                     Error = null
                                 })?.ToList();
                    return query;
                }
                catch(Exception ex)
                {
                    return new List<TasksApi> { new TasksApi{
                        IsError = true,
                        Error = ex.Message
                    } };
                    
                }
            }
            else
            {
                try
                {
                    var query = (from tasks in _context.Tasks.Where(row => row.UserId == new Guid(userid))
                                 join employee in _context.Employee on tasks.EmployeeId equals employee.Id
                                 join taskType in _context.TaskType on tasks.TaskTypeId equals taskType.Id
                                 select new TasksApi
                                 {
                                     TaskName = taskType.Name,
                                     Employee = $"{employee.Name} {employee.Surname}",
                                     StartTime = tasks.StartTime,
                                     HoursToEnd = (tasks.StartTime - DateTime.Now).TotalHours,
                                     IsDone = (tasks.StartTime < DateTime.Now) ? true : false,
                                     IsError = false,
                                     Error = null
                                 })?.ToList();
                    return query;
                }
                catch (Exception ex) 
                {
                    return new List<TasksApi> { new TasksApi{
                        IsError = true,
                        Error = ex.Message
                    } };
                }
          
            }

        }
    }
}
