import { Component, OnInit } from '@angular/core';
import { TrianglesService } from '../../shared/triangles.service';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';
import { TriangleCoordinates } from '../../shared/triangle-coordinates.model';
import { TriangleGridPosition } from '../../shared/triangle-grid-position.model';

@Component({
  selector: 'app-position-form',
  templateUrl: './position-form.component.html',
  styles: [
  ]
})
export class PositionFormComponent implements OnInit {
  constructor(public service: TrianglesService) {
  }

  //columnOptions: Array<number> = [1,2,3,4,5,6,7,8,9,10,11,12];
  formData: TriangleGridPosition = new TriangleGridPosition();

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    this.service.getCoordinatesFromPosition(this.formData);
  }

  toNumber() {
    this.formData.Column = +this.formData.Column;
  }
}
