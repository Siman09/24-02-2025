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
  ClassServiceProxy,
  ExamDto,
  ExamServiceProxy,
  StudentDto,
  StudentServiceProxy,
  TeacherDto,
  TeacherServiceProxy,
} from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-edit-exam',
  templateUrl: './edit-exam.component.html',
  styleUrl: './edit-exam.component.css'
})
export class EditExamComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  ScoreInput = new ExamDto();
  StudentList:StudentDto[]=[];
  TeacherList:TeacherDto[]=[];
  id: number;
  @Output() onSave = new EventEmitter<any>();
  constructor(
    injector: Injector,
    public _examService: ExamServiceProxy,
    public _studnetService:StudentServiceProxy,
    public _TeacherService:TeacherServiceProxy,
    public bsModalRef: BsModalRef,
    public cd:ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
      this.GetEditData();
  }
  GetEditData()
  {
      this._studnetService.getAll(undefined,undefined,undefined,undefined).subscribe((result) => {
      this.StudentList = result.items;
      this.cd.detectChanges();

      })
      this._TeacherService.getAll(undefined,undefined,undefined,undefined).subscribe((result) => {
      this.TeacherList = result.items;
      this.cd.detectChanges();

      })
      this._examService.get(this.id).subscribe((result) => {
      this.ScoreInput = result;
      this.cd.detectChanges();
      })

  }

  save(): void {
    this.saving = true;
    this._examService.update(this.ScoreInput).subscribe(
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
