export interface ReservationDto {
  id: number;
  fieldId: number;
  fieldName: string;
  reservationDate: string; // ISO string o formato fecha
  durationInHours: number;
  totalPrice: number;
  status: string;
}

export interface CreateReservationDto {
  fieldId: number;
  reservationDate: string; // Enviamos en formato ISO (ej: "2026-05-24T19:00:00")
  durationInHours: number;
}