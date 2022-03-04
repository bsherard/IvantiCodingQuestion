using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IvantiCodingQuestion.Models
{
    public class TriangleCoordinates
    {
        [JsonConverter(typeof(VertexJsonConverter))]
        public (int X, int Y) Vertex1 { get; }
        [JsonConverter(typeof(VertexJsonConverter))]
        public (int X, int Y) Vertex2 { get; }
        [JsonConverter(typeof(VertexJsonConverter))]
        public (int X, int Y) Vertex3 { get; }

        /// <summary>
        /// This holds 3 pairs of x,y points. The order is agnostic for requests;
        /// a request may provide them in any manner and still be understood.
        /// </summary>
        /// <param name="vertex1">The first x,y point.</param>
        /// <param name="vertex2">The second x,y point.</param>
        /// <param name="vertex3">The third x,y point.</param>
        public TriangleCoordinates((int X, int Y) vertex1, (int X, int Y) vertex2, (int X, int Y) vertex3)
        {
            this.Vertex1 = vertex1;
            this.Vertex2 = vertex2;
            this.Vertex3 = vertex3;
        }

        public override bool Equals(object other)
        {
            if (other is TriangleCoordinates c)
            {
                // order of verticies is unimportant
                return 
                    // todo: find a cleaner solution that still runs quickly. Maybe decerement from a dictionary with a count.
                    (c.Vertex1 == this.Vertex1 && c.Vertex2 == this.Vertex2 && c.Vertex3 == this.Vertex3) ||
                    (c.Vertex1 == this.Vertex1 && c.Vertex3 == this.Vertex2 && c.Vertex2 == this.Vertex3) ||
                    (c.Vertex2 == this.Vertex1 && c.Vertex1 == this.Vertex2 && c.Vertex3 == this.Vertex3) ||
                    (c.Vertex2 == this.Vertex1 && c.Vertex3 == this.Vertex2 && c.Vertex1 == this.Vertex3) ||
                    (c.Vertex3 == this.Vertex1 && c.Vertex1 == this.Vertex2 && c.Vertex2 == this.Vertex3) ||
                    (c.Vertex3 == this.Vertex1 && c.Vertex2 == this.Vertex2 && c.Vertex1 == this.Vertex3);
            }
            
            return false;
        }

        public override int GetHashCode()
        {
            unchecked // Allow arithmetic overflow, numbers will just "wrap around"
            {
                int hashcode = 1430287;
                hashcode *= 7302013 ^ this.Vertex1.GetHashCode();
                hashcode *= 7302013 ^ this.Vertex2.GetHashCode();
                hashcode *= 7302013 ^ this.Vertex3.GetHashCode();

                return hashcode;
            }

            // todo: see if creating the value tuple to do the hash is slower
            //return (this.Vertex1, this.Vertex2, this.Vertex3).GetHashCode();
        }
    }
}
