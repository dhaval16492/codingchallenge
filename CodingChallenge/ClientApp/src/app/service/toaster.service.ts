import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class ToasterService {
  constructor(private snackBar: MatSnackBar) { }

  /**
   * Show a success toast message.
   * @param message - The message to display in the toast.
   * @param duration - The duration for which the toast is visible (in milliseconds). Default is 3000ms.
   */
  showSuccess(message: string, duration: number = 3000): void {
    this.snackBar.open(message, 'Close', {
      duration,
      panelClass: 'success-toast',
    });
  }

  /**
   * Show an error toast message.
   * @param message - The message to display in the toast.
   * @param duration - The duration for which the toast is visible (in milliseconds). Default is 3000ms.
   */
  showError(message: string, duration: number = 3000): void {
    this.snackBar.open(message, 'Close', {
      duration,
      panelClass: 'error-toast',
    });
  }
}
