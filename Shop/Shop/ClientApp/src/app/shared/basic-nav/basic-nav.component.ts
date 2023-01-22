import { Component, Output, EventEmitter } from '@angular/core';
import UserServise from '../../core/services/user.service';

@Component({
  selector: 'basic-nav-component',
  templateUrl: './basic-nav.component.html',
})
export default class BasicNavComponent {
  public searchLine: string | null;

  @Output() searchLineIsEmpty = new EventEmitter<void>();

  @Output() searchButtonPuhsed = new EventEmitter<string>();

  constructor(private userService: UserServise) {}

  LogOut() {
    this.userService.logOut();
  }

  searchTests() {
    if (this.searchLine !== '' && this.searchLine !== undefined) {
      this.searchButtonPuhsed.emit(this.searchLine.trim());
    } else {
      console.log('searching line is empty');
    }
  }

  checkEmptiness() {
    if (this.searchLine === '') {
      this.searchLineIsEmpty.emit();
    }
  }
}
