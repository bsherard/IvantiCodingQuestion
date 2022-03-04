using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using IvantiCodingQuestion.Controllers;
using IvantiCodingQuestion.Services;
using IvantiCodingQuestion.Models;
using UnitTests.Utilities;

namespace UnitTests
{
    public class TriangleGridServiceTests
    {
        [NamedFact]
        [Trait("Category", "Unit")]
        public void GetCoordinatesFromPosition()
        {
            var service = new TriangleGridService();

            var coordinatePositionPair1 = (Coordinate: new TriangleCoordinates((9, 9), (0, 0), (0, 9)), Position: new TriangleGridPosition('A', 1));
            var coordinatePositionPair2 = (Coordinate: new TriangleCoordinates((9, 9), (0, 0), (9, 0)), Position: new TriangleGridPosition('A', 2));
            var coordinatePositionPair3 = (Coordinate: new TriangleCoordinates((19, 19), (10, 10), (10, 19)), Position: new TriangleGridPosition('B', 3));
            var coordinatePositionPair4 = (Coordinate: new TriangleCoordinates((59, 59), (50, 50), (59, 50)), Position: new TriangleGridPosition('F', 12));

            var result = service.GetCoordinatesFromPosition(coordinatePositionPair1.Position);
            Assert.Equal(coordinatePositionPair1.Coordinate, result);

            result = service.GetCoordinatesFromPosition(coordinatePositionPair2.Position);
            Assert.Equal(coordinatePositionPair2.Coordinate, result);

            result = service.GetCoordinatesFromPosition(coordinatePositionPair3.Position);
            Assert.Equal(coordinatePositionPair3.Coordinate, result);

            result = service.GetCoordinatesFromPosition(coordinatePositionPair4.Position);
            Assert.Equal(coordinatePositionPair4.Coordinate, result);
        }

        [NamedFact]
        [Trait("Category", "Unit")]
        public void GetPositionFromCoordinates()
        {
            var service = new TriangleGridService();

            var coordinatePositionPair1 = (Coordinate: new TriangleCoordinates((9, 9), (0, 0), (0, 9)), Position: new TriangleGridPosition('A', 1));
            var coordinatePositionPair2 = (Coordinate: new TriangleCoordinates((9, 9), (0, 0), (9, 0)), Position: new TriangleGridPosition('A', 2));
            var coordinatePositionPair3 = (Coordinate: new TriangleCoordinates((19, 19), (10, 10), (10, 19)), Position: new TriangleGridPosition('B', 3));
            var coordinatePositionPair4 = (Coordinate: new TriangleCoordinates((59, 59), (50, 50), (59, 50)), Position: new TriangleGridPosition('F', 12));

            var result = service.GetPositionFromCoordinates(coordinatePositionPair1.Coordinate);
            Assert.Equal(coordinatePositionPair1.Position, result);

            result = service.GetPositionFromCoordinates(coordinatePositionPair2.Coordinate);
            Assert.Equal(coordinatePositionPair2.Position, result);

            result = service.GetPositionFromCoordinates(coordinatePositionPair3.Coordinate);
            Assert.Equal(coordinatePositionPair3.Position, result);

            result = service.GetPositionFromCoordinates(coordinatePositionPair4.Coordinate);
            Assert.Equal(coordinatePositionPair4.Position, result);
        }
    }
}
