import { bootstrap } from 'aurelia-bootstrapper';
import { Aurelia } from 'aurelia-framework';
import * as environment from '../config/environment.json';
import { PLATFORM } from 'aurelia-pal';
import { TCustomAttribute } from 'aurelia-i18n';
import Backend from 'i18next-xhr-backend';

bootstrap(async (aurelia: Aurelia) => {
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName('resources/index'))
    .plugin(PLATFORM.moduleName('aurelia-validation'))
    .plugin(PLATFORM.moduleName('aurelia-fetch-client'))
    .plugin(PLATFORM.moduleName('aurelia-dialog'))
    .plugin(PLATFORM.moduleName('aurelia-i18n'), (instance) => {
      let aliases = ['t', 'i18n'];
      TCustomAttribute.configureAliases(aliases);
      instance.i18next.use(Backend);
      return instance.setup({
        backend: {
          loadPath: './locales/{{lng}}/{{ns}}.json',
        },
        attributes: aliases,
        lng: 'de',
        fallbackLng: 'en',
        debug: false
      });
    });

  aurelia.use.developmentLogging(environment.debug ? 'debug' : 'warn');

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName('aurelia-testing'));
  }

  await aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app'), document.body));
});
