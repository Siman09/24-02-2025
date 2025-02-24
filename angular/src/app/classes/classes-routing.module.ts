import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { ClassesComponent } from "./classes.component";

const routes: Routes = [
    {
        path: '',
        component: ClassesComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class classesRoutingModule {}
