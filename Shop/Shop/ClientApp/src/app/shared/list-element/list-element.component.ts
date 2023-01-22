import { Component, Input } from '@angular/core';

@Component({
  selector: 'list-element-component',
  templateUrl: './list-element.component.html',
  styleUrls: ['./list-element.component.css'],
})
export default class ListElementComponent {
  @Input()
  public Header: String = 'No header';

  @Input()
  public Content: String = 'Content is not set';
}
