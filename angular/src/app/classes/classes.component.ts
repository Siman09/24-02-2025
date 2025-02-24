import { ChangeDetectorRef, Component, Injector } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { ClassDto,ClassDtoPagedResultDto,ClassServiceProxy, } from '@shared/service-proxies/service-proxies';
// import{ClassDtoPagedResultDto} from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { CreateClassComponent } from './create-class/create-class.component';
import { EditClassComponent } from './edit-class/edit-class.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';

class PagedClassesRequestDto extends PagedRequestDto
{
  keyword:string;
  isActive:boolean|null;
}

@Component({
  selector: 'app-classes',
  templateUrl: './classes.component.html',
  styleUrl: './classes.component.css',
  animations: [appModuleAnimation()]
})
export class ClassesComponent extends PagedListingComponentBase<ClassDto> {
  keyword = '';
  ClassList:ClassDto[]=[];
      constructor(injector:Injector,
      private _classService:ClassServiceProxy,
      private _modalService: BsModalService,
      cd: ChangeDetectorRef
  
){
  super(injector,cd);
 }
 protected list( request: PagedClassesRequestDto,pageNumber: number,finishedCallback: Function):void
 {
  request.keyword = this.keyword;
  this._classService.getAll(request.keyword,
    request.isActive,
    request.skipCount,
    request.maxResultCount).pipe(
            finalize(() => {
              finishedCallback();
            })
          )
          .subscribe((result: ClassDtoPagedResultDto) => {
            this.ClassList = result.items;
            this.showPaging(result, pageNumber);
          });
                console.log("output is:-"+this.isGranted('pages.CreateTeacher'));
          
 }
 
 protected delete(classes: ClassDto): void {
  debugger
     abp.message.confirm(
       this.l('UserDeleteWarningMessage', classes.title),
       undefined,
       (result: boolean) => {
         if (result) {
           this._classService.delete(classes.id).subscribe(() => {
             abp.notify.success(this.l('SuccessfullyDeleted'));
             this.refresh();
           });
         }
       }
     );
   }
   editUser(user: ClassDto): void {
    this.showCreateOrEditUserDialog(user.id);
  }

  createUser(): void {
    this.showCreateOrEditUserDialog();
  }

  private showCreateOrEditUserDialog(id?: number): void {
    let createOrEditUserDialog: BsModalRef;
    if (!id) {
      createOrEditUserDialog = this._modalService.show(
        CreateClassComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditUserDialog = this._modalService.show(
        EditClassComponent,
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
