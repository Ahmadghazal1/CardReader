import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';


@Component({
  selector: 'app-create-cardr-reader',
  standalone: true,
  imports: [
    ButtonModule,

  ],
  templateUrl: './create-cardr-reader.component.html',
  providers: [],
  styleUrl: './create-cardr-reader.component.css'
})
export class CreateCardrReaderComponent {
  constructor(private router: Router) { }

  ManualPage() {
    this.router.navigate(["/manual-create"])
  }
}
