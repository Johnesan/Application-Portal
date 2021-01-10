import { I18N } from 'aurelia-i18n';
import { ValidationError } from '../resources/validation-error';
import { Applicant } from './applicant';
import { inject, autoinject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';
import * as environment from '../../config/environment.json';
import { config } from 'process';

@autoinject
export class ApplicantService {
    constructor(private httpClient: HttpClient, private i18n: I18N) {
        httpClient.configure(config => {
            config.useStandardConfiguration();
            config.withBaseUrl(environment.base_url);
        });
    }

    addApplicant(applicant: Applicant) {
        return this.httpClient.fetch('applicant/create', {
            method: 'post',
            headers: { 'Accept-Language': this.i18n.getLocale() },
            body: json(applicant)
        })
            .then(response => response.json())
            .then(applicant => applicant)
            .catch((error: Response) => {
                console.log(error);
                const applicationError = error.headers.get('Application-Error');
                if (applicationError) throw applicationError;
                if (error.status === 400)
                    return error.json().then(errorBody => {
                        if (errorBody.hasOwnProperty('errors')) throw new ValidationError(JSON.stringify(errorBody['errors']));
                        else throw new Error(errorBody || this.i18n.tr('errorOccurred'));
                    })
                else throw Error(this.i18n.tr('errorOccurred'));
            });
    }

    getApplicant(id: number) {
        return this.httpClient.fetch(`applicant/get/${id}`)
            .then(response => response.json())
            .then(applicant => applicant)
            .catch((error: Response) => {
                console.log(error);
                throw new Error(this.i18n.tr('applicant.getApplicantFailed'));
            });
    }
}