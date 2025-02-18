import { TaskModel } from "../models/TaskModel";
import BaseService from "./core/BaseService";
import LocalStorageService from './/LocalStorageService';

class  TaskService extends BaseService {
    listarAtividades = async (page: number, size: number) => {
       return await this.Get("api/tasks");
    };

    listarAtividadesPorUsuario = async (page: number, size: number) => {
        const id = LocalStorageService.getUsuarioLogado();
        return await this.Get("api/users/"+id+"/tasks");
     };

    obterAtividade = async (id_post: number) => {
        return await this.Get("api/tasks/"+ id_post);
    };

    inserirAtividade = async (task: TaskModel) => {
        return await this.Post(task,"api/tasks");
    };

    editarAtividade = async (id:number, task: TaskModel) => {
        return await this.Put(id, task,"api/tasks");
    };

    DeletarAtividade = async (id:number) => {
        return await this.Delete(id,"api/tasks");
    };

    listarTarefas = async () => {
        return await this.Get("api/tasks/all");
    };
}

export default TaskService;