import { ChangeDetectorRef, Component, Injector } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { PagedListingComponentBase, PagedRequestDto } from '../../shared/paged-listing-component-base';
import { ExamDto, ExamDtoPagedResultDto, ExamServiceProxy } from '../../shared/service-proxies/service-proxies';
import { CreateExamComponent } from './create-exam/create-exam.component';
import { EditExamComponent } from './edit-exam/edit-exam.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PermissionCheckerService } from '@node_modules/abp-ng2-module';

class PagedExamRequestDto extends PagedRequestDto
{
  keyword:string;
  isActive:boolean|null;
}

@Component({
  selector: 'app-exam',
  templateUrl: './exam.component.html',
  styleUrl: './exam.component.css',
  animations: [appModuleAnimation()]
})
export class ExamComponent extends PagedListingComponentBase<ExamDto> {
  keyword = '';
  canCreateExam;
  canEditAndDeleteExam;
  ExamList:ExamDto[]=[];
      constructor(injector:Injector,
      private _examService:ExamServiceProxy,
      private _modalService: BsModalService,
      cd: ChangeDetectorRef
  
){
  super(injector,cd);
 }
 protected list( request: PagedExamRequestDto,pageNumber: number,finishedCallback: Function):void
 {
  request.keyword = this.keyword;
  this._examService.getAll(request.keyword,
    request.skipCount,
    request.maxResultCount).pipe(
            finalize(() => {
              finishedCallback();
            })
          )
          .subscribe((result: ExamDtoPagedResultDto) => {
            this.ExamList = result.items;
            this.showPaging(result, pageNumber);
          });
          
 }
 protected delete(score: ExamDto): void {
     abp.message.confirm(
       this.l('UserDeleteWarningMessage', score.studentName),
       undefined,
       (result: boolean) => {
         if (result) {
           this._examService.delete(score.id).subscribe(() => {
             abp.notify.success(this.l('SuccessfullyDeleted'));
             this.refresh();
           });
         }
       }
     );
   }
   editUser(score: ExamDto): void {
    this.showCreateOrEditUserDialog(score.id);
  }

  createUser(): void {
    this.showCreateOrEditUserDialog();
  }

  private showCreateOrEditUserDialog(id?: number): void {
    let createOrEditUserDialog: BsModalRef;
    if (!id) {
      createOrEditUserDialog = this._modalService.show(
        CreateExamComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditUserDialog = this._modalService.show(
        EditExamComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }
    createOrEditUserDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}
