import {inject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';

@inject(DialogController)
export class AlertModal {
    message: string;
   constructor(private controller : DialogController) {
   }
   activate(message) {
      this.message = message;
   }
}