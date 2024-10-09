import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NgxCsvParserModule } from 'ngx-csv-parser';
import { ImportCardReader } from '../Models/CallReader.model';
import { CardreaderService } from '../Services/cardreader.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UploadFileService } from '../Services/upload-file.service';

@Component({
  selector: 'app-import-create',
  standalone: true,
  imports: [NgxCsvParserModule, CommonModule],
  templateUrl: './import-create.component.html',
  styleUrl: './import-create.component.css'
})
export class ImportCreateComponent implements OnInit {
  public Data: ImportCardReader[] = [];

  constructor(private cardReaderService: CardreaderService, private router: Router, private toastr: ToastrService, private fileUploadService: UploadFileService) { }

  ngOnInit(): void {

  }
  uploadFile?: File;
  QrCodeData: string = '';
  handleFile(event: Event) {


    const target = event.target as HTMLInputElement;
    const files = target.files;


    if (files && files.length > 0) {
      const file = files[0];

      const fileExtension = file.name.split('.').pop()?.toLowerCase();

      switch (fileExtension) {
        case 'csv':
          this.fileUploadService.parseCSV(file).then((csvData) => {
            this.Data = csvData;
          });
          break;
        case 'xml':
          ;
          break;
        case 'png':
          this.fileUploadService.readQrCode(file)
            .then((qrCodeData) => {
              this.Data = JSON.parse(qrCodeData);
            })
          break;
        default:
          this.toastr.error("Unsported File");
          break;
      }

      this.uploadFile = file;

    }

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
