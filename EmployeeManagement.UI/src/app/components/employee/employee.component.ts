import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/models/employee.model';
import { EmployeeService } from 'src/app/services/employee.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html'
})
export class EmployeeComponent implements OnInit {
  employees: Employee[] = [];
  form: FormGroup;

  constructor(private empService: EmployeeService, private fb: FormBuilder) {
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
        next: () => {
        this.loadEmployees();
        this.form.reset({ id: 0 });
      },
      error: () => alert('Something Went Wrong!')
    });
    } else {
      this.empService.update(emp).subscribe( {
        next: () => {
        this.loadEmployees();
        this.form.reset({ id: 0 });
      },
      error: () => alert('Something Went Wrong!')
    });
    }
  }

  edit(emp: Employee) {
    this.form.patchValue(emp);
  }

  delete(id: number) {
    if (confirm('Are you sure?')) {
      this.empService.delete(id).subscribe({
        next: () => {
          this.loadEmployees();
        }, 
        error: () => alert("Something went wrong")
      });
    }
  }
}
