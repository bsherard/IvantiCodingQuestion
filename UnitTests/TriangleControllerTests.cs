using Microsoft.AspNetCore.Mvc;
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
    public class TriangleControllerTests
    {
        [NamedFact]
        [Trait("Category", "Unit")]
        public void GetCoordinates_Status200()
        {
            var mockServiceResponse = new TriangleCoordinates((0,1), (2,3), (4,5));
            var mockService = new Mock<ITriangleGridService>();
            mockService
                .Setup(service => service.GetCoordinatesFromPosition(It.IsAny<TriangleGridPosition>()))
                .Returns(mockServiceResponse);

            var mockValidator = new Mock<ITriangleRequestValidator>();
            string invalidMessage;
            mockValidator
                .Setup(validator => validator.IsRequestPositionValid(It.IsAny<TriangleGridPosition>(), out invalidMessage))
                .Returns(true);

            var expectedResult = new ActionResult<TriangleCoordinates>(mockServiceResponse);

            var controller = new TriangleController(mockService.Object, mockValidator.Object);

            var result = controller.GetCoordinates(null);

            Assert.IsType(expectedResult.GetType(), result);
            Assert.IsType<OkObjectResult>(result.Result);
            
            Assert.Equal(expectedResult.Value, (result.Result as OkObjectResult).Value);
        }

        [NamedFact]
        [Trait("Category", "Unit")]
        public void GetCoordinates_Status400()
        {
            var mockService = new Mock<ITriangleGridService>();
            var mockValidator = new Mock<ITriangleRequestValidator>();
            string invalidMessage;
            mockValidator
                .Setup(validator => validator.IsRequestPositionValid(It.IsAny<TriangleGridPosition>(), out invalidMessage))
                .Returns(false);

            var controller = new TriangleController(mockService.Object, mockValidator.Object);

            var result = controller.GetCoordinates(null);

            Assert.IsType<ActionResult<TriangleCoordinates>>(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [NamedFact]
        [Trait("Category", "Unit")]
        public void GetPosition_Status200()
        {
            var mockServiceResponse = new TriangleGridPosition('A', 1);
            var mockService = new Mock<ITriangleGridService>();
            mockService
                .Setup(service => service.GetPositionFromCoordinates(It.IsAny<TriangleCoordinates>()))
                .Returns(mockServiceResponse);

            var mockValidator = new Mock<ITriangleRequestValidator>();
            string invalidMessage;
            mockValidator
                .Setup(validator => validator.IsRequestCoordinatesValid(It.IsAny<TriangleCoordinates>(), out invalidMessage))
                .Returns(true);

            var expectedResult = new ActionResult<TriangleGridPosition>(mockServiceResponse);

            var controller = new TriangleController(mockService.Object, mockValidator.Object);

            var result = controller.GetGridPosition(null);

            Assert.IsType(expectedResult.GetType(), result);
            Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(expectedResult.Value, (result.Result as OkObjectResult).Value);
        }

        [NamedFact]
        [Trait("Category", "Unit")]
        public void GetPosition_Status400()
        {
            var mockService = new Mock<ITriangleGridService>();
            var mockValidator = new Mock<ITriangleRequestValidator>();
            string invalidMessage;
            mockValidator
                .Setup(validator => validator.IsRequestPositionValid(It.IsAny<TriangleGridPosition>(), out invalidMessage))
                .Returns(false);

            var controller = new TriangleController(mockService.Object, mockValidator.Object);

            var result = controller.GetGridPosition(null);

            Assert.IsType<ActionResult<TriangleGridPosition>>(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
