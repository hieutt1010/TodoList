import { Injectable } from '@angular/core';
import { Todo } from '../app/models/todo';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiUrlTodoItems } from '../app/models/commonVariable';
import { httpOptions } from '../app/models/commonVariable';
@Injectable({
  providedIn: 'root',
})
export class TodolistService {
  constructor(private http: HttpClient) {}

  apiUrl = ApiUrlTodoItems;
  httpOptions = httpOptions;

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

  modifyTodoItem(todo: Todo): Observable<Todo> {
    return this.http.put<Todo>(
      `${this.apiUrl}/update/`,
      todo,
      this.httpOptions
    );
  }
  modifyCompletedStatus(id: string, isComplete: boolean): Observable<Todo> {
    return this.http.put<Todo>(
      `${this.apiUrl}/${id}/${isComplete}`,
      this.httpOptions
    );
  }

  deleteTodoItem(id: string): Observable<any> {
    return this.http.delete<Todo>(
      `${this.apiUrl}/delete/?id=${id}`,
      this.httpOptions
    );
  }
  searchTodoItem(search: string): Observable<any> {
    return this.http.get<Todo[]>(
      `${this.apiUrl}/search/${search}`,
      this.httpOptions
    );
  }
}
