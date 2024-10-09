import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NgxCsvParserModule } from 'ngx-csv-parser';
import { NgxCsvParser } from 'ngx-csv-parser';
import { ImportCardReader } from '../Models/CallReader.model';
import { CardreaderService } from '../Services/cardreader.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-import-create',
  standalone: true,
  imports: [NgxCsvParserModule, CommonModule],
  templateUrl: './import-create.component.html',
  styleUrl: './import-create.component.css'
})
export class ImportCreateComponent implements OnInit {
  public csvData: ImportCardReader[] = []; // Store parsed CSV data

  constructor(private ngxCsvParser: NgxCsvParser, private cardReaderService: CardreaderService, private router: Router) { }

  ngOnInit(): void {

  }
  uploadFile?: File;
  handleFile(event: Event) {
    const target = event.target as HTMLInputElement;
    const files = target.files;
    if (files && files.length > 0) {
      const file = files[0];
      this.uploadFile = file;
      this.parseCSV(file);
    }

  }
  parseCSV(file: File) {
    this.ngxCsvParser.parse(file, { header: true, delimiter: ',' })
      .subscribe({
        next: (result: any) => {

          this.csvData = result;
        },
        error: (error) => {
          console.error('Error parsing CSV:', error);
        }
      });
  }

  onSubmit() {
    const formData = new FormData();
    if (this.uploadFile instanceof File) {
      formData.append('file', this.uploadFile);
    }

    this.cardReaderService.ImportCardReader(formData).subscribe(response => {
      console.log(response);
      this.router.navigate(["/"]);
    })
  }


}
