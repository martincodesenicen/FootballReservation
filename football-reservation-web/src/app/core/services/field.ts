import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateFieldDto, FieldDto } from '../models/field.models';

@Injectable({
  providedIn: 'root'
})
export class FieldService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5035/api/fields'; // Ajusta el puerto según tu API

  constructor() {}

  getAllFields(): Observable<FieldDto[]> {
    return this.http.get<FieldDto[]>(this.apiUrl);
  }

  getFieldById(id: number): Observable<FieldDto> {
    return this.http.get<FieldDto>(`${this.apiUrl}/${id}`);
  }

  createField(fieldData: CreateFieldDto): Observable<FieldDto> {
    return this.http.post<FieldDto>(this.apiUrl, fieldData);
  }
}