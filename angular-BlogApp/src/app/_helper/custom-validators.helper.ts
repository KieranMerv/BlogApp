import { AbstractControl, ValidatorFn, FormControl, FormGroup, ValidationErrors } from '@angular/forms';

export class CustomValidators {
  constructor() { }

  static passwordMatchConfirm(controlName: string, matchingControlName: string): ValidatorFn | null {
    return (formGroup: AbstractControl): ValidationErrors | null => {
      const control = formGroup.get(controlName);
      const matchingControl = formGroup.get(matchingControlName);

      if (control !== null && matchingControl !== null) {
        if (control.value !== matchingControl.value) {
          if (matchingControl.errors === null) {
            matchingControl.setErrors({ passwordMatchConfirm: true });
          } else {
            matchingControl.errors['passwordMatchConfirm'] = true;
          }
          return { formGroupPasswordMatchConfirm: true };
        } else {
          return null;
        }
      }

      return null;
    };
  }
}