import { HttpClient, json } from 'aurelia-fetch-client';
import { autoinject } from 'aurelia-dependency-injection';
import { activationStrategy, RouterConfiguration, Router } from 'aurelia-router';
import { PLATFORM, customElement } from "aurelia-framework";
import { I18N } from 'aurelia-i18n';
import * as environment from '../config/environment.json';
import { AlertModal } from 'resources/alert-modal';
import { DialogService } from 'aurelia-dialog';
import { BindingSignaler } from 'aurelia-templating-resources';
import { EventAggregator } from 'aurelia-event-aggregator';
import I18NextXhrBackend from 'i18next-xhr-backend';


@autoinject
export class App {
  router: Router;
  availableLocales: Array<any>
  selectedLocale: string;

  constructor(private i18n: I18N, private httpClient: HttpClient, private dialogService: DialogService) {
    this.availableLocales = [
      { name: 'english', value: 'en' },
      { name: 'german', value: 'de' }
    ];
    let currentLocale = sessionStorage.getItem('locale');
    if (currentLocale == '' || currentLocale == null || currentLocale === undefined ||
      this.availableLocales.find(x => x.value == currentLocale) === undefined)
      currentLocale = this.availableLocales[0].value;

    this.selectedLocale = currentLocale;
    this.i18n.setLocale(this.selectedLocale).then(() =>
      this.availableLocales.forEach(element => {
        element.name = this.i18n.tr(element.name);
      }));
  }

  configureRouter(config: RouterConfiguration, router: Router): void {
    this.router = router;
    config.title = this.i18n.tr('title');
    config.options.pushState = true;
    config.options.root = '/';
    config.map([
      { route: '', name: 'home', moduleId: PLATFORM.moduleName('application/add-applicant'), title: this.i18n.tr('apply') },
      { route: 'application-success/:id', name: 'application-success', moduleId: PLATFORM.moduleName('application/application-success'), title: this.i18n.tr('details') }
    ]);
  }

  setLocale(locale) {
    this.i18n.setLocale(locale)
      .then(() => {
        sessionStorage.setItem('locale', locale);
        location.reload();
      })
      .catch(error => this.displayMessage(this.i18n.tr('setLocaleFailed')));
  }

  private displayMessage(message: string) {
    this.dialogService.open({ viewModel: AlertModal, model: message, lock: false });
  }
} 