import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/User';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  users: User[] = [];
  user: User = { id: 0, username: '', password: '', email: '', userType: '' }

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getAllUsers()
      .subscribe(x => this.users = x);
  }

  edit(user: User): void {
    this.user = user;
  }

  delete(user: User): void {
    if (confirm('Are you sure you want to delete this entry?')) {
      this.userService.deleteUser(user.id)
        .subscribe(() => {
          this.users = this.users.filter(x => x.id != user.id)
        })
    }
  }

  save(): void {
    console.log(this.user);
    if (this.user.id == 0) {
      this.userService.addUser(this.user)
        .subscribe({
          next: (x) => {
            this.users.push(x);
            this.user = this.resetUser();
          },
          error: (err) => {
            console.log(err.error);
          }
        });
    } else {
      this.userService.updateUser(this.user.id, this.user)
        .subscribe({
          error: (err) => {
            console.log(err.error);
          },
          complete: () => {
            this.user = this.resetUser();
          }
        })
    }
  }

  cancel(): void {
    this.user = this.resetUser();
  }

  resetUser(): User {
    var user: User = { id: 0, username: '', password: '', email: '', userType: '' }
    return user;
  }
}
