import { inject } from 'aurelia-framework';
import { DialogController } from 'aurelia-dialog';

@inject(DialogController)
export class PromptModal {
   message: string;

   constructor(private controller: DialogController) {
      this.controller = controller;
      controller.settings.centerHorizontalOnly = false;
      controller.settings.overlayDismiss = true;
   }
   activate(message) {
      this.message = message;
   }
}