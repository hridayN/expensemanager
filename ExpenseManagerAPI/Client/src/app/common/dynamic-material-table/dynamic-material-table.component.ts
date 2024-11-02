import { Component } from '@angular/core';

@Component({
  selector: 'app-dynamic-material-table',
  templateUrl: './dynamic-material-table.component.html',
  styleUrl: './dynamic-material-table.component.css'
})
export class DynamicMaterialTableComponent {
  dynamicColumns = [{
    columnDef: 'id',
    header: 'ID',
    cell: (row: { id: any; }) => row.id,
  }, {
    columnDef: 'name',
    header: 'Name',
    cell: (row: { name: any; }) => row.name,
  // }, {
  //   columnDef: 'graceperiod',
  //   header: 'Grace Period',
  //   cell: (row: { graceperiod: any; }) => row.color,
  // 
  }];
}
