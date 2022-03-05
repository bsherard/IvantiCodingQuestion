using IvantiCodingQuestion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IvantiCodingQuestion.Services
{
    public class TriangleRequestValidator : ITriangleRequestValidator
    {
        private static readonly HashSet<int> coordinateRange = new HashSet<int>() { 0, 9, 10, 19, 20, 29, 30, 39, 40, 49, 50, 59 };
        private static readonly (int Minimum, int Maximum) gridColumnRange = (1, 12);
        private static readonly HashSet<char> gridRows = new HashSet<char>() { 'A', 'B', 'C', 'D', 'E', 'F'};
        private static readonly double triangleHypotenusLength = Math.Sqrt(200);
        private static readonly double triangleSideLength = Math.Sqrt(100);

        public bool IsRequestCoordinatesValid(TriangleCoordinates coordinates, out string invalidMessage)
        {
            if (coordinates == null)
            {
                invalidMessage = $"{nameof(TriangleCoordinates)} is null";
                return false;
            }

            if (
                   !TriangleRequestValidator.IsVertexInRange(coordinates.Vertex1) ||
                   !TriangleRequestValidator.IsVertexInRange(coordinates.Vertex2) ||
                   !TriangleRequestValidator.IsVertexInRange(coordinates.Vertex3)
               )
            {
                invalidMessage = $"{nameof(TriangleCoordinates)} verticies must be contained in the following set [ 0, 9, 10, 19, 20, 29, 30, 39, 40, 49, 50, 59 ]";
                return false;
            }

            if (!TriangleRequestValidator.IsVerticiesATriangle(coordinates, out string m))
            {
                invalidMessage = m;
                return false;
            }

            invalidMessage = null;
            return true;
        }

        public bool IsRequestPositionValid(TriangleGridPosition position, out string invalidMessage)
        {
            if (position == null)
            {
                invalidMessage = $"{nameof(TriangleGridPosition)} is null";
                return false;
            }

            if (!TriangleRequestValidator.gridRows.Contains(position.Row))
            {
                invalidMessage = $"{nameof(position.Row)} must be contained in the set ['A', 'B', 'C', 'D', 'E', 'F']";
                return false;
            }

            if (position.Column < TriangleRequestValidator.gridColumnRange.Minimum || position.Column > TriangleRequestValidator.gridColumnRange.Maximum)
            {
                invalidMessage = $"{nameof(position.Column)} must be inclusively contained between 1 and 12";
                return false;
            }

            invalidMessage = null;
            return true;
        }

        /// <summary>
        /// Checks that the verticies make up a triangle of expected shape, short circuiting
        /// the checks to minimize operations. Also ensures that the hypotenus is formed by
        /// a vertex on the top left and the bottom right.
        /// </summary>
        /// <param name="coordinates">The coordinates to be validated.</param>
        /// <param name="invalidMessage">The message describing why the coordinates are invalid.</param> 
        /// <returns></returns>
        private static bool IsVerticiesATriangle(TriangleCoordinates coordinates, out string invalidMessage)
        {
            bool foundHypotenus = false;

            var length1 = TriangleRequestValidator.GetVectorLength(coordinates.Vertex1, coordinates.Vertex2);

            if (length1 == TriangleRequestValidator.triangleHypotenusLength)
            {
                if (!IsTopLeftBottomRightHypotenus(coordinates.Vertex1, coordinates.Vertex2))
                {
                    invalidMessage = $"{nameof(TriangleCoordinates)} must have a hypotenus between the top left vertex and the bottom right.";
                    return false;
                }

                foundHypotenus = true;
            }
            else if (length1 != TriangleRequestValidator.triangleSideLength)
            {
                invalidMessage = $"{nameof(TriangleCoordinates)} 2 sides must be of length {triangleSideLength}.";
                return false;
            }

            var length2 = TriangleRequestValidator.GetVectorLength(coordinates.Vertex2, coordinates.Vertex3);

            if (length2 == TriangleRequestValidator.triangleHypotenusLength)
            {
                if (foundHypotenus)
                {
                    invalidMessage = $"{nameof(TriangleCoordinates)} 2 sides must be of length {triangleSideLength}.";
                    return false;
                }

                if (!IsTopLeftBottomRightHypotenus(coordinates.Vertex2, coordinates.Vertex3))
                {
                    invalidMessage = $"{nameof(TriangleCoordinates)} must have a hypotenus between the top left vertex and the bottom right.";
                    return false;
                }

                foundHypotenus = true;
            }
            else if (length2 != TriangleRequestValidator.triangleSideLength)
            {
                invalidMessage = $"{nameof(TriangleCoordinates)} 2 sides must be of length {triangleSideLength}.";
                return false;
            }

            var length3 = TriangleRequestValidator.GetVectorLength(coordinates.Vertex3, coordinates.Vertex1);

            if (length3 == TriangleRequestValidator.triangleHypotenusLength)
            {
                if (foundHypotenus)
                {
                    invalidMessage = $"{nameof(TriangleCoordinates)} 2 sides must be of length {triangleSideLength}.";
                    return false;
                }

                if (!IsTopLeftBottomRightHypotenus(coordinates.Vertex3, coordinates.Vertex1))
                {
                    invalidMessage = $"{nameof(TriangleCoordinates)} must have a hypotenus between the top left vertex and the bottom right.";
                    return false;
                }

                foundHypotenus = true;
            }
            else if (length3 != TriangleRequestValidator.triangleSideLength)
            {
                invalidMessage = $"{nameof(TriangleCoordinates)} 2 sides must be of length {triangleSideLength}.";
                return false;
            }

            invalidMessage = null;
            return foundHypotenus;
        }

        /// <summary>
        /// Gets a pixel length between two verticies. Because this is discrete whole pixels, 
        /// the distance between say pixel 0 and pixel 9 is |9 - 0| + 1 = 10. However, this
        /// formula doesn't apply for the distance between pixel 0 and pixel 0, which should
        /// return 0.
        /// </summary>
        /// <param name="vertex1">The first vertex.</param>
        /// <param name="vertex2">The second vertex.</param>
        /// <returns></returns>
        private static double GetVectorLength((int X, int Y) vertex1, (int X, int Y) vertex2)
        {
            var deltaX = Math.Abs(vertex1.X - vertex2.X);
            deltaX = (deltaX == 0) ? deltaX : deltaX + 1;
            
            var deltaY = Math.Abs(vertex1.Y - vertex2.Y);
            deltaY = (deltaY == 0) ? deltaY : deltaY + 1;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        private static bool IsTopLeftBottomRightHypotenus((int X, int Y) vertex1, (int X, int Y) vertex2)
        {
            return (vertex1.X == vertex2.X + 9 && vertex1.Y == vertex2.Y + 9) || 
                   (vertex1.X == vertex2.X - 9 && vertex1.Y == vertex2.Y - 9);
        }

        private static bool IsVertexInRange((int X, int Y) vertex)
        {
            return TriangleRequestValidator.coordinateRange.Contains(vertex.X) && TriangleRequestValidator.coordinateRange.Contains(vertex.Y);
        }
    }
}
