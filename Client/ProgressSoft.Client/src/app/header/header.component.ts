import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MenubarModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  items!: MenuItem[];
  ngOnInit() {
    this.items = [
      {
        label: 'Home',
        icon: 'pi pi-home',
        routerLink: ['/'] // or you can use a click event
      },
      {
        label: 'Create',
        icon: 'pi pi-plus',
        routerLink: ['/create'] // you can change this to the route for your "Create" page
      }
    ];
  }
}
