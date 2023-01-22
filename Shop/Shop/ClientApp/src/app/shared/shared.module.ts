import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import NotFoundComponent from './not-found.component';
import ListElementComponent from './list-element/list-element.component';
import ForbiddenComponent from './forbidden.component';
import BasicNavComponent from './basic-nav/basic-nav.component';
import DeleteListElementComponent from './delete-list-element/delete-list-element.component';

@NgModule({
  imports: [RouterModule, FormsModule, CommonModule],
  exports: [
    NotFoundComponent,
    ListElementComponent,
    ForbiddenComponent,
    BasicNavComponent,
    DeleteListElementComponent,
  ],
  declarations: [
    NotFoundComponent,
    ListElementComponent,
    ForbiddenComponent,
    BasicNavComponent,
    DeleteListElementComponent,
  ],
})
export default class SharedModule {}
