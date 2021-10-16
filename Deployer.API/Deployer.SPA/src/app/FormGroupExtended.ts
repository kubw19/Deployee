import { FormGroup } from "@angular/forms";

export class FormGroupExtended extends FormGroup {

    public IsNotValid(controlName: string): boolean {
        return FormGroupExtended.IsNotValidStatic(this, controlName);
    }

    public static IsNotValidStatic(form, controlName: string): boolean {
        return form.controls[controlName].invalid && (form.controls[controlName].dirty || form.controls[controlName].touched || form.root._isInValid == true)
    }

    private _isInValid
    public Validate(): boolean {
        return FormGroupExtended.ValidateStatic(this)
    }
    public static ValidateStatic(form: FormGroup | any): boolean {
        form._isInValid = form.invalid
        return !form._isInValid;
    }
}