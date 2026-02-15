import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/Models/Product';
import { Pagination } from './shared/Models/Pagination';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  baseUrl='https://localhost:5001/api'
  private http=inject(HttpClient);//or use constructor(private http:HttpClient){ }
   title = 'Skinet';
   products:Product[]=[];
   ngOnInit(): void {
            this.http.get<Pagination<Product>>(this.baseUrl +'/products').subscribe({
              next:respose=>this.products=respose.data,
              error:error=>console.log(error),
              complete:()=>console.log('complete')
            })
  }



}
