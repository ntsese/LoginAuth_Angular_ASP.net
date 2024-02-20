import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})


export class AuthenticateService {
private baseUrl: string = "https://localhost:44384/api/Users"

constructor(private http: HttpClient) { }

loginUser (loginObj: any){
  return this.http.post<any>(this.baseUrl + "authenticate", loginObj);
}


registerUser (userProfile: any){
  return this.http.post<any>(this.baseUrl + "register", userProfile);
}
}
