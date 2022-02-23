import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { UserComponent } from './admin/user/user.component';
import { FormsModule } from '@angular/forms';
import { PurchaseComponent } from './admin/purchase/purchase.component';

@NgModule({
  declarations: [
    AppComponent,
    FrontpageComponent,
    UserComponent,
    PurchaseComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
