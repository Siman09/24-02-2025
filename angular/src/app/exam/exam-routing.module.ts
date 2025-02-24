import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { ExamComponent } from "./exam.component";

const routes: Routes = [
    {
        path: '',
        component: ExamComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ExamRoutingModule {}
