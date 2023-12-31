import { HttpHeaders } from '@angular/common/http';

export const ApiUrlTodoItems = 'https://localhost:7219/api/TodoItems';
export const ApiLogin = 'https://localhost:7219/api/User';
export const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Headers': 'Content-Type, Authorization',
    'Access-Control-Allow-Methods': '*',
  }),
};
