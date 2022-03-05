using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IvantiCodingQuestion.Models;

namespace IvantiCodingQuestion.Services
{
    public interface ITriangleRequestValidator
    {
        public bool IsRequestCoordinatesValid(TriangleCoordinates coordinates, out string invalidMessage);
        public bool IsRequestPositionValid(TriangleGridPosition position, out string invalidMessage);
    }
}
