import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { AppCommonModule } from './app.common.module';

export const appConfig: ApplicationConfig = {
  providers: [
    importProvidersFrom(AppCommonModule), // import providers declared in @NgModule in AppCommonModule
    provideRouter(routes)
  ]
};
