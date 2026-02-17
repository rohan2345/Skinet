import {inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Product } from '../../shared/Models/Product';
import { Pagination } from '../../shared/Models/Pagination';
import { ShopParams } from '../../shared/Models/shopParams';


@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl='https://localhost:5001/api/'//service is initialized when our application starts and it's a singleton
  //so any properties that we store inside our service are going to be available for the lifetime of our app.
  private http=inject(HttpClient);//or use constructor(private http:HttpClient){ } //ngdocheck ---
  // // content prjoection must  visit ngaftercontent,ngaftercontentcheck
  types:string[]=[];
  brands:string[]=[];
  getProducts(shopParams:ShopParams){
    let params=new HttpParams();
    if(shopParams.brands.length>0){
      params=params.append('brands',shopParams.brands.join(','));
    }
    if(shopParams.types.length>0){
      params=params.append('types',shopParams.types.join(','));
    }
    if(shopParams.sort){
      params=params.append('sort',shopParams.sort);
    }
    if(shopParams.search){
      params=params.append('search',shopParams.search);
    }
    params=params.append('pageSize',shopParams.pageSize);
     params=params.append('pageIndex',shopParams.pageNumber);
    return this.http.get<Pagination<Product>>(this.baseUrl +'products',{params});
  }
  getBrands(){
    if(this.brands.length>0) return;
    return this.http.get<string[]>(this.baseUrl+'products/brands').subscribe({
      next:response=>this.brands=response
    })
  }
  getTypes(){
    if(this.types.length>0) return;
    return this.http.get<string[]>(this.baseUrl +'products/types').subscribe({
      next:response=>this.types=response
    })
  }


}
