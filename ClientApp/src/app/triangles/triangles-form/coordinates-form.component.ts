import { Component, OnInit } from '@angular/core';
import { TrianglesService } from '../../shared/triangles.service';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';
import { TriangleCoordinates } from '../../shared/triangle-coordinates.model';
import { TriangleGridPosition } from '../../shared/triangle-grid-position.model';

@Component({
  selector: 'app-coordinates-form',
  templateUrl: './coordinates-form.component.html',
  styles: [
  ]
})
export class CoordinatesFormComponent implements OnInit {
  constructor(public service: TrianglesService) {
  }

  formData: TriangleCoordinates = new TriangleCoordinates();

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    this.service.getPositionFromCoordinates(this.formData);
  }
}
