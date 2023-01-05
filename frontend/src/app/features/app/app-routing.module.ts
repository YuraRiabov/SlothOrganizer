import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { AuthorizedGuard } from '@shared/guards/authorized.guard';
import { NgModule } from '@angular/core';
import { UnauthorizedGuard } from '@shared/guards/unauthorized.guard';

const routes: Routes = [
    {
        path: 'auth',
        loadChildren: () => import('../auth/auth.module').then((m) => m.AuthModule),
        canActivate: [UnauthorizedGuard]
    },
    {
        path: '**',
        component: AppComponent,
        canActivate: [AuthorizedGuard]
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}
