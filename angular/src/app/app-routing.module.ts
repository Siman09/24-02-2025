import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { AppComponent } from './app.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    {
                        path: 'home',
                        loadChildren: () => import('./home/home.module').then((m) => m.HomeModule),
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'about',
                        loadChildren: () => import('./about/about.module').then((m) => m.AboutModule),
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'users',
                        loadChildren: () => import('./users/users.module').then((m) => m.UsersModule),
                        data: { permission: 'Pages.Users' },
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'roles',
                        loadChildren: () => import('./roles/roles.module').then((m) => m.RolesModule),
                        data: { permission: 'Pages.Roles' },
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'tenants',
                        loadChildren: () => import('./tenants/tenants.module').then((m) => m.TenantsModule),
                        data: { permission: 'Pages.Tenants' },
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'students',
                        loadChildren: () => import('./students/students.module').then((m) => m.StudentsModule),
                        data: { permission: 'pages.school_management'},
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'classes',
                        loadChildren: () => import('./classes/classes.module').then((m) => m.ClassesModule),
                        data: { permission: 'pages.school_management'},
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'teachers',
                        loadChildren: () => import('./teachers/teacher.module').then((m) => m.teachersModule),
                        data: { permission: 'pages.school_management'},
                        canActivate: [AppRouteGuard]
                    },
                    {
                        path: 'exam',
                        loadChildren: () => import('./exam/exam.module').then((m) => m.ExamModule),
                        data: { permission: 'pages.school_management'},
                        canActivate: [AppRouteGuard]
                    },
                    
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
