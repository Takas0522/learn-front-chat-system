import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ChatWindowService } from '../chat-window.service';
import { WebApiService } from 'src/app/services/webapi.service';

@Component({
  selector: 'app-connection',
  templateUrl: './connection.component.html',
  styleUrls: ['./connection.component.scss']
})
export class ConnectionComponent implements OnInit {

  formGroup!: FormGroup;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private webApiService: WebApiService,
    private chatWindowService: ChatWindowService
  ) { }

  ngOnInit(): void {
    this.formGroup = this.fb.group({
      name: ['', Validators.required],
      question: ['', Validators.required]
    });
  }

  submit() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.invalid) {
      return;
    }
    const val = this.formGroup.value;
    const message = `<h1>質問：${val.question}</h1><br>${val.name}さんより`;
    this.webApiService.createMessage(message).subscribe(x => {
      console.log(`messageId:${x}`);
      this.chatWindowService.saveMessageId(x);
      this.router.navigate(['chat', x]);
    });
  }

}
