import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { 
    path: 'auth', 
    loadComponent: () => import('./features/auth/auth').then(m => m.AuthComponent) 
  },
  { 
    path: 'customer-dashboard', 
    loadComponent: () => import('./features/customer-dashboard/customer-dashboard').then(m => m.CustomerDashboard),
    canActivate: [authGuard(['Customer'])] // Ajusta según el string exacto de tus clientes (ej: Customer o User)
  },
  { 
    path: 'admin-dashboard', 
    loadComponent: () => import('./features/admin-dashboard/admin-dashboard').then(m => m.AdminDashboardComponent),
    canActivate: [authGuard(['Admin'])] // Protegido estrictamente para el rol "Admin"
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