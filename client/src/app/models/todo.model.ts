export interface Todo {
  id: number;
  title: string;
  description?: string;
  isCompleted: boolean;
  createdAt: Date;
  updatedAt: Date;
}

export interface CreateTodoDto {
  title: string;
  description?: string;
}

export interface UpdateTodoDto {
  id: number;
  title?: string;
  description?: string;
  isCompleted?: boolean;
}
