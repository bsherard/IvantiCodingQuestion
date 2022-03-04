using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IvantiCodingQuestion.Models;

namespace IvantiCodingQuestion.Services
{
    public interface ITriangleGridService
    {
        public TriangleGridPosition GetPositionFromCoordinates(TriangleCoordinates coordinates);

        public TriangleCoordinates GetCoordinatesFromPosition(TriangleGridPosition position);
    }
}
