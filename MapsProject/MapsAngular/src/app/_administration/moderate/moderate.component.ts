import { Component, OnInit, ViewChild, HostListener, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { merge } from 'rxjs/observable/merge';
import { of as observableOf } from 'rxjs/observable/of';
import { catchError } from 'rxjs/operators/catchError';
import { map } from 'rxjs/operators/map';
import { startWith } from 'rxjs/operators/startWith';
import { switchMap } from 'rxjs/operators/switchMap';
import { Observable } from 'rxjs/Rx';

import { AuthenticationService } from '../_services/authentication.service';
import { ModerateDataService } from '../_services/moderate-data.service';

import { ComponentCanDeactivate } from '../_guards/leave-moderate-page.guard';


@Component({
  selector: 'app-moderate',
  templateUrl: './moderate.component.html',
  styleUrls: ['./moderate.component.css'],
  providers: [AuthenticationService, ModerateDataService]
})
export class ModerateComponent implements OnInit, ComponentCanDeactivate {
  displayedColumns = ['Id', 'Name', 'Tags', 'Latitude', 'Longitude', 'Status', 'Delete', 'Approve'];
  dataSource = new MatTableDataSource();

  resultsLength = 0;
  isLoadingResults = true;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  leaved = true;

  @HostListener('window:beforeunload')
  canDeactivate(): boolean | Observable<boolean> {
    if (this.leaved) {
      this.authenticationService.logout();
      return true;
    }
    return false;
  }

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private dataService: ModerateDataService,
    private authenticationService: AuthenticationService
  ) {
  }

  deleteObject(id) {
    this.dataService.deleteData(id).subscribe(data => {
      this.loadData();
    });
  }

  approvedObject(id) {
    this.dataService.approvedObject(id).subscribe(data => {
      this.loadData();
    });
  }

  openDialog(id, action): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      height: '200px',
      data: { action: action }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        if (action === 'delete') {
          this.deleteObject(id);
        }
        if (action === 'approve') {
          this.approvedObject(id);
        }
      }
    });
  }

  ngOnInit() {
    this.loadData();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
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

@Component({
  selector: 'confirm-dialog',
  templateUrl: 'confirm-dialog.html',
})
export class ConfirmDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
