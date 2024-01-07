import { Routes } from '@angular/router';
import { StartComponent } from './pages/start/start/start.component';
import { Component } from '@angular/core';
import { AboutComponent } from './pages/about/about/about.component';
import { LocationComponent } from './pages/location/location/location.component';
import { ProductsComponent } from './pages/products/products/products.component';
import { StoresComponent } from './pages/stores/stores/stores.component';
import { RegisterComponent } from './pages/register/register.component';

export const routes: Routes = [
    {path:"", component:StartComponent},
    {path:"start", component:StartComponent},
    {path:"about", component:AboutComponent},
    {path:"location", component:LocationComponent},
    {path:"products", component:ProductsComponent},
    {path:"stores", component:StoresComponent},
    {path:"register", component:RegisterComponent}
];
