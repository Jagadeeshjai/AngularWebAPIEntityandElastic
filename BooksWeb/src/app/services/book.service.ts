import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Books } from '../models/book';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private http: HttpClient) { }

    getBooks(pageNo:number,pageSize:number,search:string,iselastic:boolean) {
     
        return this.http.get<Books>(`${environment.apiUrl}Books/${!iselastic?"GetBooks":"GetESBooks"}?pageNo=${pageNo}&pageSize=${pageSize}&search=${search}`);
    }
}

