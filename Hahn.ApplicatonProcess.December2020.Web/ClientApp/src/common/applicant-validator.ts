import { inject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Rule, ValidationRules } from "aurelia-validation";
import { Applicant } from "./applicant";

@inject(HttpClient)
export class ApplicantValidator{  
    constructor(private httpClient: HttpClient) {
     }

    getValidationRules() : Rule<Applicant, any>[][] {
        return ValidationRules
        .ensure((a: Applicant) => a.name).required().minLength(2).withMessage("NameMessage")
        .ensure((a: Applicant) => a.familyName).required().minLength(5).withMessage("NameMessage")
        .ensure((a: Applicant) => a.address).required().minLength(10).withMessage("NameMessage")
        .ensure((a: Applicant) => a.emailAddress).required().email().withMessage("NameMessage")
        .ensure((a: Applicant) => a.age).required().range(10, 60).withMessage("NameMessage")
        .ensure((a: Applicant) => a.countryOfOrigin).required()
        .satisfies((value: string, obj : Applicant) => 
            (value === null || value === undefined || this.validateCountry(value))).withMessage("Invalid country")
        .rules;
    }

    validateCountry(country : string) {
         return this.httpClient.fetch(`https://restcountries.eu/rest/v2/name/${country}?fullText=true`)
        .then(response => {return response.ok})
        .catch(() => {return false});
    }
}
