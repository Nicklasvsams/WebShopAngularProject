import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Purchase } from '../_models/purchase';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {

  private apiUrl = environment.apiUrl + 'purchase/';

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http: HttpClient) { }

  getAllPurchases(): Observable<Purchase[]> {
    return this.http.get<Purchase[]>(this.apiUrl);
  }

  getPurchase(purchaseId: number): Observable<Purchase> {
    return this.http.get<Purchase>(this.apiUrl + purchaseId);
  }

  createPurchase(purchase: Purchase): Observable<Purchase> {
    return this.http.post<Purchase>(this.apiUrl, purchase, this.httpOptions);
  }

  updatePurchase(purchaseId: number, purchase: Purchase): Observable<Purchase> {
    return this.http.put<Purchase>(this.apiUrl + purchaseId, purchase, this.httpOptions);
  }

  deletePurchase(purchaseId: number): Observable<Purchase> {
    return this.http.delete<Purchase>(this.apiUrl + purchaseId, this.httpOptions);
  }
}
