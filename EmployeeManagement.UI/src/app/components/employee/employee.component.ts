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
    this.empService.getAll().subscribe((data) => {
      this.employees = data;
    });
  }

  onSubmit() {
    const emp = this.form.value;

    if (emp.id === 0) {
      this.empService.create(emp).subscribe(() => {
        this.loadEmployees();
        this.form.reset({ id: 0 });
      });
    } else {
      this.empService.update(emp.id, emp).subscribe(() => {
        this.loadEmployees();
        this.form.reset({ id: 0 });
      });
    }
  }

  edit(emp: Employee) {
    this.form.patchValue(emp);
  }

  delete(id: number) {
    if (confirm('Are you sure?')) {
      this.empService.delete(id).subscribe(() => {
        this.loadEmployees();
      });
    }
  }
}
