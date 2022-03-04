using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IvantiCodingQuestion.Models;

namespace IvantiCodingQuestion.Services
{
    public class TriangleGridService : ITriangleGridService
    {
        private static readonly Dictionary<char, int> rowPositionMap = new Dictionary<char, int>() 
        { 
            { 'A', 0 }, { 'B', 10 }, { 'C', 20 }, { 'D', 30 }, { 'E', 40 }, { 'F', 50 } 
        };

        private static readonly Dictionary<TriangleGridPosition, TriangleCoordinates> gridCoordinatesMap;
        private static readonly Dictionary<TriangleCoordinates, TriangleGridPosition> coordinatesGridMap;

        static TriangleGridService()
        {
            TriangleGridService.gridCoordinatesMap = new Dictionary<TriangleGridPosition, TriangleCoordinates>();
            TriangleGridService.coordinatesGridMap = new Dictionary<TriangleCoordinates, TriangleGridPosition>();

            foreach (var row in rowPositionMap.Keys)
            {
                // 1, 3, ... ,11
                foreach (var oddColumns in Enumerable.Range(1, 12).Where(x => x % 2 == 1))
                {
                    (int X, int Y) topLeft = (((oddColumns - 1) / 2) * 10, TriangleGridService.rowPositionMap[row]);
                    (int X, int Y) bottomLeft = (topLeft.X, topLeft.Y + 9);
                    (int X, int Y) topRight = (topLeft.X + 9, topLeft.Y);
                    (int X, int Y) bottomRight = (topLeft.X + 9, topLeft.Y + 9);

                    var bottomLeftGridPosition = new TriangleGridPosition(row, oddColumns);
                    var topRightGridPosition = new TriangleGridPosition(row, oddColumns + 1);

                    var bottomLeftCoordinates = new TriangleCoordinates(bottomLeft, topLeft, bottomRight);
                    var topRightCoordinates = new TriangleCoordinates(topRight, topLeft, bottomRight);

                    TriangleGridService.gridCoordinatesMap[bottomLeftGridPosition] = bottomLeftCoordinates;
                    TriangleGridService.gridCoordinatesMap[topRightGridPosition] = topRightCoordinates;

                    TriangleGridService.coordinatesGridMap[bottomLeftCoordinates] = bottomLeftGridPosition;
                    TriangleGridService.coordinatesGridMap[topRightCoordinates] = topRightGridPosition;
                }
            }
        }

        public TriangleGridPosition GetPositionFromCoordinates(TriangleCoordinates coordinates)
        {
            return TriangleGridService.coordinatesGridMap[coordinates];
        }

        //public TriangleCoordinates GetCoordinatesFromPosition(TriangleGridPosition position)
        //{
        //    (int X, int Y) diagonalVertex1 = (((position.Column - 1)/2)*10, TriangleGrid.rowPositionMap[position.Row]);
        //    (int X, int Y) diagonalVertex2 = (diagonalVertex1.X + 10, diagonalVertex1.Y + 10);
        //    (int X, int Y) baseVertex;

        //    bool isBottom = (position.Column % 2) == 1;

        //    if (isBottom)
        //    {
        //        baseVertex = (diagonalVertex1.X, diagonalVertex1.Y + 10);
        //    }
        //    else
        //    {
        //        baseVertex = (diagonalVertex1.X + 10, diagonalVertex1.Y);
        //    }

        //    return new TriangleCoordinates(diagonalVertex1, diagonalVertex2, baseVertex);
        //}

        public TriangleCoordinates GetCoordinatesFromPosition(TriangleGridPosition position)
        {
            return TriangleGridService.gridCoordinatesMap[position];
        }
    }
}
