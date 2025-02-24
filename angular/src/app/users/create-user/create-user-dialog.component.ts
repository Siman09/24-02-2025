import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, map as _map, orderBy } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  UserServiceProxy,
  CreateUserDto,
  RoleDto,
  RoleServiceProxy,
  PermissionDtoListResultDto,
  PermissionDto
} from '@shared/service-proxies/service-proxies';
import { AbpValidationError } from '@shared/components/validation/abp-validation.api';

@Component({
  templateUrl: './create-user-dialog.component.html'
})
export class CreateUserDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  user = new CreateUserDto();
  roles: RoleDto[] = [];
  permissions: PermissionDto[] = [];
  parentchildMaping:any[]=[];
  checkedRolesMap: { [key: string]: boolean } = {};
  defaultRoleCheckedStatus = false;
  defaultPermissionCheckedStatus = true;
  checkedPermissionsMap: { [key: string]: boolean } = {};
  passwordValidationErrors: Partial<AbpValidationError>[] = [
    {
      name: 'pattern',
      localizationKey:
        'PasswordsMustBeAtLeast8CharactersContainLowercaseUppercaseNumber',
    },
  ];
  confirmPasswordValidationErrors: Partial<AbpValidationError>[] = [
    {
      name: 'validateEqual',
      localizationKey: 'PasswordsDoNotMatch',
    },
  ];

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _userService: UserServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef,
    private _roleService: RoleServiceProxy,

  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.user.isActive = true;
    this.getRoleAndPermission();
    
  }

  setInitialRolesStatus(): void {
    _map(this.roles, (item) => {
      this.checkedRolesMap[item.normalizedName] = this.isRoleChecked(
        item.normalizedName
      );
    });
  }

  isRoleChecked(normalizedName: string): boolean {
    // just return default role checked status
    // it's better to use a setting
    return this.defaultRoleCheckedStatus;
  }

  onRoleChange(role: RoleDto, $event) {
    this.checkedRolesMap[role.normalizedName] = $event.target.checked;
  }

  getCheckedRoles(): string[] {
    const roles: string[] = [];
    _forEach(this.checkedRolesMap, function (value, key) {
      if (value) {
        roles.push(key);
      }
    });
    return roles;
  }

  // save(): void {
  //   this.saving = true;
  //   this.user.roleNames = this.getCheckedRoles();
  //   this._userService.create(this.user).subscribe(
  //     () => {
  //       this.notify.info(this.l('SavedSuccessfully'));
  //       this.bsModalRef.hide();
  //       this.onSave.emit();
  //     },
  //     () => {
  //       this.saving = false;
  //     }
  //   );
    
  // }
  
  save(): void {
      this.saving = true;
      const user = new CreateUserDto();
      user.init(this.user);
      user.grantedPermissions = this.getCheckedPermissions();
  
      this._userService
        .create(user)
        .subscribe(
          () => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.bsModalRef.hide();
            this.onSave.emit(null);
          },
          () => {
            this.saving = false;
            this.cd.detectChanges();
          }
        );
    }

    getCheckedPermissions(): string[] {
      const permissions: string[] = [];
      _forEach(this.checkedPermissionsMap, function (value, key) {
        if (value) {
          permissions.push(key);
        }
      });
      return permissions;
    }
  
  getRoleAndPermission()
  {
    // this._userService.getRoles().subscribe((result) => {
    //   this.roles = result.items;
    //   this.setInitialRolesStatus();
    //   this.cd.detectChanges();
    // });

     this._roleService
          .getAllPermissions()
          .subscribe((result: PermissionDtoListResultDto) => {
            this.permissions = result.items;
            this.setMappingParentChild()
            this.setInitialPermissionsStatus();
            this.cd.detectChanges();
          });
  }

  setInitialPermissionsStatus(): void {
    _map(this.permissions, (item) => {
      this.checkedPermissionsMap[item.name] = this.isPermissionChecked(
        item.name
      );
    });
  }

  isPermissionChecked(permissionName: string): boolean {
    // just return default permission checked status
    // it's better to use a setting
    return this.defaultPermissionCheckedStatus;
  }
  onPermissionChange(permission: PermissionDto, $event) {
    this.checkedPermissionsMap[permission.name] = $event.target.checked;
  }

  setMappingParentChild() {
    for(let permission of this.permissions)
    {
      console.log(permission)
    }
  }
 

}
