import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { TeachersComponent } from "./teachers.component";

const routes: Routes = [
    {
        path: '',
        component: TeachersComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class teachersRoutingModule {}


