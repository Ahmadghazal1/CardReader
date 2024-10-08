import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AllCardreadersComponent } from "./all-cardreaders/all-cardreaders.component";
import { HeaderComponent } from "./header/header.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AllCardreadersComponent, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ProgressSoft.Client';
}
