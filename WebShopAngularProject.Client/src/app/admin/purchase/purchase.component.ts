import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { Purchase } from 'src/app/_models/purchase';
import { User } from 'src/app/_models/user';
import { PurchaseService } from 'src/app/_services/purchase.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.css']
})
export class PurchaseComponent implements OnInit {
  purchases: Purchase[] = [];
  users: User[] = [];
  products: Product[] = [];

  purchase: Purchase = { id: 0, purchaseDate: new Date(), userId: 0, productId: 0 }

  constructor(private purchaseService: PurchaseService, private userService: UserService) { }

  ngOnInit(): void {
    this.purchaseService.getAllPurchases()
      .subscribe({
        next: (x) => {
          this.purchases = x;
          console.log(x);
        }
      });

  }

  save(): void {

  }

  edit(purchase: Purchase): void {

  }

  delete(purchase: Purchase): void {

  }

  cancel(): void {

  }
}
