import { Component } from '@angular/core';
import { DataComponent } from '../../../dataComponent';

@Component({
  templateUrl: './column.component.html',
})
export class ColumnComponent implements DataComponent {
  constructor() {}
  data: any;
}
