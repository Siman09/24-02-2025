import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { StudentsComponent } from './students.component';
import { StudentsRoutingModule } from './students-routing.module';
import {CreateStudentComponent} from './create-student/create-student.component';
import {EditStudentComponent} from './edit-student/edit-student.component'
import { CommonModule } from '@node_modules/@angular/common';

@NgModule({
    declarations: [StudentsComponent,CreateStudentComponent,EditStudentComponent],
    imports: [SharedModule,CommonModule, StudentsRoutingModule],
})
export class StudentsModule {}