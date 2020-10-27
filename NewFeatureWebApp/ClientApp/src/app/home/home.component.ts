import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from "@angular/forms";
import { FileUploadService } from "../file-upload.service";
import { HttpEvent, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  form: FormGroup;
  progress: number = 0;

  constructor(
    public fb: FormBuilder,
    public fileUploadService: FileUploadService
  ) {
    this.form = this.fb.group({
      description: [''],
      imagefile: [null]
    })
  }

  ngOnInit() { }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files[0];
    this.form.patchValue({
      imagefile: file
    });
    this.form.get('imagefile').updateValueAndValidity()
  }

  submitUser() {
    this.fileUploadService.addAttachment(
      this.form.value.description,
      this.form.value.imagefile
    ).subscribe((event: HttpEvent<any>) => {
      switch (event.type) {
        case HttpEventType.Sent:
          console.log('Request has been made!');
          break;
        case HttpEventType.ResponseHeader:
          console.log('Response header has been received!');
          break;
        case HttpEventType.UploadProgress:
          this.progress = Math.round(event.loaded / event.total * 100);
          console.log(`Uploaded! ${this.progress}%`);
          break;
        case HttpEventType.Response:
          console.log('File successfully created!', event.body);
          setTimeout(() => {
            this.progress = 0;
          }, 1500);
      }
    })
  }

}



