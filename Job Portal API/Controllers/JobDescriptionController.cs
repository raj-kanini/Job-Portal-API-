﻿using Job_Portal_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Job_Portal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobDescriptionController : ControllerBase
    {
        private readonly JobDBContext _dbContext;

        public JobDescriptionController(JobDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/JobDescription
        [HttpGet]
        public IActionResult Get()
        {
            var jobDescriptions = _dbContext.JobDescription.ToList();
            return Ok(jobDescriptions);
        }

        // GET: api/JobDescription/1 (for example, to get a job description by ID)
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var jobDescription = _dbContext.JobDescription.Find(id);

            if (jobDescription == null)
            {
                return NotFound(); // 404 Not Found if the job description with the given ID is not found.
            }

            return Ok(jobDescription);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Job_Description jobDescription)
        {
            if (jobDescription == null)
            {
                return BadRequest("JobDescription data is invalid.");
            }

            try
            {
                _dbContext.JobDescription.Add(jobDescription);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction("Get", new { id = jobDescription.Id }, jobDescription);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
