import { bootstrap } from 'aurelia-bootstrapper';
import {Aurelia} from 'aurelia-framework';
import * as environment from '../config/environment.json';
import {PLATFORM} from 'aurelia-pal';

bootstrap(async (aurelia: Aurelia) => {
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName('resources/index'))
    .plugin(PLATFORM.moduleName('aurelia-validation'))
    .plugin(PLATFORM.moduleName('aurelia-fetch-client'))
    .plugin(PLATFORM.moduleName('aurelia-dialog'));

  aurelia.use.developmentLogging(environment.debug ? 'debug' : 'warn');

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName('aurelia-testing'));
  }

  await aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app'), document.body));
});
