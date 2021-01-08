import {RouterConfiguration, Router} from 'aurelia-router';
import { PLATFORM } from "aurelia-framework";

export class App {
  router: Router;

  configureRouter(config: RouterConfiguration, router: Router): void {
    this.router = router;
    config.title = 'Hahn Application';
    config.options.pushState = true;
    config.options.root = '/';
    config.map([
      { route: '', name: 'home', moduleId: PLATFORM.moduleName('application/add-applicant'),   title: 'Apply'},
      { route: 'application-success/:id', name: 'application-success', moduleId: PLATFORM.moduleName('application/application-success'),   title: 'Details'}
    ]);
  }
} 