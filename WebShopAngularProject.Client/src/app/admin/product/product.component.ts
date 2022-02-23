import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})

export class ProductComponent implements OnInit {
  products: Product[] = [];
  product: Product = { id: 0, name: '', price: 0, description: '', stock: 0 };


  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.productService.getAllProducts()
      .subscribe(x => this.products = x);
  }

  save(): void {
    if (this.product.id == 0) {
      if (confirm('Save new product?')) {
        this.productService.createProduct(this.product)
          .subscribe({
            next: (x) => {
              this.products.push(this.product);
              this.product = this.resetProduct();
            },
            error: (err) => {
              console.log(err);
            }
          });
      };
    } else {
      if (confirm('Update product with ID ' + this.product.id + '?')) {
        this.productService.updateProduct(this.product.id, this.product)
          .subscribe({
            next: () => {
              this.product = this.resetProduct();
            },
            error: (err) => {
              console.log(err);
            }
          })
      }
    }
  }

  edit(product: Product): void {
    if (confirm('Do you want to edit this product?')) {
      this.product = product;
    }
  }

  delete(product: Product): void {
    if (confirm('Do you want to delete this product?')) {
      this.productService.deleteProduct(product.id)
        .subscribe({
          next: (x) => {
            this.products = this.products.filter(x => x.id != product.id);
          },
          error: (err) => {
            console.log(err);
          }
        });
    };
  }

  cancel(): void {
    this.product = this.resetProduct();
  }

  resetProduct(): Product {
    var product: Product = { id: 0, name: '', price: 0, description: '', stock: 0 };
    return product;
  }
}
