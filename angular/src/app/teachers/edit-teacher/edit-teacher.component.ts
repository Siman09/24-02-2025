
import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  ClassDto,
  ClassDtoPagedResultDto,
  ClassServiceProxy,
  GenderEnum,
  StudentDto,
  StudentServiceProxy,
  TeacherDto,
  TeacherServiceProxy
} from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-edit-teacher',
  templateUrl: './edit-teacher.component.html',
  styleUrl: './edit-teacher.component.css'
})
export class EditTeacherComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  genderEnum = [{ key: "Male", value: 1 }, { key: "Female", value: 2 }, { key: "Other", value: 3 }];
  Teacher = new TeacherDto();
  ClassList: ClassDto[] = [];
  id: number;
  @Output() onSave = new EventEmitter<any>();
  constructor(
    injector: Injector,
    public _teacherService: TeacherServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef,
    public _classService: ClassServiceProxy,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.GetClassList();
    this._teacherService.get(this.id).subscribe((result) => {
      this.Teacher = result;
      this.cd.detectChanges();
    })
  }
  GetClassList() {
    this._classService.getAll(undefined, undefined, undefined, undefined).subscribe((result: ClassDtoPagedResultDto) => {
      this.ClassList = result.items;
      this.cd.detectChanges();
    })
  }

  save(): void {
    this.saving = true;
    this._teacherService.update(this.Teacher).subscribe(
      () => {
        this.notify.info(this.l('Updated Successfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
