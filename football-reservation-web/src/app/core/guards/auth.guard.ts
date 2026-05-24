import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth';

export const authGuard = (allowedRoles?: string[]): CanActivateFn => {
  return () => {
    const authService = inject(AuthService);
    const router = inject(Router);

    // 1. Verificar si está logueado
    if (!authService.isLoggedIn()) {
      router.navigate(['/auth']);
      return false;
    }

    // 2. Si se especificaron roles permitidos, verificar el rol del usuario
    if (allowedRoles && allowedRoles.length > 0) {
      const user = authService.getUser();
      const userRole = user?.role; // Devolverá "Admin", "Customer", etc.

      if (!userRole || !allowedRoles.includes(userRole)) {
        // Si no tiene el rol necesario, lo redirigimos a la raíz o vista por defecto
        router.navigate(['/auth']);
        return false;
      }
    }

    return true;
  };
};