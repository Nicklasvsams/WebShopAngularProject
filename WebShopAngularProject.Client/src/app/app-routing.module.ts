import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from './admin/category/category.component';
import { ProductComponent } from './admin/product/product.component';
import { PurchaseComponent } from './admin/purchase/purchase.component';
import { UserComponent } from './admin/user/user.component';
import { FrontpageComponent } from './frontpage/frontpage.component';

const routes: Routes = [
  { path: '', component: FrontpageComponent },
  { path: 'admin/user', component: UserComponent },
  { path: 'admin/purchase', component: PurchaseComponent },
  { path: 'admin/product', component: ProductComponent },
  { path: 'admin/category', component: CategoryComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
