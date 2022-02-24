import { Component, OnInit } from '@angular/core';
import { Purchase } from 'src/app/_models/purchase';
import { PurchaseService } from 'src/app/_services/purchase.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.css']
})
export class PurchaseComponent implements OnInit {

  purchase: Purchase = { id: 0, purchaseDate: new Date(), userId: 0, productId: 0 }
  purchases: Purchase[] = [];

  constructor(private purchaseService: PurchaseService, private userService: UserService) { }

  ngOnInit(): void {
    this.purchaseService.getAllPurchases()
      .subscribe({
        next: (x) => {
          this.purchases = x;
        },
        error: (err) => {
          console.log(err);
        }
      });

  }

  save(): void {
    if (this.purchase.id == 0) {
      if (confirm('Save new purchase?')) {
        this.purchaseService.createPurchase(this.purchase)
          .subscribe({
            next: (x) => {
              this.purchases.push(x);
              this.purchase = this.resetPurchase();
            },
            error: (err) => {
              console.log(err);
            }
          });
      }
    } else {
      if (confirm('Update purchase with ID ' + this.purchase.id + '?')) {
        this.purchaseService.updatePurchase(this.purchase.id, this.purchase)
          .subscribe({
            next: () => {
              this.purchase = this.resetPurchase();
            },
            error: (err) => {
              console.log(err);
            }
          });
      };
    };
  }

  edit(purchase: Purchase): void {
    if (confirm('Do you want to edit this purchase?')) {
      this.purchase = purchase;
    };
  };

  delete(purchase: Purchase): void {
    if (confirm('Do you want to delete this purchase?')) {
      this.purchaseService.deletePurchase(purchase.id)
        .subscribe({
          next: (x) => {
            this.purchases = this.purchases.filter(x => x.id != purchase.id);
          },
          error: (err) => {
            console.log(err);
          }
        });
    };
  }

  cancel(): void {
    this.purchase = this.resetPurchase();
  };

  resetPurchase(): Purchase {
    var purchase: Purchase = { id: 0, purchaseDate: new Date(), userId: 0, productId: 0 };
    return purchase;
  };
}
