import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticateService } from 'src/app/services/authenticate.service';
import ValidateForm from 'src/app/helpers/validator';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authenticate: AuthenticateService
  ) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }


  onLogin(){
    if (this.loginForm.valid){

      this.authenticate.loginUser(this.loginForm.value).subscribe({
        next:(response)=>{
          alert(response.message);
          this.loginForm.reset();
          this.router.navigate(['home'])
        },
        error: (err)=>{
          alert(err.error.message);
        }
      })
    }
    else {
      ValidateForm.validateAllFormFields(this.loginForm);
      alert("Incorrect username and password");
    }
  }


  // private ValidateAllFields(formGroup: FormGroup){
  //   Object.keys(formGroup.controls).forEach(field =>{
  //     const control = formGroup.get(field);

  //     if (control instanceof FormControl){
  //       control?.markAsDirty({onlySelf: true});
  //     }

  //     else if(control instanceof FormGroup){
  //       this.ValidateAllFields(control)
  //     }
  //   })
  // }

}
