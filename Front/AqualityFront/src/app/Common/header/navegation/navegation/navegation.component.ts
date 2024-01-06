import { Component } from '@angular/core';
import { RouterLink,RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-navegation',
  standalone: true,
  imports: [RouterLink,RouterLinkActive],
  templateUrl: './navegation.component.html',
  styleUrl: './navegation.component.scss'
})
export class NavegationComponent {
  menu=[
    {titulo: "Inicio", url: "/start"},
    {titulo: "Conócenos", url: "/about"},
    {titulo: "Ubicación", url: "/location"},
    {titulo: "Tiendas", url: "/stores"},
    {titulo: "Productos", url: "/products"},
  ];
}

