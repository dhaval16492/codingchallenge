import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StoreModule } from '@ngrx/store';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AppConfirmationDialogComponent } from './shared/app-confirmation-dialog/app-confirmation-dialog.component';
import { employeeReducer } from './reducers/employee.reducer';
import { EffectsModule } from '@ngrx/effects';
import { EmployeeEffects } from './effects/employee.effects';
import { EmployeeService } from './service/employee.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { EmployeeComponent } from './employee/employee.component';
import { MatSelectModule } from '@angular/material/select';
import { deviceReducer } from './reducers/device.reducer';
import { DeviceEffects } from './effects/device.effects';
import { DeviceService } from './service/device.service';
import { DeviceComponent } from './device/device.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { ToasterService } from './service/toaster.service';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AppConfirmationDialogComponent,
    EmployeeComponent,
    DeviceComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MatTableModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: EmployeeComponent, pathMatch: 'full' },
      { path: 'device', component: DeviceComponent },
    ]),
    BrowserAnimationsModule,
    ReactiveFormsModule, // Add this line
    MatFormFieldModule, // Add this line
    MatInputModule, // Add this line
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    StoreModule.forRoot({ employees: employeeReducer, devices: deviceReducer }), // Register your reducers here
    EffectsModule.forRoot([EmployeeEffects, DeviceEffects]), // Re
    MatProgressSpinnerModule,
    MatSelectModule,
    MatPaginatorModule, // <-- Added Paginator Module
    MatSnackBarModule
  ],
  providers: [EmployeeService, DeviceService,ToasterService],
  bootstrap: [AppComponent]
})
export class AppModule { }
