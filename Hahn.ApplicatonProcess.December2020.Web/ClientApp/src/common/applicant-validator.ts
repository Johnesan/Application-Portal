import { I18N } from 'aurelia-i18n';
import { inject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Rule, ValidationRules } from "aurelia-validation";
import { Applicant } from "./applicant";

@inject(HttpClient, I18N)
export class ApplicantValidator{  
    constructor(private httpClient: HttpClient, private i18n : I18N) {
     }

    getValidationRules() : Rule<Applicant, any>[][] {
        return ValidationRules
        .ensure((a: Applicant) => a.name).required().minLength(5).withMessage(this.i18n.tr('applicant.nameLengthValidation'))
        .ensure((a: Applicant) => a.familyName).required().minLength(5).withMessage(this.i18n.tr('applicant.familyNameLengthValidation'))
        .ensure((a: Applicant) => a.address).required().minLength(10).withMessage(this.i18n.tr('applicant.addressLengthValidation'))
        .ensure((a: Applicant) => a.emailAddress).required().matches(/\S+@\S+\.\S+/).withMessage(this.i18n.tr('applicant.emailValidation'))
        .ensure((a: Applicant) => a.age).required().range(20, 60).withMessage(this.i18n.tr('applicant.ageRangeValidation'))
        .ensure((a: Applicant) => a.countryOfOrigin).required()
        .satisfies((value: string, obj : Applicant) => 
            (value === null || value === undefined || this.validateCountry(value))).withMessage(this.i18n.tr('applicant.countryValidation'))
        .rules;
    }

    validateCountry(country : string) {
         return this.httpClient.fetch(`https://restcountries.eu/rest/v2/name/${country}?fullText=true`)
        .then(response => {return response.ok})
        .catch(() => {return false});
    }
}
