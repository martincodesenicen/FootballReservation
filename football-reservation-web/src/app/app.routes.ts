import { Routes } from '@angular/router';

export const routes: Routes = [
  { 
    path: 'auth', 
    loadComponent: () => import('./features/auth/auth').then(m => m.AuthComponent) 
  },
  { 
    path: '', 
    redirectTo: 'auth', 
    pathMatch: 'full' 
  }
];