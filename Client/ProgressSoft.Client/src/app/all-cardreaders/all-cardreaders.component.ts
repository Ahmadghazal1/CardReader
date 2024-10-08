import { Component, OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
import { CardreaderService } from '../Services/cardreader.service';
import { ICardReader } from '../Models/CallReader.model';
import { ButtonModule } from 'primeng/button';
import { ThisReceiver } from '@angular/compiler';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
@Component({
  selector: 'app-all-cardreaders',
  standalone: true,
  imports: [TableModule, ButtonModule, ToastModule, CommonModule, ConfirmDialogModule],
  providers: [MessageService, ConfirmationService],
  templateUrl: './all-cardreaders.component.html',
  styleUrl: './all-cardreaders.component.css'
})
export class AllCardreadersComponent implements OnInit {
  cardReaders: ICardReader[] = [];
  constructor(private readerService: CardreaderService,
    private messageService: MessageService,
    private router: Router,
    private confirmationService: ConfirmationService) { }
  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.readerService.getAllCardReaders().subscribe((response) => {
      this.cardReaders = response;
    })
  }

  deleteCardReader(event: any) {
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

  confirm1(id: number) {
    this.confirmationService.confirm({
      message: 'Are you sure that you want to delete card reader?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      acceptIcon: "none",
      rejectIcon: "none",
      rejectButtonStyleClass: "p-button-text",
      accept: () => {
        this.deleteCardReader(id);
      },
      reject: () => {
      }
    });
  }
}
