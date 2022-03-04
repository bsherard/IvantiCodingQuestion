import { Component, OnInit } from '@angular/core';
import { TriangleCoordinates } from '../shared/triangle-coordinates.model';
import { TriangleGridPosition } from '../shared/triangle-grid-position.model';
import { TrianglesService } from '../shared/triangles.service';

@Component({
  selector: 'app-triangles',
  templateUrl: './triangles.component.html',
  styles: [
  ]
})
export class TrianglesComponent implements OnInit {

  constructor(public service: TrianglesService) { }

  ngOnInit(): void { }
}
