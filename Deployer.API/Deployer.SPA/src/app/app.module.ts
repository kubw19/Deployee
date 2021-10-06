import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID, InjectionToken } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';

import { environment } from 'src/environments/environment';
import { DashboardComponent } from './panel/dashboard/dashboard.component'

import { CustomDatePipe } from './custom-date.pipe';
import { CustomHoursPipe } from './custom-hours.pipe';
import { PanelLayoutComponent } from './layouts/panel-layout/panel-layout.component';
import localePL from '@angular/common/locales/pl';

import { DataTablesModule } from 'angular-datatables';
import { registerLocaleData } from '@angular/common';

import { DateInterceptor } from './date.interceptor';
import { TimeSpanPipe } from './timespan.pipe'
import { DictionaryPipe } from './pipes/dictionary.pipe'


import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { FullCalendarModule } from '@fullcalendar/angular';
import { AuthInterceptor } from './auth/auth.interceptor';
import { ListTargetsComponent } from './panel/targets/list-targets/list-targets.component';
import { AddTargetComponent } from './panel/targets/add-target/add-target.component';
import { ListArtifactsComponent } from './panel/artifacts/list-artifacts/list-artifacts.component';
import { ModalComponent } from './utils/modal/modal.component';
import { StepsComponent } from './panel/steps/steps.component';




registerLocaleData(localePL, 'pl');

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    DashboardComponent,
    CustomDatePipe,
    CustomHoursPipe,
    TimeSpanPipe,
    DictionaryPipe,
    PanelLayoutComponent,
    ListTargetsComponent,
    AddTargetComponent,
    ListTargetsComponent,
    ListArtifactsComponent,
    ModalComponent,
    StepsComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    SweetAlert2Module.forRoot(),
    FullCalendarModule,
    RouterModule.forRoot([


      {
        path: '', redirectTo: "panel", pathMatch: "full"
      },

      // {
      //   path: '', component: UserLayoutComponent, pathMatch: 'full', canActivate: [AuthGuard], children: [
      //     { path: '', component: DashboardComponent, pathMatch: "full" }
      //   ]
      // },
      {
        path: 'panel', component: PanelLayoutComponent, children: [
          { path: '', redirectTo: "dashboard", pathMatch: "full" },
          { path: "dashboard", component: DashboardComponent },
          { path: "targets", component: ListTargetsComponent },
          { path: "targets/new", component: AddTargetComponent },
          { path: "artifacts", component: ListArtifactsComponent },
          { path: "steps", component: StepsComponent }
        ]
      },


    ]), FormsModule,
    ReactiveFormsModule,
    DataTablesModule
  ],
  providers: [
    {
      provide: LOCALE_ID, useValue: "pl"
    }, {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }, {
      provide: HTTP_INTERCEPTORS,
      useClass: DateInterceptor,
      multi: true
    }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

