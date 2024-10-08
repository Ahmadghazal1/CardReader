import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment ';
import { Observable } from 'rxjs';
import { ICardReader } from '../Models/CallReader.model';

@Injectable({
  providedIn: 'root'
})
export class CardreaderService {
  controller = "CardReaders";
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {
  }

  getAllCardReaders(): Observable<ICardReader[]> {
    return this.http.get<ICardReader[]>(this.baseUrl + this.controller);
  }

  deleteCardReader(id: number): Observable<ICardReader> {
    return this.http.delete<ICardReader>(`${this.baseUrl}${this.controller}/${id}`);
  }

  CreateCardReader(formData: FormData): Observable<any> {
    debugger
    return this.http.post<any>(this.baseUrl + this.controller, formData)
  }
}
