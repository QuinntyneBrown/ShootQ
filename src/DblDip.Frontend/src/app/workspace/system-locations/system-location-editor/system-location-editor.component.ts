import { Component, forwardRef, Input, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormArray, FormControl, FormGroup, NG_VALIDATORS, NG_VALUE_ACCESSOR, ValidationErrors, Validator, Validators } from '@angular/forms';

@Component({
  selector: 'app-system-location-editor',
  templateUrl: './system-location-editor.component.html',
  styleUrls: ['./system-location-editor.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SystemLocationEditorComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => SystemLocationEditorComponent),
      multi: true
    }       
  ]
})
export class SystemLocationEditorComponent implements ControlValueAccessor,  Validator  {
  validate(control: AbstractControl): ValidationErrors {
    const error = { validate: true };
      
    if (!control.value && !control.pristine) {
      return error;
    }
    
    return null as any;
  }
  
  public form = new FormGroup({
    name: new FormControl(null, [Validators.required]),
  });
  
  writeValue(obj: any): void {   
    this.form.patchValue({
      name: obj.name,
    }, { emitEvent: false });
  }

  registerOnChange(fn: any): void {
    this.form.valueChanges.subscribe(fn);
  }
  
  onTouched = () => {
  
  };

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    isDisabled ? this.form.disable() : this.form.enable();
  }
}
