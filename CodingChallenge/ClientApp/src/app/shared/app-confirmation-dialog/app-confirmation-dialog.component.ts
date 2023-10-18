import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-app-confirmation-dialog',
  templateUrl: './app-confirmation-dialog.component.html',
  styleUrls: ['./app-confirmation-dialog.component.css']
})
export class AppConfirmationDialogComponent {
  constructor(public dialogRef: MatDialogRef<AppConfirmationDialogComponent>) { }

  /**
   * Confirms the action and closes the dialog with a result of `true`.
   */
  confirm(): void {
    this.dialogRef.close(true); // Return true to indicate confirmation
  }

  /**
   * Cancels the action and closes the dialog with a result of `false`.
   */
  cancel(): void {
    this.dialogRef.close(false); // Return false to indicate cancellation
  }
}
