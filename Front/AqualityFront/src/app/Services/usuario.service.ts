import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  constructor(private http: HttpClient) { }

  public GetUsers() : Observable<any>{
    return this.http.get("https://localhost:7172/Usuarios")
  }

  public createUser(Usuario :any) : Observable <any>{
    return this.http.post('https://localhost:7172/Usuarios', Usuario);
  }
}
