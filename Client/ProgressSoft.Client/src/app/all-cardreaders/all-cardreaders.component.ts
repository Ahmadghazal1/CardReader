import { Component, OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
import { CardreaderService } from '../Services/cardreader.service';
import { ICardReader } from '../Models/CallReader.model';
import { ButtonModule } from 'primeng/button';
import { ThisReceiver } from '@angular/compiler';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-all-cardreaders',
  standalone: true,
  imports: [TableModule, ButtonModule, ToastModule, CommonModule],
  providers: [MessageService],
  templateUrl: './all-cardreaders.component.html',
  styleUrl: './all-cardreaders.component.css'
})
export class AllCardreadersComponent implements OnInit {
  cardReaders: ICardReader[] = [];
  constructor(private readerService: CardreaderService, private messageService: MessageService, private router: Router) { }
  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.readerService.getAllCardReaders().subscribe((response) => {
      this.cardReaders = response;
    })
  }

  OnClickDelete(event: any) {
    this.readerService.deleteCardReader(event).subscribe(response => {
      debugger
      if (response) {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'The card deleted successfuly' });
        this.loadData();
      }
    })
  }

  createPage() {
    this.router.navigate(["/create"]);
  }
}
