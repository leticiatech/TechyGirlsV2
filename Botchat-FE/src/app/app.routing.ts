import { RouterModule, Routes } from "@angular/router";

import { InicioComponent } from "./inicio/inicio.component";



const APP_ROUTES: Routes = [
    // { path: 'registro', component: RegistroComponent },
    { path: '', component: InicioComponent },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];

export const routing = RouterModule.forRoot(APP_ROUTES);
