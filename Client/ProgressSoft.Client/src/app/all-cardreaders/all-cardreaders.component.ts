import { Component, OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
import { CardreaderService } from '../Services/cardreader.service';
import { ICardReader } from '../Models/CallReader.model';
import { ButtonModule } from 'primeng/button';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-all-cardreaders',
  standalone: true,
  imports: [TableModule, ButtonModule, CommonModule],
  providers: [],
  templateUrl: './all-cardreaders.component.html',
  styleUrl: './all-cardreaders.component.css'
})
export class AllCardreadersComponent implements OnInit {
  cardReaders: ICardReader[] = [];
  constructor(private readerService: CardreaderService,
    private toasterService: ToastrService,
    private router: Router,
  ) { }
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
        this.toasterService.success("Card Reader Deleted Successfuly");
        this.loadData();
      }
    })
  }

  createPage() {
    this.router.navigate(["/create"]);
  }



}
