import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {
  baseURL = 'http://192.168.0.105:5000/api/evento';

constructor(private http: HttpClient) { }

getAllEventos(): Observable<Evento[]> {
  return this.http.get<Evento[]>(this.baseURL);
 }

 getEventoByTema(tema: string): Observable<Evento> {
  return this.http.get<Evento>(`${this.baseURL}/getByTema/${tema}`);
}

 getEventoById(id: number): Observable<Evento> {
   return this.http.get<Evento>(`${this.baseURL}/${id}`);
 }

 postEvento(evento: Evento){
   return this.http.post(this.baseURL, evento);
 }

 putEvento(evento: Evento){
   debugger
   return this.http.put(`${this.baseURL}/${evento.id}`, evento);
 }

 deleteEvento(id: number) {
   return this.http.delete(`${this.baseURL}/${id}`);
 }
}



 