import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html'
})
export class SearchComponent implements OnInit {

  form: FormGroup;

  constructor(
    public fb: FormBuilder) {
    this.form = this.fb.group({
      description: [''],
      type: ['JPG'],
      size: [10]
    })
  }

  ngOnInit() {
  }

  submitUser() {
    console.log(this.form)
  }
}
