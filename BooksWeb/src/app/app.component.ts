import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { debounceTime, distinctUntilChanged, filter, fromEvent, map } from 'rxjs';
import { Book } from './models/book';
import { BookService } from './services/book.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'BooksWeb';
  books:Book[]=[];
  pageNo=1;
  pageSize=10;
  searchStr="";
  isElastic=false;
  initialLoad=true;
  loading = true;
  totalRecords=0;
  @ViewChild('bookAuthorSearchInput', { static: true }) bookAuthorSearchInput?: ElementRef;
  isSearching: boolean=false;

  
  constructor(private bookService: BookService) { }

    ngOnInit() {
      fromEvent(this.bookAuthorSearchInput?.nativeElement, 'keyup').pipe(

        // get value
        map((event: any) => {
          return event.target.value;
        })
        // if character length greater then 2
       // , filter(res => res.length > 2)
  
        // Time in milliseconds between key events
        , debounceTime(300)
  
        // If previous query is diffent from current   
        , distinctUntilChanged()
  
        // subscription for response
      ).subscribe((text: string) => {
  
        this.isSearching = true;
        this.searchStr = text;
        this.pageNo=1;
        this.LoadBooks();
  
      });

      this.LoadBooks();
    }

    datafrom(){
      this.isElastic = ! this.isElastic;
    }

    LoadBooks(){
      this.bookService.getBooks(this.pageNo,this.pageSize, this.searchStr, this.isElastic).subscribe(data => {
        //this.loading = false;
        this.books = data.books;
        this.totalRecords=data.count;
        this.initialLoad=false;
        this.loading = false;
        this.isSearching =false;
      });
    }

    NavtoPage(prevorNext:any){
      this.loading = true;
      this.pageNo=this.pageNo+prevorNext;
      this.LoadBooks();
    }

  print(): void {
      let printContents, popupWin;
      printContents = document.getElementById('print-section')!.outerHTML;
      popupWin = window.open('', '_blank', 'top=0,left=0,height='+screen.width+',width='+screen.width);
      popupWin!.document.open();
      popupWin!.document.write(`
        <html>
          <head>
            <title>Books & Authors</title>`+
          //    <style type="text/css">
          //   table { 
          //     border-spacing: 0;
          //     border-collapse: collapse;
          //   }
          //   table th, table td {
          //     border:1px solid #000;
          //     padding:0.5em;
          //   } 
          // </style>
          `</head>
      <body onload="window.print();window.close()">${printContents}</body>
        </html>`
      );
      popupWin!.document.close();
  }
}

