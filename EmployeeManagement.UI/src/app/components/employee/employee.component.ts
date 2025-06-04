import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/models/employee.model';
import { EmployeeService } from 'src/app/services/employee.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html'
})
export class EmployeeComponent implements OnInit {
  employees: Employee[] = [];
  form: FormGroup;

  constructor(private empService: EmployeeService, private fb: FormBuilder, private toastr: ToastrService) {
    this.form = this.fb.group({
      id: [0],
      name: ['', Validators.required],
      department: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees() {
    this.empService.getAll().subscribe( {
      next : (data) => {
        this.employees = data;
      },
      error: () => alert('Something went wrong!')
    });
  }

  onSubmit() {
    const emp = this.form.value;

    if (emp.id == 0) {
      this.empService.create(emp).subscribe( {
        next: (res: any) => {
        res.isSuccess ? this.toastr.success(res.message) : this.toastr.error(res.message);
        this.form.reset({ id: 0 });
      },
      error: (error) => this.toastr.error(JSON.stringify(error)),
      complete: () => this.loadEmployees()
    });
  } else {
    this.empService.update(emp).subscribe( {
      next: (res: any) => {
        debugger;
        res.isSuccess ? this.toastr.success(res.message) : this.toastr.error(res.message);
        this.form.reset({ id: 0 });
      },
      error: (error) => this.toastr.error(JSON.stringify(error)),
      complete: () => this.loadEmployees()
    });
    }
  }

  edit(emp: Employee) {
    this.form.patchValue(emp);
  }

  delete(id: number) {
    if (confirm('Are you sure?')) {
      this.empService.delete(id).subscribe({
        next: (res: any) => {
          res.isSuccess ? this.toastr.success(res.message) : this.toastr.error(res.message);
          this.loadEmployees();
        },
        error: (error) => this.toastr.error(JSON.stringify(error)),
      });
    }
  }
}
