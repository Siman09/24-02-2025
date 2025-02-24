import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, map as _map, forEach } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  StudentServiceProxy,
  CreateStudentDto,
  ClassServiceProxy,
  ClassDto,
  ClassDtoPagedResultDto,
  GenderEnum
} from '@shared/service-proxies/service-proxies';
import { AbpValidationError } from '@shared/components/validation/abp-validation.api';
import { finalize } from '@node_modules/rxjs/dist/types';

@Component({
  selector: 'app-create-student',
  templateUrl: './create-student.component.html',
  styleUrl: './create-student.component.css'
})
export class CreateStudentComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  classList: ClassDto[] = [];
  genderEnum=[{key:"Male",value:1},{key:"Female",value:2},{key:"Other",value:3}];
  student = new CreateStudentDto();
  defaultRoleCheckedStatus = false;
  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _studentService: StudentServiceProxy,
    public _classService: ClassServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.GetClassList();
  }
  GetClassList() {
    this._classService.getAll(undefined, undefined, undefined, undefined).subscribe((result: ClassDtoPagedResultDto) => {
      this.classList = result.items;
      this.cd.detectChanges();
    })
  }
  save(): void {
    this.saving = true;
    this._studentService.create(this.student).subscribe(
      () => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
