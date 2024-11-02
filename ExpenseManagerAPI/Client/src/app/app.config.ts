import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { AppCommonModule } from './app.common.module';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

export const appConfig: ApplicationConfig = {
  providers: [
    importProvidersFrom(AppCommonModule), // import providers declared in @NgModule in AppCommonModule
    provideRouter(routes), provideAnimationsAsync()
  ]
};
