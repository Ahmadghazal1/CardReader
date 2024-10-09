import { Injectable } from '@angular/core';
import jsQR from 'jsqr';
import { NgxCsvParser } from 'ngx-csv-parser';
import { ImportCardReader } from '../Models/CallReader.model';

@Injectable({
  providedIn: 'root'
})
export class UploadFileService {
  qrCodeResult: string = '';
  constructor(private ngxCsvParser: NgxCsvParser,) { }


  readQrCode(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();

      reader.onload = (e: any) => {
        const imgElement = document.createElement('img');
        imgElement.src = e.target.result;

        imgElement.onload = () => {
          const canvas = document.createElement('canvas');
          const context = canvas.getContext('2d');

          canvas.width = imgElement.width;
          canvas.height = imgElement.height;
          context?.drawImage(imgElement, 0, 0);

          const imageData = context?.getImageData(0, 0, canvas.width, canvas.height);
          if (imageData) {
            const code = jsQR(imageData.data, canvas.width, canvas.height);
            if (code) {
              resolve(code.data);
            } else {
              reject('No QR code found');
            }
          } else {
            reject('Image data not available');
          }
        };


      };

      // Read the file as a data URL
      reader.readAsDataURL(file);
    });
  }

  parseCSV(file: File): Promise<ImportCardReader[]> {

    return new Promise((resolve, reject) => {
      this.ngxCsvParser.parse(file, { header: true, delimiter: ',' })
        .subscribe({
          next: (result: any) => {

            resolve(result);
          },
          error: (error) => {
            resolve(error);
          }
        });
    })

  }
}

