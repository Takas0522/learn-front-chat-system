import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { FrontChatLibModule } from '../../../front-chat-lib/src/lib/front-chat-lib.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FrontChatLibModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
