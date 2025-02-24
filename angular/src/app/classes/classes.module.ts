import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { classesRoutingModule } from './classes-routing.module';
import { ClassesComponent } from './classes.component';
import{CreateClassComponent} from './create-class/create-class.component';
import{EditClassComponent} from './edit-class/edit-class.component';
import { CommonModule } from '@node_modules/@angular/common';

@NgModule({
    declarations: [ClassesComponent,CreateClassComponent,EditClassComponent],
    imports: [SharedModule,CommonModule, classesRoutingModule],
})
export class ClassesModule {}