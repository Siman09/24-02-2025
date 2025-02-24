import { NgModule } from '@angular/core';
import { ExamComponent } from './exam.component';
import { CreateExamComponent } from './create-exam/create-exam.component';
import { EditExamComponent } from './edit-exam/edit-exam.component';
import { SharedModule } from '../../shared/shared.module';
import { ExamRoutingModule } from './exam-routing.module';
import { CommonModule } from '@node_modules/@angular/common';

@NgModule({
    declarations: [ExamComponent,CreateExamComponent,EditExamComponent],
    imports: [SharedModule,CommonModule,ExamRoutingModule],
})
export class ExamModule {}