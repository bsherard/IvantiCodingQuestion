export class TriangleCoordinates {
  Vertex1: Vertex;
  Vertex2: Vertex;
  Vertex3: Vertex;

  constructor() {
    this.Vertex1 = { X: 0, Y: 0 };
    this.Vertex2 = { X: 0, Y: 0 };
    this.Vertex3 = { X: 0, Y: 0 };
  }
}

interface Vertex {
  X: number;
  Y: number;
}
