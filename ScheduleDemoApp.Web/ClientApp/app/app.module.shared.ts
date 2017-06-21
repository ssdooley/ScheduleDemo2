import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ToastrService } from './services/toastr.service';

import { AppComponent } from './components/app/app.component'
import { HomeComponent } from './components/home/home.component';

export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        HomeComponent
    ],
    providers: [
        ToastrService
    ],
    imports: [
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
};
