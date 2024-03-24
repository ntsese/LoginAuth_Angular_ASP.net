import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticateService } from 'src/app/services/authenticate.service';
import ValidateForm from 'src/app/helpers/validator';


@Component({
  selector: 'app-signUp',
  templateUrl: './signUp.component.html',
  styleUrls: ['./signUp.component.scss']
})
export class SignUpComponent implements OnInit {

  signUpForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authenticate: AuthenticateService ) { }

  ngOnInit() {
    this.signUpForm = this.fb.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  onSignUp(){
    if(this.signUpForm.valid){
      this.authenticate.registerUser(this.signUpForm.value).subscribe({
        next:(response)=>{
          alert(response.message);
          this.signUpForm.reset();
          this.router.navigate(['login']);
        },
        error: (err)=>{
          alert(err.error.message);
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.signUpForm);
      alert("form not complete, please fill the form");
    }
  }

}
