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
    <cardReaders>
        <CardReader>
            <Id>${cardReader.id}</Id>
            <Name>${cardReader.name}</Name>
            <Address>${cardReader.address}</Address>
            <Phone>${cardReader.phone}</Phone>
            <DateOfBirth>${cardReader.dateOfBirth}</DateOfBirth>
            <Email>${cardReader.email}</Email>
            <Photo>${cardReader.photo}</Photo>
            <Gender>${cardReader.gender}</Gender>
        </CardReader>
      </cardReaders>
    `.trim(); 
  }
}
