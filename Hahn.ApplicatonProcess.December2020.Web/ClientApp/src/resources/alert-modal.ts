import {inject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';

@inject(DialogController)
export class AlertModal {
    message: string;
   constructor(private controller : DialogController) {
      this.controller = controller;
   }
   activate(message) {
      this.message = message;
   }
}