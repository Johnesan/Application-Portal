import { Router } from 'aurelia-router';
import { Applicant } from '../common/applicant';
import { ApplicantService } from '../common/applicant-service';
import { autoinject } from 'aurelia-framework';
import { DialogService } from 'aurelia-dialog';
import { AlertModal } from './../resources/alert-modal';

@autoinject
export class ApplicationSuccess {
  id : number;
  applicant = new Applicant;
  constructor(private applicantService: ApplicantService, private dialogService : DialogService,
    private router : Router){
  }

  activate(params, routeConfig, navigationInstruction) {
    this.id = params.id;
    this.applicantService.getApplicant(this.id)
      .then(applicant => { this.applicant = applicant })
      .catch(error => { this.displayMessage(error) });
  }

  newApplication(){
    this.router.navigateToRoute('home');
  }

  private displayMessage(message : string) {
    this.dialogService.open({ viewModel: AlertModal, model: message, lock: false });
  }
}