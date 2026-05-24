import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './auth.html',
  styleUrl: './auth.scss'
})
export class AuthComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  isLoginMode = true;
  errorMessage = '';

  loginForm: FormGroup = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  registerForm: FormGroup = this.fb.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  toggleMode() {
    this.isLoginMode = !this.isLoginMode;
    this.errorMessage = '';
  }

  onSubmit() {
    this.errorMessage = '';
    console.log('--- Iniciando envío de formulario ---');
    console.log('Modo Login:', this.isLoginMode);

    if (this.isLoginMode) {
      if (this.loginForm.invalid) {
        console.warn('Formulario de login inválido:', this.loginForm.errors);
        this.errorMessage = 'Por favor, completa correctamente los campos.';
        return;
      }
      
      console.log('Enviando credenciales:', this.loginForm.value);

      this.authService.login(this.loginForm.value).subscribe({
      next: (userProfile) => {
          console.log('¡Login Exitoso! Perfil recibido del backend:', userProfile);
          
          const role = userProfile.role || (userProfile as any).Role;
          console.log('Rol detectado:', role);

          if (role === 'Admin') {
            console.log('Redirigiendo a panel de Administrador...');
            this.router.navigate(['/admin-dashboard']);
          } else if (role === 'Client') { // <-- Cambiado de 'Customer' a 'Client'
            console.log('Redirigiendo a panel de Cliente...');
            this.router.navigate(['/customer-dashboard']);
          } else {
            console.warn('Rol desconocido recibido:', role);
          }
        },
        error: (err) => {
          console.error('Error atrapado en el componente:', err);
          this.errorMessage = err.error?.message || 'Error al iniciar sesión. Verifica tus credenciales o conexión con el servidor.';
        }
      });
    } else {
      if (this.registerForm.invalid) {
        console.warn('Formulario de registro inválido:', this.registerForm.errors);
        return;
      }

      this.authService.register(this.registerForm.value).subscribe({
        next: (userProfile) => {
          console.log('¡Registro Exitoso! Perfil recibido:', userProfile);
          const role = userProfile.role || (userProfile as any).Role;
          
          if (role === 'Admin') {
            this.router.navigate(['/admin-dashboard']);
          } else {
            this.router.navigate(['/customer-dashboard']);
          }
        },
        error: (err) => {
          console.error('Error en el registro:', err);
          this.errorMessage = err.error?.message || 'Error en el registro. Inténtalo de nuevo.';
        }
      });
    }
  }
}