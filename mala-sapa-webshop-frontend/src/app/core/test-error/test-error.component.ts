import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html', 
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
  baseUrl = environment.apiUrl;
  greskeValidacije: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  get404Error() {
      this.http.get(this.baseUrl + 'proizvod/42').subscribe(response => {
        console.log(response);
      }, error => {
        console.log(error);
      });
  }

  get500Error() {
    this.http.get(this.baseUrl + 'bug/servererror').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  get400Error() {
  this.http.get(this.baseUrl + 'bug/badrequest').subscribe(response => {
    console.log(response);
  }, error => {
    console.log(error);
  });
  }

  get400ValidationError() {
    this.http.get(this.baseUrl + 'proizvod/four').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
      this.greskeValidacije = error.errors;  
    });
    }

}
