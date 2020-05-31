import { Component, OnInit } from '@angular/core';
import { InputFormModel } from './models/input-form.model';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { FrontChatService } from '../../../service/front-chat.service';
import { Router } from '@angular/router';

@Component({
  selector: 'fcl-connection',
  templateUrl: './connection.component.html',
  styleUrls: ['./connection.component.scss']
})
export class ConnectionComponent implements OnInit {

  formGroup!: FormGroup;
  constructor(
    private fb: FormBuilder,
    private chatService: FrontChatService,
    private router: Router
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
    const message = `${val.name}さんから問い合わせ<br>質問内容：${val.question}`;
    this.chatService.createMessage(message).subscribe(x => {
      console.log(`messageId:${x}`);
      this.chatService.saveMessageId(x);
      this.router.navigate(['chat', x]);
    });
  }

}
