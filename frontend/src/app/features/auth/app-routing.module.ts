import { RouterModule, Routes } from '@angular/router';

import { NgModule } from '@angular/core';

const routes: Routes = [
    {
        path: 'auth',
        loadChildren: () =>
<<<<<<<< HEAD:frontend/src/app/features/auth/app-routing.module.ts
            import('../features/auth/auth.module').then((m) => m.AuthModule)
========
            import('../auth/auth.module').then((m) => m.AuthModule)
>>>>>>>> feature/sign-up:frontend/src/app/features/app/app-routing.module.ts
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}
