import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { FieldService } from '../../core/services/field';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.scss'
})
export class AdminDashboardComponent {
  private fb = inject(FormBuilder);
  private fieldService = inject(FieldService);
  private authService = inject(AuthService);
  private router = inject(Router);

  successMessage = '';
  errorMessage = '';

  fieldForm: FormGroup = this.fb.group({
    name: ['', [Validators.required]],
    capacity: ['', [Validators.required]], // Ej: "Fútbol 5", "Fútbol 7"
    pricePerHour: [0, [Validators.required, Validators.min(1)]]
  });

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/auth']);
  }

  onSubmit() {
    if (this.fieldForm.invalid) return;

    this.fieldService.createField(this.fieldForm.value).subscribe({
      next: (newField) => {
        this.successMessage = `La cancha "${newField.name}" fue creada con éxito.`;
        this.errorMessage = '';
        this.fieldForm.reset({ pricePerHour: 0 });
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Error al intentar crear la cancha.';
        this.successMessage = '';
      }
    });
  }
}