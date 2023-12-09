import { Injectable } from '@angular/core';
import { Todo } from '../app/models/todo';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TodolistService {
  constructor(private http: HttpClient) {}

  apiUrl = 'https://localhost:7219/api/TodoItems';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Headers': 'Content-Type, Authorization',
      'Access-Control-Allow-Methods': '*',
    }),
  };

  getTodoList(): Observable<Todo[]> {
    return this.http.get<Todo[]>(`${this.apiUrl}`);
  }

  createTodoItem(description: string): Observable<Todo> {
    let todo: Todo = {
      Id: '',
      Description: description,
      IsComplete: false,
    };

    return this.http.post<Todo>(`${this.apiUrl}`, todo, this.httpOptions);
  }

  modifyTodoItem(todo: Todo): void {
    this.http
      .put<Todo>(`${this.apiUrl}/update/`, todo, this.httpOptions)
      .subscribe({
        error: (err) => console.log(err),
        complete: () =>
          console.log(
            `update success - id: ${todo.Id} - IsComplete: ${todo.IsComplete}`
          ),
      });
  }
  modifyCompletedStatus(id: string, isComplete: boolean): void {
    this.http
      .put<Todo>(`${this.apiUrl}/${id}/${isComplete}`, this.httpOptions)
      .subscribe({
        error: (err) =>
          console.error(`Change complete status error:  + ${{ err }}`),
        complete: () => console.log(`update success status: ${isComplete}`),
      });
  }

  deleteTodoItem(id: string) {
    this.http
      .delete<Todo>(`${this.apiUrl}/delete/?id=${id}`, this.httpOptions)
      .subscribe({
        error: (err) => console.log(`Error on Delete Item ${err}`),
        complete: () => {
          console.log(`delete Done on Id: ${id}`);
        },
      });
  }
}
