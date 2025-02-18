import "./task.css";
import { useCallback, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Header from "../Header/Header";
import Input from "../../components/core/input/Input";
import Title from "../../components/core/title/title";
import { TaskModel } from "../../models/TaskModel";
import Button from "../../components/core/button/Button";
import TaskService from "../../services/TaskService";
import { useGlobalContext } from "../../providers/GlobalProvider";
import { MessageType } from "../../components/core/message/MessageType";
import LocalStorageService from "../../services/LocalStorageService";
import { jwtDecode } from "jwt-decode";
import UsuarioService from "../../services/UsuarioService";
import { Combo } from "../../components/models/Combo";
import { UsuarioModel } from "../../models/UsuarioModel";
import Select from "../../components/core/select/Select";

const Atividade: React.FC<any> = () => {
    const [task, setTask] = useState<TaskModel>(new TaskModel());
    const { id } = useParams<{ id?: string }>();
    const { OpenMessage } = useGlobalContext();
    const navigate = useNavigate();
    const [options, setOptions] = useState<Combo[]>(new Array<Combo>());

    useEffect(() => {
        if (!LocalStorageService.getToken()) {
            navigate('/login');
        }
    }, [navigate]);

    const loadTask = useCallback((response: any) => {
        setTask({
            id_user: response.id_user,
            name: response.name,
            description: response.description,
            registrationDate: new Date(response.registrationDate),
            endDate: new Date(response.endDate)
        });
    }, []);

    const carregarTask = useCallback(async (id: number) => {
        try {
            const taskService = new TaskService();
            const response = await taskService.obterAtividade(id);
            console.log((response as any).data.data);
            loadTask((response as any).data.data);
        } catch (error) {
            OpenMessage(MessageType.Error, "Erro ao carregar a atividade");
        }
    }, [loadTask, OpenMessage]);

    const carregarUsuario = useCallback(() => {
        const token = LocalStorageService.getToken();
        if (token) {
            const decoded = jwtDecode<{ name: string; unique_name: string }>(token);
            setTask(prevPost => ({
                ...prevPost,
                id_user: Number(decoded.unique_name)
            }));
        }
    }, []);

    const carregarComboUsuarios = useCallback(async () => {
        try {
            const usuarioService = new UsuarioService();
            const response = await usuarioService.listarUsuarios();
            const users = (response as any).data.data as UsuarioModel[];
            const options: Combo[] = users.map(item => new Combo(item.id? item.id : 0, item.name));
            setOptions(options);
        } catch (error) {
            OpenMessage(MessageType.Error, "Erro ao carregar a usuários");
        }
    }, [OpenMessage]);


    useEffect(() => {
        if (id) {
            carregarTask(Number(id));
        }
        carregarUsuario();
        carregarComboUsuarios();
    }, [id, carregarTask, carregarUsuario, carregarComboUsuarios]);



    const handleNome = (event: React.ChangeEvent<HTMLInputElement>) => {
        setTask(prevTask => ({
            ...prevTask,
            name: event.target.value
        }));
    };

    const handleDescription = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
        setTask(prevTask => ({
            ...prevTask,
            description: event.target.value
        }));
    };

    const handleRegistrationDate = (event: React.ChangeEvent<HTMLInputElement>) => {
        setTask(prevTask => ({
            ...prevTask,
            registrationDate: new Date(event.target.value)
        }));
    };

    const handleEndDate = (event: React.ChangeEvent<HTMLInputElement>) => {
        setTask(prevTask => ({
            ...prevTask,
            endDate: new Date(event.target.value) // Corrigido para "endDate"
        }));
    };

    
    const handleUsuario = (value: string | number) => {
        setTask({
            ...task,
            id_user: Number(value)
        });
    };

    const cancelar = () => {
        navigate("/tasks");
    };

    const salvar = async () => {
        const taskService = new TaskService();
      
        try {
            if (id !== undefined) {
                await taskService.editarAtividade(Number(id), task);
            } else {
                await taskService.inserirAtividade(task);
            }
            OpenMessage(MessageType.Success, "Registro inserido com sucesso");
            navigate("/tasks");
        } catch (error) {
            OpenMessage(MessageType.Error, (error as any).data.message);
        }
    };

    return (
        <>
            <Header />
            <div className="container container-user">
                <div className="padding-container">
                    <Title value="Cadastrar Atividade" />
                    <div className="row">
                        <div className="col-md-12">
                            <Input
                                label="Título"
                                type="text"
                                value={task.name}
                                placeholder="Título"
                                required
                                disabled={false}
                                maxlength={50}
                                onChange={handleNome}
                            />
                        </div>

                        <div className="col-md-12">
                                <Select
                                    options={options}
                                    value={task.id_user}
                                    onChange={handleUsuario}
                                    required={true}
                                    label="Usuários"
                                />
                            </div>

                        <div className="col-md-6">
                            <Input
                                label="Data de Início"
                                type="date"
                                value={task.registrationDate ? task.registrationDate.toISOString().split("T")[0] : ""}
                                placeholder="Data de Início"
                                required
                                disabled={false}
                                onChange={handleRegistrationDate}
                            />
                        </div>

                        <div className="col-md-6">
                            <Input
                                label="Data de Término"
                                type="date"
                                value={task.endDate ? task.endDate.toISOString().split("T")[0] : ""}
                                placeholder="Data de Término"
                                required
                                disabled={false}
                                onChange={handleEndDate}
                            />
                        </div>
                    </div>
                    <div className="col-md-12 mb-1 mt-1">
                            <label className="form-label">Texto</label>
                        </div>
                        <div className="col-md-12">
                            <textarea
                                rows={5}
                                maxLength={255}
                                className="form-control"
                                value={task.description}
                                onChange={handleDescription}
                            />
                        </div>

                    <hr />
                    <div className="mb-3 pt-3 d-flex justify-content-between">
                       <div className="justify-content-end">
                            <Button 
                                text="Voltar" 
                                disabled={false} 
                                classe="btn-outline-primary button-user button-user-link"
                                onClick={cancelar} 
                            />
                        </div>
                        <div className='justify-content-end'>
                           <Button 
                                text="Cancelar" 
                                disabled={false} 
                                classe="btn-outline-primary button-user button-user-link"
                                onClick={cancelar} 
                            />
                           <Button 
                                text="Salvar" 
                                disabled={false}
                                classe="btn-primary button-user"  
                                onClick={salvar} 
                            />
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default Atividade;