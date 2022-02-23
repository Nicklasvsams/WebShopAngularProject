import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from '../_models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = environment.apiUrl + 'product/';

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http: HttpClient) { }

  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }

  getProduct(productId: number): Observable<Product> {
    return this.http.get<Product>(this.apiUrl + productId);
  }

  createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product, this.httpOptions);
  }

  updateProduct(productId: number, product: Product): Observable<Product> {
    return this.http.put<Product>(this.apiUrl + productId, product, this.httpOptions);
  }

  deleteProduct(productId: number): Observable<Product> {
    return this.http.delete<Product>(this.apiUrl + productId, this.httpOptions);
  }
}
