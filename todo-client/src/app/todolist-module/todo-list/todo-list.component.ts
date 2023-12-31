import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
// ~Todo Module
import { Todo } from '../../models/todo';
import { TodolistService } from '../../../services/todolist.service';
// Nz Module
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzInputModule } from 'ng-zorro-antd/input';
import { SigninComponent } from '../../signin/signin.component';

// Toasts
import { ToastService } from '../../../services/toasts.service';
import { ToastsContainerComponent } from '../../toasts-container/toasts-container.component';
@Component({
  selector: 'app-todo-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NzTableModule,
    NzDividerModule,
    NzCheckboxModule,
    NzPopconfirmModule,
    NzInputModule,
    SigninComponent,
    ToastsContainerComponent,
  ],
  templateUrl: './todo-list.component.html',
  styleUrl: './todo-list.component.css',
  providers: [TodolistService],
})
export class TodoListComponent implements OnInit {
  todoListSource: Todo[] = [];
  editCache: { [key: string]: { edit: boolean; data: Todo } } = {};
  classDone = 'btn btn-outline-success';
  classTodo = 'btn btn-outline-warning';
  @ViewChild('createdSuccessTpl', { static: true })
  successTpl!: TemplateRef<any>;
  @ViewChild('modifySuccessTpl', { static: true })
  modifyTpl!: TemplateRef<any>;
  @ViewChild('deletedSuccessTpl', { static: true })
  deletedTpl!: TemplateRef<any>;
  pageSize!: 5;
  loading = true;
  constructor(
    private todoService: TodolistService,
    private toastsService: ToastService
  ) {}

  //#region Load Todo List
  ngOnInit(): void {
    this.getTodoList();
  }

  getTodoList() {
    this.todoService.getTodoList().subscribe({
      next: (res) => {
        this.todoListSource = res;
        console.log(this.todoListSource);
      },
      error: (err) => {
        console.log(`Error at get TodoList ${err}`);
      },
      complete: () => {
        this.updateEditCache();
        this.loading = false;
      },
    });
  }
  //#endregion Load Todo List

  //#region  Edit
  startEdit(id: string) {
    console.log(id);
    this.editCache[id].edit = true;
  }

  cancelEdit(id: string) {
    const index = this.todoListSource.findIndex((item) => item.Id === id);
    this.editCache[id] = {
      data: { ...this.todoListSource[index] },
      edit: false,
    };
  }

  saveEdit(id: string) {
    const index = this.todoListSource.findIndex(
      (todoItem) => todoItem.Id == id
    );
    Object.assign(this.todoListSource[index], this.editCache[id].data);
    this.todoService.modifyTodoItem(this.todoListSource[index]).subscribe({
      error: (err) => console.log(`Error on Save Item ${err}`),
      complete: () => this.toastsService.showCreatedSuccess(this.modifyTpl),
    });
    this.editCache[id].edit = !this.editCache[id].edit;
  }

  updateEditCache(): void {
    this.todoListSource.forEach((item) => {
      this.editCache[item.Id] = {
        edit: false,
        data: { ...item },
      };
      console.log(item);
    });
    console.log(this.editCache);
  }

  modifyCompleteStatus(id: string) {
    this.editCache[id].data.IsComplete = !this.editCache[id].data.IsComplete;
    this.todoService
      .modifyCompletedStatus(id, this.editCache[id].data.IsComplete)
      .subscribe({
        error: (err) => console.error(`Error on Change Satus Item ${err}`),
        complete: () => this.toastsService.showCreatedSuccess(this.modifyTpl),
      });
  }
  //#endregion Edit

  //#region Delete
  onDelete(id: string) {
    this.todoService.deleteTodoItem(id).subscribe({
      error: (err) => console.log(`Error on Delete Item ${err}`),
      complete: () => this.toastsShowDeleted(this.deletedTpl),
    });
    this.todoListSource = this.todoListSource.filter((todo) => todo.Id != id);
  }
  //#endregion Delete

  //#region Create
  onCreate(description: string) {
    let todo: Todo = { Id: '', Description: '', IsComplete: false };

    this.todoService.createTodoItem(description).subscribe({
      next: (res) => {
        todo = res;
        this.todoListSource = [todo, ...this.todoListSource];
        this.editCache[todo.Id] = {
          edit: false,
          data: { ...todo },
        };
        console.log(this.editCache); // remove after
      },
      error: (err) => alert(err),
      complete: () => {
        this.toastsShowCreated(this.successTpl);
      },
    });

    this.editCache[todo.Id] = { edit: false, data: todo };
  }
  //#endregion Create

  //#region Search
  searchTodoItem(search: string) {
    if (search !== '') {
      this.loading = true;
      this.todoService.searchTodoItem(search).subscribe({
        next: (res) => {
          this.todoListSource = res;
        },
        error: (err) => console.log(`Error on search item: ${err}`),
        complete: () => {
          this.updateEditCache();
          this.loading = false;
        },
      });
    }
  }

  onKey(event: any) {
    var searchString = event.target.value;

    if (searchString === '') {
      this.getTodoList();
    } else {
      this.searchTodoItem(searchString);
    }
  }
  //#endregion Search

  //#region Toasts
  toastsShowCreated(template: TemplateRef<any>) {
    this.toastsService.showCreatedSuccess(template);
  }
  toastsShowDeleted(template: TemplateRef<any>) {
    this.toastsService.showDeletedSuccess(template);
  }
  //#endregion Toasts
}
