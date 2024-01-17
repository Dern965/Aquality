import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ProductoService } from '../../../Services/producto.service';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})

//DESDE AQUI
export class ProductsComponent {
  lista : DTOProducto[] = [];
  constructor(private pService: ProductoService){
    this.pService.GetProducts().subscribe(result => {
      this.lista = result;
      console.log(result);
    });
  }
}
export interface  DTOProducto {
  idProducto: number,
  nombreProducto: string,
  materiales: string,
  fabricacion:string,
  price: number,
  disponibilidad: string,
  idTienda: string
}
//HASTA AQUI, ES DE JURGEN

/*
export class ProductsComponent {
  lista : ModelEspecie[] = [];

  nuevaEspecie : ModelEspecie ={
    especie :'',
    habitat :'',
    conservacion :'',
    adaptabilidad :'',
    amenazas :'',
    contaminacion :'',
    consejos :'',
    poblacion :'',
    idProductos :''
  };

  constructor (private eService : ProductoService){
    this.eService.GetProducts().subscribe(result =>{
      this.lista = result;
      console.log(this.lista);
    });
  }

  enviarFormulario(){
    this.eService.createProduct(this.nuevaEspecie).subscribe(response =>{
      console.log(response);
    });
  }
}

export interface ModelEspecie {
   especie : string,
   habitat :string,
   conservacion :string,
   adaptabilidad :string,
   amenazas :string,
   contaminacion :string,
   consejos :string,
   poblacion :string,
   idProductos :string
}
*/