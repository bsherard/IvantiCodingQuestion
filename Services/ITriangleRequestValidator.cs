using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IvantiCodingQuestion.Models;

namespace IvantiCodingQuestion.Services
{
    public interface ITriangleRequestValidator
    {
        public bool IsRequestCoordinatesValid(TriangleCoordinates coordinates);
        public bool IsRequestPositionValid(TriangleGridPosition position);
    }
}
