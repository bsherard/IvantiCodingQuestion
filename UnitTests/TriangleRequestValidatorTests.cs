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
    public class TriangleRequestValidatorTests
    {
        [NamedFact]
        [Trait("Category", "Unit")]
        public void IsRequestPositionValid_True()
        {
            var validator = new TriangleRequestValidator();
            var position1 = new TriangleGridPosition('A', 1);
            var position2 = new TriangleGridPosition('A', 2);
            var position3 = new TriangleGridPosition('B', 3);
            var position4 = new TriangleGridPosition('F', 12);
            string invalidMessage = null;

            var result = validator.IsRequestPositionValid(position1, out invalidMessage);
            Assert.True(result);
            Assert.Null(invalidMessage);

            result = validator.IsRequestPositionValid(position2, out invalidMessage);
            Assert.True(result);
            Assert.Null(invalidMessage);

            result = validator.IsRequestPositionValid(position3, out invalidMessage);
            Assert.True(result);
            Assert.Null(invalidMessage);

            result = validator.IsRequestPositionValid(position4, out invalidMessage);
            Assert.True(result);
            Assert.Null(invalidMessage);
        }

        [NamedFact]
        [Trait("Category", "Unit")]
        public void IsRequestPositionValid_False()
        {
            var validator = new TriangleRequestValidator();
            var position1 = new TriangleGridPosition('a', 1);
            var position2 = new TriangleGridPosition('A', 0);
            var position3 = new TriangleGridPosition('A', 13);
            var position4 = new TriangleGridPosition('G', 1);
            var position5 = new TriangleGridPosition('A', -1);
            var position6 = new TriangleGridPosition('1', 1);
            string invalidMessage = null;

            var result = validator.IsRequestPositionValid(position1, out invalidMessage);
            Assert.False(result);
            Assert.Equal($"{nameof(position1.Row)} must be contained in the set ['A', 'B', 'C', 'D', 'E', 'F']", invalidMessage);

            result = validator.IsRequestPositionValid(position2, out invalidMessage);
            Assert.False(result);
            Assert.Equal($"{nameof(position1.Column)} must be inclusively contained between 1 and 12", invalidMessage);

            result = validator.IsRequestPositionValid(position3, out invalidMessage);
            Assert.False(result);
            Assert.Equal($"{nameof(position1.Column)} must be inclusively contained between 1 and 12", invalidMessage);

            result = validator.IsRequestPositionValid(position4, out invalidMessage);
            Assert.False(result);
            Assert.Equal($"{nameof(position1.Row)} must be contained in the set ['A', 'B', 'C', 'D', 'E', 'F']", invalidMessage);

            result = validator.IsRequestPositionValid(position5, out invalidMessage);
            Assert.False(result);
            Assert.Equal($"{nameof(position1.Column)} must be inclusively contained between 1 and 12", invalidMessage);

            result = validator.IsRequestPositionValid(position6, out invalidMessage);
            Assert.False(result);
            Assert.Equal($"{nameof(position1.Row)} must be contained in the set ['A', 'B', 'C', 'D', 'E', 'F']", invalidMessage);
        }

        [NamedFact]
        [Trait("Category", "Unit")]
        public void IsRequestCoordinatesValid_True()
        {
            var validator = new TriangleRequestValidator();
            var coordinates1 = new TriangleCoordinates((0, 0), (9, 9), (0, 9));
            var coordinates2 = new TriangleCoordinates((9, 9), (9, 0), (0, 0));
            var coordinates3 = new TriangleCoordinates((10, 10), (19, 10), (19, 19));
            var coordinates4 = new TriangleCoordinates((19, 10), (19, 19), (10, 10));
            var coordinates5 = new TriangleCoordinates((19, 19), (10, 10), (19, 10));
            string invalidMessage = null;

            var result = validator.IsRequestCoordinatesValid(coordinates1, out invalidMessage);
            Assert.True(result);
            Assert.Null(invalidMessage);

            result = validator.IsRequestCoordinatesValid(coordinates2, out invalidMessage);
            Assert.True(result);
            Assert.Null(invalidMessage);

            result = validator.IsRequestCoordinatesValid(coordinates3, out invalidMessage);
            Assert.True(result);
            Assert.Null(invalidMessage);

            result = validator.IsRequestCoordinatesValid(coordinates4, out invalidMessage);
            Assert.True(result);
            Assert.Null(invalidMessage);

            result = validator.IsRequestCoordinatesValid(coordinates5, out invalidMessage);
            Assert.True(result);
            Assert.Null(invalidMessage);
        }

        [NamedFact]
        [Trait("Category", "Unit")]
        public void IsRequestCoordinatesValid_False()
        {
            var validator = new TriangleRequestValidator();
            var coordinates1 = new TriangleCoordinates((0, 0), (-9, 0), (-9, -9));
            var coordinates2 = new TriangleCoordinates((1, 1), (1, 0), (0, 0));
            var coordinates3 = new TriangleCoordinates((11, 11), (20, 11), (20, 20));
            var coordinates4 = new TriangleCoordinates((70, 70), (70, 79), (79, 79));
            var coordinates5 = new TriangleCoordinates((0, 0), (0, 0), (0, 0));
            var coordinates6 = new TriangleCoordinates((0, 0), (19, 19), (0, 19));
            var coordinates7 = new TriangleCoordinates((0, 0), (9, 0), (0, 9));
            string invalidMessage = null;

            var result = validator.IsRequestCoordinatesValid(coordinates1, out invalidMessage);
            Assert.False(result);
            Assert.Equal($"{nameof(TriangleCoordinates)} verticies must be contained in the following set [ 0, 9, 10, 19, 20, 29, 30, 39, 40, 49, 50, 59 ]", invalidMessage);


            result = validator.IsRequestCoordinatesValid(coordinates2, out invalidMessage);
            Assert.False(result);
            Assert.Equal($"{nameof(TriangleCoordinates)} verticies must be contained in the following set [ 0, 9, 10, 19, 20, 29, 30, 39, 40, 49, 50, 59 ]", invalidMessage);

            result = validator.IsRequestCoordinatesValid(coordinates3, out invalidMessage);
            Assert.False(result);
            Assert.Equal($"{nameof(TriangleCoordinates)} verticies must be contained in the following set [ 0, 9, 10, 19, 20, 29, 30, 39, 40, 49, 50, 59 ]", invalidMessage);

            result = validator.IsRequestCoordinatesValid(coordinates4, out invalidMessage);
            Assert.False(result);
            Assert.Equal($"{nameof(TriangleCoordinates)} verticies must be contained in the following set [ 0, 9, 10, 19, 20, 29, 30, 39, 40, 49, 50, 59 ]", invalidMessage);

            result = validator.IsRequestCoordinatesValid(coordinates5, out invalidMessage);
            Assert.False(result);
            Assert.Equal($"{nameof(TriangleCoordinates)} 2 sides must be of length {Math.Sqrt(100)}.", invalidMessage);

            result = validator.IsRequestCoordinatesValid(coordinates6, out invalidMessage);
            Assert.False(result);
            Assert.Equal($"{nameof(TriangleCoordinates)} 2 sides must be of length {Math.Sqrt(100)}.", invalidMessage);

            result = validator.IsRequestCoordinatesValid(coordinates7, out invalidMessage);
            Assert.False(result);
            Assert.Equal($"{nameof(TriangleCoordinates)} must have a hypotenus between the top left vertex and the bottom right.", invalidMessage);
        }
    }
}
