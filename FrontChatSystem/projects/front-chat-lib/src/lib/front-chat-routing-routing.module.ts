import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChatWindowComponent } from './components/chat-window/chat-window.component';
import { ConnectionComponent } from './components/chat-window/connection/connection.component';
import { ChatComponent } from './components/chat-window/chat/chat.component';


const routes: Routes = [
  { path: 'connection', component: ConnectionComponent },
  { path: 'chat/:id', component: ChatComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class FrontChatRoutingModule { }
