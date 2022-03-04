using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IvantiCodingQuestion.Models
{
    public class TriangleGridPosition
    {
        /// <summary>
        /// A to F.
        /// </summary>
        public char Row { get; }
        /// <summary>
        /// 1 to 12.
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// This stores the grid position of a triangle within a 6 by 6 grid.
        /// Each square of the grid contains 2 triangles, occupying both the bottom left and the top right.
        /// By convention, the column for the bottom left triangle is equal to 2 times the grid's column minus 1.
        /// The column for the top right triangle is one added to that.
        /// </summary>
        /// <param name="row">The row for the triangle.</param>
        /// <param name="column">The column for the triangle.</param>
        public TriangleGridPosition(char row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public override bool Equals(object other)
        {
            if (other is TriangleGridPosition p)
            {
                return (p.Row == this.Row && p.Column == this.Column);
            }

            return false;
        }

        public override int GetHashCode()
        {
            // todo: see if creating the tuple to do the hash is slower
            return (this.Row, this.Column).GetHashCode();
        }
    }
}
