import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Capacitor } from '@capacitor/core';
import { environment } from '../../environments/environment';
import { Todo, CreateTodoDto, UpdateTodoDto } from '../models/todo.model';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    // Dynamically set API URL based on platform
    if (Capacitor.isNativePlatform()) {
      // Android emulator uses 10.0.2.2 to access host machine localhost
      // Physical devices use LAN IP
      if (Capacitor.getPlatform() === 'android') {
        this.apiUrl = 'http://10.0.2.2:8080/api/todos';
      } else {
        // iOS simulator and physical devices use LAN IP
        this.apiUrl = 'http://192.168.1.91:8080/api/todos';
      }
    } else {
      // Web uses localhost
      this.apiUrl = `${environment.apiUrl}/todos`;
    }
    
    console.log('TodoService initialized with API URL:', this.apiUrl);
    console.log('Platform:', Capacitor.getPlatform());
    console.log('Is Native:', Capacitor.isNativePlatform());
  }

  getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.apiUrl);
  }

  getTodo(id: number): Observable<Todo> {
    return this.http.get<Todo>(`${this.apiUrl}/${id}`);
  }

  createTodo(todo: CreateTodoDto): Observable<Todo> {
    return this.http.post<Todo>(this.apiUrl, todo);
  }

  updateTodo(todo: UpdateTodoDto): Observable<Todo> {
    return this.http.put<Todo>(`${this.apiUrl}/${todo.id}`, todo);
  }

  deleteTodo(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  toggleComplete(id: number, isCompleted: boolean): Observable<Todo> {
    return this.http.put<Todo>(`${this.apiUrl}/${id}`, { id, isCompleted });
  }
}
