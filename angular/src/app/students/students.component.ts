import { ChangeDetectorRef, Component, Injector } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { GenderEnum, StudentDto, StudentDtoPagedResultDto, StudentServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateStudentComponent } from './create-student/create-student.component';
import { EditStudentComponent } from './edit-student/edit-student.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';

class PagedStudentRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  animations: [appModuleAnimation()],
  styleUrl: './students.component.css'
})
export class StudentsComponent extends PagedListingComponentBase<StudentDto> {
  StudentList: StudentDto[] = [];
  keyword = '';
  isActive: boolean | null;
  advancedFiltersVisible = false;
  constructor(
    injector: Injector,
    private _studentService: StudentServiceProxy,
    private _modalService: BsModalService,
    cd: ChangeDetectorRef
  ) {
    super(injector, cd);
  }
  protected list(
    request: PagedStudentRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    this._studentService
      .getAll(
        request.keyword,
        request.isActive,
        request.skipCount,
        request.maxResultCount
      )
      .pipe(
        finalize(() => {
          finishedCallback();
          console.log("fgfdsghfdgsd");
        })
      )
      .subscribe((result: StudentDtoPagedResultDto) => {
        this.StudentList = result.items;
        this.showPaging(result, pageNumber);
      });
  }
  get Setgender(): (value: number) => string {
      return (value: number): string => {
        switch (value) {
          case GenderEnum.Male:
            return 'Male';
          case GenderEnum.Female:
            return 'Female';
          case GenderEnum.Other:
            return 'Other';
          default:
            return 'Unknown';
        }
      };
    }
   
    get SetMaritStatus(): (value: boolean) => string {
      return (value: boolean): string => {
          if (value === true) {
              return "Married";
          } else {
              return "Unmarried";
          }
      };
  }
  protected delete(user: StudentDto): void {
    abp.message.confirm(
      this.l('UserDeleteWarningMessage', user.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._studentService.delete(user.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  editUser(user: StudentDto): void {
    this.showCreateOrEditUserDialog(user.id);
  }

  createUser(): void {
    this.showCreateOrEditUserDialog();
  }

  private showCreateOrEditUserDialog(id?: number): void {
    let createOrEditUserDialog: BsModalRef;
    if (!id) {
      createOrEditUserDialog = this._modalService.show(
        CreateStudentComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditUserDialog = this._modalService.show(
        EditStudentComponent,
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
