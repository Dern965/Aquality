import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import internal from 'stream';
import { UsuarioService } from '../../Services/usuario.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  lista : ModelUsuario[] = [];

  nuevoUsuario : ModelUsuario = {
    username: '',
    password: '',
    email: '',
    phone: '',
    first_Name: '',
    last_Name: '',
    fnac : '',
    idHistorial: 0
  };
  constructor (private uService : UsuarioService){
    this.uService.GetUsers().subscribe(result =>{
      this.lista = result;
      console.log(this.lista);
    });
  }

  enviarFormulario(){
    this.uService.createUser(this.nuevoUsuario).subscribe(response =>{
      console.log(response);
    });
  }
}

export interface ModelUsuario {
  username: string,
  password: string,
  email: string,
  phone: string,
  first_Name: string,
  last_Name: string,
  fnac : string,
  idHistorial: number
}