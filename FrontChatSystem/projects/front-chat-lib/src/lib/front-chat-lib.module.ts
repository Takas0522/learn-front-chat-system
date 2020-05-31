import { NgModule } from '@angular/core';
import { ChatWindowComponent } from './components/chat-window/chat-window.component';
import { FrontChatRoutingModule } from './front-chat-routing-routing.module';
import { ConnectionComponent } from './components/chat-window/connection/connection.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ChatComponent } from './components/chat-window/chat/chat.component';

@NgModule({
  declarations: [
    ChatWindowComponent,
    ConnectionComponent,
    ChatComponent
  ],
  imports: [
    FrontChatRoutingModule,
    MaterialModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  exports: [
    ChatWindowComponent,
  ]
})
export class FrontChatLibModule { }
