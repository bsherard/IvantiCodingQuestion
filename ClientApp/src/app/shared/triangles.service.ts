import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { BehaviorSubject, Observable } from 'rxjs';
import { TriangleCoordinates } from './triangle-coordinates.model';
import { TriangleGridPosition } from './triangle-grid-position.model';

@Injectable({
  providedIn: 'root'
})
export class TrianglesService {

  private coordinatesUrl = "";
  private positionUrl = "";

  private positionSubject = new BehaviorSubject<TriangleGridPosition>(null);
  private positionStore: { position: TriangleGridPosition } = { position: null };
  readonly positionObservable = this.positionSubject.asObservable();

  private positionErrorSubject = new BehaviorSubject<string>(null);
  private positionErrorStore: { error: string } = { error: null };
  readonly positionErrorObservable = this.positionErrorSubject.asObservable();

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.coordinatesUrl = baseUrl + "Triangle/coordinates";
    this.positionUrl = baseUrl + "Triangle/position";
  }

  getPositionFromCoordinates(coordinates: TriangleCoordinates) {
    this.http.post<TriangleGridPosition>(this.positionUrl, coordinates)
      .subscribe(
        (response: TriangleGridPosition) => {
          this.positionStore.position = response;
          this.positionSubject.next(Object.assign({}, this.positionStore).position);
        },
        (errorResponse: HttpErrorResponse) => {
          this.positionStore.position = null;
          this.positionSubject.next(Object.assign({}, this.positionStore).position);

          this.positionErrorStore.error = errorResponse.error;
          this.positionErrorSubject.next(Object.assign({}, this.positionErrorStore).error);
          console.log(errorResponse);
        }
      )
  }

  getCoordinatesFromPosition(position: TriangleGridPosition): Observable<TriangleCoordinates> {
    return this.http.post<TriangleCoordinates>(this.coordinatesUrl, position);
  }
}
