import { NgModule } from '@angular/core';
import { TeachersComponent } from './teachers.component';
import { teachersRoutingModule } from './teacher-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { EditTeacherComponent } from './edit-teacher/edit-teacher.component';
import { CreateTeacherComponent } from './create-teacher/create-teacher.component';
import { CommonModule } from '@node_modules/@angular/common';



@NgModule({
    declarations: [TeachersComponent,EditTeacherComponent,CreateTeacherComponent],
    imports: [SharedModule,CommonModule, teachersRoutingModule],
})
export class teachersModule {}