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
  StudentServiceProxy
} from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrl: './edit-student.component.css'
})
export class EditStudentComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  student = new StudentDto();
  classList:ClassDto[]=[];
  genderEnum=[{key:"Male",value:1},{key:"Female",value:2},{key:"Other",value:3}];
  id: number;
  @Output() onSave = new EventEmitter<any>();
  constructor(
    injector: Injector,
    public _studentService: StudentServiceProxy,
    public _classService:ClassServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
      this.GetClassList();
      this._studentService.get(this.id).subscribe((result) => {
      this.student = result;
      this.cd.detectChanges();
    })
  }
  GetClassList() {
      this._classService.getAll(undefined, undefined, undefined, undefined).subscribe((result: ClassDtoPagedResultDto) => {
        this.classList = result.items;
        this.cd.detectChanges();
      })
    }

  save(): void {
    this.saving = true;
    this._studentService.update(this.student).subscribe(
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
