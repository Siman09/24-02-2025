import { ChangeDetectorRef, Component, Injector, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { GenderEnum, StudentDto, StudentDtoPagedResultDto, StudentServiceProxy, TeacherDto, TeacherDtoPagedResultDto, TeacherServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateTeacherComponent } from './create-teacher/create-teacher.component';
import { EditTeacherComponent } from './edit-teacher/edit-teacher.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';

class PagedStudentRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
  styleUrl: './teachers.component.css',
  animations: [appModuleAnimation()]
})
export class TeachersComponent extends PagedListingComponentBase<TeacherDto> {
  TeacherList: TeacherDto[] = [];
  keyword = '';
  isActive: boolean | null;
  advancedFiltersVisible = false;
  constructor(
    injector: Injector,
    private _teacherService: TeacherServiceProxy,
    private _modalService: BsModalService,
    cd: ChangeDetectorRef
  ) {
    super(injector, cd);
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
  protected list(
    request: PagedStudentRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    this._teacherService
      .getAll(
        request.keyword,
        request.isActive,
        request.skipCount,
        request.maxResultCount
      )
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: TeacherDtoPagedResultDto) => {
        this.TeacherList = result.items;
        this.showPaging(result, pageNumber);
      });
  }
  protected delete(user: TeacherDto): void {
    abp.message.confirm(
      this.l('UserDeleteWarningMessage', user.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._teacherService.delete(user.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            
            this.refresh();
          });
        }
      }
    );
  }
 
  editUser(input: TeacherDto): void {
    this.showCreateOrEditUserDialog(input.id);
  }

  createUser(): void {
    this.showCreateOrEditUserDialog();
  }

  private showCreateOrEditUserDialog(id?: number): void {
    let createOrEditUserDialog: BsModalRef;
    if (!id) {
      createOrEditUserDialog = this._modalService.show(
        CreateTeacherComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditUserDialog = this._modalService.show(
        EditTeacherComponent,
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
