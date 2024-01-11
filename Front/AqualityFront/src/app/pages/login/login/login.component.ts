import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../../../Services/usuario.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  public username!:string;
  public password!:string;
  public imagen!:string;

  constructor(private uService: LoginService, private ruteador:Router ){

  }
  
  public Iniciar(){
    this.uService.LoginUser(this.username,this.password).subscribe(resultado=>{
      //this.uService.LoginUser(["productos"]);
      this.imagen=resultado.image;
    }, error =>{
      console.warn(error);
    });
  }
}

export interface  DTOUsuario {
  id: number,
  username: string,
  email: string,
  firstname: string,
  lastname: string,
  gender: string,
  image: string,
  token: string,
}