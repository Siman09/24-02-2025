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
  StudentDto,
  StudentServiceProxy
} from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-edit-class',
  templateUrl: './edit-class.component.html',
  styleUrl: './edit-class.component.css'
})
export class EditClassComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  ClassInput = new ClassDto();
  id: number;
  @Output() onSave = new EventEmitter<any>();
  constructor(
    injector: Injector,
    public _classService: ClassServiceProxy,
    public bsModalRef: BsModalRef,
    public cd:ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
      this._classService.get(this.id).subscribe((result) => {
      this.ClassInput = result;
      this.cd.detectChanges();
    })
  }

  save(): void {
    this.saving = true;
    this._classService.update(this.ClassInput).subscribe(
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
