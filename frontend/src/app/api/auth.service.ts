import { HttpInternalService } from './http-internal.service';
import { Injectable } from '@angular/core';
import { NewUser } from '../types/user/NewUser';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUri: string = '/auth';

  constructor(private httpService: HttpInternalService) {}

  public signUp(user: NewUser) {
    return this.httpService.postRequest<NewUser>(
      `${this.baseUri}/signup`,
      user
    );
  }
}
