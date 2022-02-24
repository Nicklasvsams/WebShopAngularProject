import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {
  constructor(private userService: UserService) { }

  users: User[] = [];

  ngOnInit(): void {
    this.userService.getAllUsers()
      .subscribe(x => this.users = x);
  }
}
