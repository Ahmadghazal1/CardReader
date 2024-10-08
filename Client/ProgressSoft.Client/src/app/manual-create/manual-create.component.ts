import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { ToastModule } from 'primeng/toast';
import { CardreaderService } from '../Services/cardreader.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-manual-create',
  standalone: true,
  imports: [CommonModule,
    ReactiveFormsModule,
    InputTextModule,
    DropdownModule,
    CalendarModule,
    ButtonModule,
    ToastModule],
  providers: [MessageService],
  templateUrl: './manual-create.component.html',
  styleUrl: './manual-create.component.css'
})
export class ManualCreateComponent {
  cardReaderForm: FormGroup;
  imageSrc: string = '';
  constructor(private fb: FormBuilder, private cardreaderService: CardreaderService, private router: Router, private messageService: MessageService) {
    this.cardReaderForm = this.fb.group({
      name: ['', Validators.required],
      gender: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      address: ['', Validators.required],
      photo: [null]
    });
  }


  onSubmit() {
    if (this.cardReaderForm.valid) {
      const formData = new FormData();

      formData.append('name', this.cardReaderForm.get('name')?.value);
      formData.append('gender', this.cardReaderForm.get('gender')?.value);
      formData.append('dateOfBirth', this.cardReaderForm.get('dateOfBirth')?.value.toISOString());
      formData.append('email', this.cardReaderForm.get('email')?.value);
      formData.append('phone', this.cardReaderForm.get('phone')?.value);
      formData.append('address', this.cardReaderForm.get('address')?.value);

      const photoFile = this.cardReaderForm.get('photo')?.value;
      if (photoFile instanceof File) {
        formData.append('photo', photoFile);
      }

      this.cardreaderService.CreateCardReader(formData).subscribe((response) => {
        if (response.success) {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Card reader created successfuly' });
          this.resetForm();
        }
        else {
          console.log(response.title);
        }
      }, (error) => {
        console.error('Error occurred:', error);
      });
    }
  }


  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.cardReaderForm.patchValue({
        photo: file
      });
    }
  }

  resetForm() {
    this.cardReaderForm.reset();
  }

  onCancel() {
    this.router.navigate(['/']);
  }
}
