import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ProductoService } from '../../../Services/producto.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
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
