﻿import { AlertModal } from './../resources/alert-modal';
import { PromptModal } from './../resources/prompt-modal';
import { BootstrapFormRenderer } from './../resources/bootstrap-form-renderer';
import { Router } from 'aurelia-router';
import { ApplicantService } from '../common/applicant-service';
import { ApplicantValidator } from '../common/applicant-validator';
import { Applicant } from '../common/applicant';
import {autoinject} from 'aurelia-dependency-injection';
import { ValidationControllerFactory, ValidationController, validateTrigger, Validator, ValidateEvent} from 'aurelia-validation';
import { DialogService } from 'aurelia-dialog';


@autoinject
export class AddApplicant {
  controller: ValidationController;
  applicant: Applicant = new Applicant;
  canSave: boolean = false;
  canReset: boolean = false;
  
  constructor(controllerFactory: ValidationControllerFactory, private applicantValidator : ApplicantValidator,
    private applicantService : ApplicantService, private router : Router, private dialogService: DialogService) {
    this.controller = controllerFactory.createForCurrentScope();
    this.controller.validateTrigger = validateTrigger.change;
    this.controller.addRenderer(new BootstrapFormRenderer());
    this.controller.addObject(this.applicant, applicantValidator.getValidationRules());
    this.controller.subscribe(event => this.validateWhole(event));
  }

  saveApplicant(){
    this.controller.validate()
    .then(result => {
      if(result.valid){
        this.applicantService.addApplicant(this.applicant)
        .then((applicantData : Applicant) => {
          this.router.navigateToRoute('application-success', {id: applicantData.id});
          this.displayMessage("You have successfully applied!");
        })
        .catch(error => {
          if(error.name === 'ValidationError'){
             let errors : object = JSON.parse(error.message);
             for (var key in errors) {
               var propertyName = key.charAt(0).toLowerCase() + key.slice(1);
               this.controller.addError(errors[key][0], this.applicant, propertyName);
            }
          }
          else{
            this.displayMessage(error);
          }
        });
      }
    });
  }

  reset(){
    this.dialogService.open({ viewModel: PromptModal, model: 'Are you sure you want to clear all inputs?', lock: false }).whenClosed(response => {
      if (!response.wasCancelled) {
        (document.getElementById('add-applicant-form') as HTMLFormElement).reset();
      }
    });
  }

  private validateWhole(event : ValidateEvent) {
    this.canSave = event.type === 'validate' && this.controller.errors.length === 0;
    this.canReset = event.type === 'validate' && this.nonEmptyFieldExist();
  }

  private nonEmptyFieldExist() : boolean {
    for (var key in this.applicant) {
      if (this.applicant[key] !== null && this.applicant[key] != "" && this.applicant[key] != undefined) return true;
    }
    return false;
  }

  private displayMessage(message : string) {
    this.dialogService.open({ viewModel: AlertModal, model: message, lock: false });
  }
}