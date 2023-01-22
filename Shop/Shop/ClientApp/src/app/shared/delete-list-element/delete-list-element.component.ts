import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'delete-list-element-component',
  templateUrl: './delete-list-element.component.html',
  styleUrls: ['./delete-list-element.component.css'],
})
export default class DeleteListElementComponent {
  @Input()
  public Id: number = 0;

  @Input()
  public Header: string = 'No header';

  @Input()
  public Content: string = 'Content is not set';

  @Output() deleteButtonPushed = new EventEmitter<number>();

  onDeleteButtonPushed() {
    this.deleteButtonPushed.emit(this.Id);
  }
}
