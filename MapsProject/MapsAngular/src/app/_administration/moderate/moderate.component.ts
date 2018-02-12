import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

import { merge } from 'rxjs/observable/merge';
import { of as observableOf } from 'rxjs/observable/of';
import { catchError } from 'rxjs/operators/catchError';
import { map } from 'rxjs/operators/map';
import { startWith } from 'rxjs/operators/startWith';
import { switchMap } from 'rxjs/operators/switchMap';

import { AuthenticationService } from '../_services/authentication.service';
import { ModerateDataService } from '../_services/moderate-data.service';


@Component({
  selector: 'app-moderate',
  templateUrl: './moderate.component.html',
  styleUrls: ['./moderate.component.css'],
  providers: [AuthenticationService, ModerateDataService]
})
export class ModerateComponent implements OnInit {
  displayedColumns = ['Id', 'Name', 'Tags', 'Latitude', 'Longitude', 'Status', 'Delete', 'Approve'];
  dataSource = new MatTableDataSource();

  resultsLength = 0;
  isLoadingResults = true;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private authenticationService: AuthenticationService,
    private dataService: ModerateDataService,
    private router: Router) {
  }

  deleteObject(id) {
    this.dataService.deleteData(id).subscribe(data => {
      console.log('Delete');
      this.loadData();
    });
  }

  approvedObject(id) {
    this.dataService.approvedObject(id).subscribe(data => {
      console.log('Approved');
      this.loadData();
    });
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
      startWith({}),
      switchMap(() => {
        this.isLoadingResults = true;
        return this.dataService.getData();
      }),
      map(data => {
        this.isLoadingResults = false;
        this.resultsLength = data.length;
        this.dataSource.sort = this.sort;
        return data;
      }),
      catchError(() => {
        this.isLoadingResults = false;
        return observableOf([]);
      })
      ).subscribe(data => this.dataSource.data = data);
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['']);
  }
}
