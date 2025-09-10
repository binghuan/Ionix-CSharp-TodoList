import { Component, OnInit } from '@angular/core';
import { AlertController, LoadingController, ToastController } from '@ionic/angular';
import { TodoService } from '../../services/todo.service';
import { Todo, CreateTodoDto } from '../../models/todo.model';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.page.html',
  styleUrls: ['./todo.page.scss'],
})
export class TodoPage implements OnInit {
  todos: Todo[] = [];
  loading = false;

  constructor(
    private todoService: TodoService,
    private alertController: AlertController,
    private loadingController: LoadingController,
    private toastController: ToastController
  ) {}

  ngOnInit() {
    this.loadTodos();
  }

  async loadTodos() {
    this.loading = true;
    try {
      this.todos = await this.todoService.getTodos().toPromise() || [];
    } catch (error) {
      console.error('Error loading todos:', error);
      this.showToast('Error loading todos', 'danger');
    } finally {
      this.loading = false;
    }
  }

  async showAddTodoDialog() {
    const alert = await this.alertController.create({
      header: 'Add Todo',
      inputs: [
        {
          name: 'title',
          type: 'text',
          placeholder: 'Enter todo title'
        },
        {
          name: 'description',
          type: 'textarea',
          placeholder: 'Enter description (optional)'
        }
      ],
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel'
        },
        {
          text: 'Add',
          handler: async (data) => {
            if (data.title?.trim()) {
              await this.addTodo({
                title: data.title.trim(),
                description: data.description?.trim() || undefined
              });
            }
          }
        }
      ]
    });

    await alert.present();
  }

  async addTodo(createTodo: CreateTodoDto) {
    const loading = await this.loadingController.create({
      message: 'Adding todo...'
    });
    await loading.present();

    try {
      const newTodo = await this.todoService.createTodo(createTodo).toPromise();
      if (newTodo) {
        this.todos.unshift(newTodo);
        this.showToast('Todo added successfully', 'success');
      }
    } catch (error) {
      console.error('Error adding todo:', error);
      this.showToast('Error adding todo', 'danger');
    } finally {
      await loading.dismiss();
    }
  }

  async showEditTodoDialog(todo: Todo) {
    const alert = await this.alertController.create({
      header: 'Edit Todo',
      inputs: [
        {
          name: 'title',
          type: 'text',
          placeholder: 'Enter todo title',
          value: todo.title
        },
        {
          name: 'description',
          type: 'textarea',
          placeholder: 'Enter description (optional)',
          value: todo.description || ''
        }
      ],
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel'
        },
        {
          text: 'Update',
          handler: async (data) => {
            if (data.title?.trim()) {
              await this.updateTodo({
                id: todo.id,
                title: data.title.trim(),
                description: data.description?.trim() || undefined
              });
            }
          }
        }
      ]
    });

    await alert.present();
  }

  async updateTodo(updateTodo: any) {
    const loading = await this.loadingController.create({
      message: 'Updating todo...'
    });
    await loading.present();

    try {
      const updatedTodo = await this.todoService.updateTodo(updateTodo).toPromise();
      if (updatedTodo) {
        const index = this.todos.findIndex(t => t.id === updatedTodo.id);
        if (index !== -1) {
          this.todos[index] = updatedTodo;
        }
        this.showToast('Todo updated successfully', 'success');
      }
    } catch (error) {
      console.error('Error updating todo:', error);
      this.showToast('Error updating todo', 'danger');
    } finally {
      await loading.dismiss();
    }
  }

  async toggleComplete(todo: Todo) {
    try {
      const updatedTodo = await this.todoService.toggleComplete(todo.id, !todo.isCompleted).toPromise();
      if (updatedTodo) {
        const index = this.todos.findIndex(t => t.id === updatedTodo.id);
        if (index !== -1) {
          this.todos[index] = updatedTodo;
        }
      }
    } catch (error) {
      console.error('Error toggling todo:', error);
      this.showToast('Error updating todo', 'danger');
    }
  }

  async showDeleteConfirm(todo: Todo) {
    const alert = await this.alertController.create({
      header: 'Confirm Delete',
      message: `Are you sure you want to delete "${todo.title}"?`,
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel'
        },
        {
          text: 'Delete',
          handler: () => {
            this.deleteTodo(todo.id);
          }
        }
      ]
    });

    await alert.present();
  }

  async deleteTodo(id: number) {
    const loading = await this.loadingController.create({
      message: 'Deleting todo...'
    });
    await loading.present();

    try {
      await this.todoService.deleteTodo(id).toPromise();
      this.todos = this.todos.filter(t => t.id !== id);
      this.showToast('Todo deleted successfully', 'success');
    } catch (error) {
      console.error('Error deleting todo:', error);
      this.showToast('Error deleting todo', 'danger');
    } finally {
      await loading.dismiss();
    }
  }

  async showToast(message: string, color: 'success' | 'danger' | 'warning') {
    const toast = await this.toastController.create({
      message,
      duration: 3000,
      color,
      position: 'bottom'
    });
    await toast.present();
  }

  doRefresh(event: any) {
    this.loadTodos().finally(() => {
      event.target.complete();
    });
  }

  trackByTodoId(index: number, todo: Todo): number {
    return todo.id;
  }
}
