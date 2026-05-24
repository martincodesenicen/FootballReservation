import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { 
    path: 'auth', 
    loadComponent: () => import('./features/auth/auth').then(m => m.AuthComponent) 
  },
  { 
    path: 'customer-dashboard', 
    loadComponent: () => import('./features/customer-dashboard/customer-dashboard').then(m => m.CustomerDashboardComponent),
    canActivate: [authGuard(['Client'])] // <-- Cambiado de 'Customer' a 'Client'
  },
  { 
    path: 'admin-dashboard', 
    loadComponent: () => import('./features/admin-dashboard/admin-dashboard').then(m => m.AdminDashboardComponent),
    canActivate: [authGuard(['Admin'])] 
  },
  { 
    path: '', 
    redirectTo: 'auth', 
    pathMatch: 'full' 
  },
  {
    path: '**',
    redirectTo: 'auth'
  }
];