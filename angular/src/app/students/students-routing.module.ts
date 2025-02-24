import { RouterModule, Routes } from "@angular/router";
import { StudentsComponent } from "./students.component";
import { NgModule } from "@angular/core";

const routes: Routes = [
    {
        path: '',
        component: StudentsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class StudentsRoutingModule {}
