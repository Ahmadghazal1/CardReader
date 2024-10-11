import { Injectable } from '@angular/core';
import { ICardReader } from '../Models/CallReader.model';

@Injectable({
  providedIn: 'root'
})
export class ExportService {

  constructor() { }


  exportToCSV(cardReader: ICardReader) {
    const csvRows: string[] = [];


    const headers = Object.keys(cardReader) as Array<keyof ICardReader>;;
    csvRows.push(headers.join(','));


    const values = headers.map(header => {
      const escaped = ('' + cardReader[header]).replace(/"/g, '""');
      return `"${escaped}"`;
    });
    csvRows.push(values.join(','));


    const blob = new Blob([csvRows.join('\n')], { type: 'text/csv;charset=utf-8;' });
    const link = document.createElement('a');
    const url = URL.createObjectURL(blob);

    link.setAttribute('href', url);
    link.setAttribute('download', 'card_reader.csv');
    link.style.visibility = 'hidden';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }

  exportToXML(cardReader: ICardReader) {
    const xmlString = this.convertToXML(cardReader);
    const blob = new Blob([xmlString], { type: 'application/xml' });
    const link = document.createElement('a');
    const url = URL.createObjectURL(blob);

    link.href = url;
    link.download = 'card_reader.xml';
    link.style.visibility = 'hidden';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }

  convertToXML(cardReader: ICardReader): string {
    return `
    <CardReaders>
        <CardReader>
            <name>${cardReader.name}</name>
            <address>${cardReader.address}</address>
            <phone>${cardReader.phone}</phone>
            <dateOfBirth>${cardReader.dateOfBirth}</dateOfBirth>
            <email>${cardReader.email}</email>
            <photo>${cardReader.photo}</photo>
            <gender>${cardReader.gender}</gender>
        </CardReader>
      </CardReaders>
    `.trim();
  }
}
