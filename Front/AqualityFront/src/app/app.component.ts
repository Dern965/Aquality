import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './Common/header/header.component';
import { FooterComponent } from './Common/footer/footer.component';
import { HttpClientModule } from '@angular/common/http';
import { UsuarioService } from './Services/usuario.service';
import { ProductoService } from './Services/producto.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HeaderComponent, FooterComponent, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers: [UsuarioService, ProductoService]
})
export class AppComponent {
  title = 'AqualityFront';
}
