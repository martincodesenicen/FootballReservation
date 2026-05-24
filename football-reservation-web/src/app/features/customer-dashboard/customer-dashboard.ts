import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { DatePipe, CurrencyPipe } from '@angular/common';
import { FieldService } from '../../core/services/field';
import { ReservationService } from '../../core/services/reservation';
import { FieldDto } from '../../core/models/field.models';
import { ReservationDto } from '../../core/models/reservation.models';

@Component({
  selector: 'app-customer-dashboard',
  standalone: true,
  imports: [ReactiveFormsModule, DatePipe, CurrencyPipe],
  templateUrl: './customer-dashboard.html',
  styleUrl: './customer-dashboard.scss'
})
export class CustomerDashboardComponent implements OnInit {
  private fb = inject(FormBuilder);
  private fieldService = inject(FieldService);
  private reservationService = inject(ReservationService);

  fields: FieldDto[] = [];
  myBookings: ReservationDto[] = [];
  
  successMessage = '';
  errorMessage = '';

  bookingForm: FormGroup = this.fb.group({
    fieldId: ['', [Validators.required]],
    reservationDate: ['', [Validators.required]],
    durationInHours: [1, [Validators.required, Validators.min(1), Validators.max(4)]]
  });

  ngOnInit(): void {
    this.loadFields();
    this.loadMyBookings();
  }

  loadFields(): void {
    this.fieldService.getAllFields().subscribe({
      next: (data) => this.fields = data.filter(f => f.isActive),
      error: () => this.errorMessage = 'No se pudieron cargar las canchas.'
    });
  }

  loadMyBookings(): void {
    this.reservationService.getMyBookings().subscribe({
      next: (data) => this.myBookings = data,
      error: () => this.errorMessage = 'No se pudo cargar tu historial de reservas.'
    });
  }

  onSubmit(): void {
    if (this.bookingForm.invalid) return;

    this.successMessage = '';
    this.errorMessage = '';

    // Convertimos la fecha ingresada a formato ISO string requerido por .NET DateTime
    const formValue = this.bookingForm.value;
    const dto = {
      fieldId: Number(formValue.fieldId),
      reservationDate: new Date(formValue.reservationDate).toISOString(),
      durationInHours: Number(formValue.durationInHours)
    };

    this.reservationService.createReservation(dto).subscribe({
      next: (res) => {
        this.successMessage = `¡Reserva confirmada con éxito en ${res.fieldName}!`;
        this.bookingForm.reset({ durationInHours: 1, fieldId: '' });
        this.loadMyBookings(); // Recargamos la lista automáticamente
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'La cancha no está disponible en el horario seleccionado.';
      }
    });
  }
}