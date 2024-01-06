import { Component } from '@angular/core';
import { NavegationComponent } from './navegation/navegation/navegation.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [NavegationComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
}
