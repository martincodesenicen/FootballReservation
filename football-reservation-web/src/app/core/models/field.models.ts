export interface FieldDto {
  id: number;
  name: string;
  capacity: string;
  pricePerHour: number;
  isActive: boolean;
}

export interface CreateFieldDto {
  name: string;
  capacity: string;
  pricePerHour: number;
}