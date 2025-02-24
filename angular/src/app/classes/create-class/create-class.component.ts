import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
} from '@angular/core';
// import { BsModalRef } from 'ngx-bootstrap/modal';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { ClassDto, ClassServiceProxy, CreateClassDto } from '../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from '../../../shared/app-component-base';
import { EditClassComponent } from '../edit-class/edit-class.component';

@Component({
  selector: 'app-create-class',
  templateUrl: './create-class.component.html',
  styleUrl: './create-class.component.css'
})
export class CreateClassComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  ClassInput = new CreateClassDto();
  defaultRoleCheckedStatus = false;
  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _modalService:BsModalRef,
    public _classService: ClassServiceProxy,
    public bsModalRef: BsModalRef,
  ) {
    super(injector);
  }
  ngOnInit(): void {
   
  }

  save(): void {
    this.saving = true;
    this._classService.create(this.ClassInput).subscribe(
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
