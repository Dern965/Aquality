import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  constructor(private http: HttpClient) { }

  public GetProducts() : Observable<any>{
    return this.http.get("https://localhost:7172/Especies")
  }

  public createProduct(Especie :any) : Observable <any>{
    return this.http.post('https://localhost:7172/Especies', Especie);
  }
}
