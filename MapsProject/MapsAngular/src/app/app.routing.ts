import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './_administration/login/login.component';
import { ModerateComponent } from './_administration/moderate/moderate.component';
import { AuthGuard } from './_administration/_guards/auth.guard';
import { MapComponent } from './map/map.component';
import { LeaveModeratePageGuard } from './_administration/_guards/leave-moderate-page.guard';

const appRoutes: Routes = [
    { path: '', component: MapComponent },
    { path: 'auth', component: LoginComponent },
    {
        path: 'moderate',
        component: ModerateComponent,
        canActivate: [AuthGuard],
        canDeactivate: [LeaveModeratePageGuard]
    },

    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);
