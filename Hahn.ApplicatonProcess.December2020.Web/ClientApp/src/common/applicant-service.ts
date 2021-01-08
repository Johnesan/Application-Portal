import { ValidationError } from '../resources/validation-error';
import { Applicant } from './applicant';
import { inject } from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import * as environment from '../../config/environment.json';

@inject(HttpClient)
export class ApplicantService{
    constructor(private httpClient: HttpClient) {
         httpClient.configure(config => {
             config.useStandardConfiguration();
             config.withBaseUrl(environment.base_url);
         });
    }

    addApplicant(applicant: Applicant){
        return this.httpClient.fetch('applicant/create', {
            method: 'post',
            body: json(applicant)
            }) 
            .then(response => response.json())
            .then(applicant => applicant)         
            .catch((error : Response) => {
                console.log(error);
                const applicationError = error.headers.get('Application-Error');
                if (applicationError) throw applicationError;
                if(error.status === 400) 
                    return error.json().then(errorBody => {
                        if(errorBody.hasOwnProperty('errors')) throw new ValidationError(JSON.stringify(errorBody['errors']));                        
                        else throw new Error(errorBody || 'An error occured.'); 
                    })
                else throw Error('An error occurred.');
            });
    }

    getApplicant(id: number){
        return this.httpClient.fetch(`applicant/get/${id}`)
            .then(response => response.json())
            .then(applicant => applicant)
            .catch((error: Response) => {
                console.log(error);
                throw new Error('Failed to fetch application details.');
            });
    }
}