import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
// ~Todo Module
import { Todo } from '../../models/todo';
import { TodolistService } from '../../../services/todolist.service';
import { TodoColumns } from '../../models/columnsSchema';
// Nz Module
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzInputModule } from 'ng-zorro-antd/input';
import {
  NzNotificationModule,
  NzNotificationPlacement,
  NzNotificationService,
} from 'ng-zorro-antd/notification';
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
    NzNotificationModule,
    NzInputModule,
  ],
  templateUrl: './todo-list.component.html',
  styleUrl: './todo-list.component.css',
  providers: [TodolistService],
})
export class TodoListComponent implements OnInit {
  tableLable!: string[];
  todoListSource: Todo[] = [];
  editCache: { [key: string]: { edit: boolean; data: Todo } } = {};
  classDone = 'btn btn-outline-success';
  classTodo = 'btn btn-outline-warning';
  constructor(
    private todoService: TodolistService,
    private notification: NzNotificationService
  ) {
    this.tableLable = TodoColumns.map((key) => key.label);
  }

  //#region Load Todo List
  ngOnInit(): void {
    this.getTodoList();
  }

  getTodoList() {
    this.todoService.getTodoList().subscribe({
      next: (value) => {
        this.todoListSource = value; // asign response from API to variable todoListSource
      },
      complete: () => this.updateEditCache(), // update when todoListSource has data
    });
  }
  //#endregion Load Todo List

  //#region  Edit
  startEdit(id: string) {
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
    this.todoService.modifyTodoItem(this.todoListSource[index]);
    this.editCache[id].edit = !this.editCache[id].edit;
  }

  updateEditCache(): void {
    this.todoListSource.forEach((item) => {
      this.editCache[item.Id] = {
        edit: false,
        data: { ...item },
      };
    });
  }

  modifyCompleteStatus(id: string) {
    this.editCache[id].data.IsComplete = !this.editCache[id].data.IsComplete;
    this.todoService.modifyCompletedStatus(
      id,
      this.editCache[id].data.IsComplete
    );
  }
  //#endregion Edit

  //#region Delete
  onDelete(id: string) {
    this.todoService.deleteTodoItem(id);
    this.todoListSource = this.todoListSource.filter((todo) => todo.Id != id);
  }
  //#endregion Delete

  //#region Create
  onCreate(description: string) {
    let todo: Todo = { Id: '', Description: '', IsComplete: false };
    this.todoService.createTodoItem(description).subscribe({
      next: (res) => (todo = res),
      error: (err) => console.error('Create new item error: ' + err),
      complete: () =>
        console.log(`after create: ${todo.Id} - ${todo.Description}`),
    });

    this.todoListSource.unshift(todo);
    this.editCache[todo.Id] = { edit: false, data: todo };
  }
  //#endregion Create

  createBasicNotification(position: NzNotificationPlacement): void {
    this.notification.warning(
      'Notification Title',
      'This is the content of the notification. This is the content of the notification. This is the content of the notification.'
    );
  }
}
