using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IvantiCodingQuestion.Models;
using IvantiCodingQuestion.Services;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IvantiCodingQuestion.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TriangleController : ControllerBase
    {
        private ITriangleGridService TriangleGridService { get; set; }
        private ITriangleRequestValidator TriangleRequestValidator { get; set; }

        public TriangleController(ITriangleGridService triangleGridService, ITriangleRequestValidator triangleRequestValidator)
        {
            this.TriangleGridService = triangleGridService;
            this.TriangleRequestValidator = triangleRequestValidator;
        }

        // Using POST instead of GET since angular doesn't support GET request with a body(even though the HTTP standard permits this)
        [HttpPost("coordinates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TriangleCoordinates> GetCoordinates(TriangleGridPosition position)
        {
            if (this.TriangleRequestValidator.IsRequestPositionValid(position))
            {
                return Ok(this.TriangleGridService.GetCoordinatesFromPosition(position));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("position")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TriangleGridPosition> GetGridPosition(TriangleCoordinates coordinates)
        {
            if (this.TriangleRequestValidator.IsRequestCoordinatesValid(coordinates))
            {
                return this.TriangleGridService.GetPositionFromCoordinates(coordinates);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
