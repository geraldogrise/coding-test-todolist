import { useCallback, useEffect, useState } from "react";
import Header from "../Header/Header";
import { useGlobalContext } from "../../providers/GlobalProvider";
import TaskService from "../../services/TaskService";
import { TaskUserModel } from "../../models/TaskUserModel";
import { MessageType } from "../../components/core/message/MessageType";
import Card from "../../components/core/card/Card.";


const Views: React.FC<any> = () => {
     const { OpenMessage } = useGlobalContext();
     const [tasks, setTasks] = useState<TaskUserModel[]>(new Array<TaskUserModel>());
     const carregarTarefas = useCallback(async () => {
        try {
            const taskService = new TaskService();
            const response = await taskService.listarTarefas();
            const tarefas = (response as any).data.data as TaskUserModel[];
            setTasks(tarefas);

        } catch (error) {
            OpenMessage(MessageType.Error, "Erro ao carregar a tarefas");
        }
    }, [OpenMessage]);

    useEffect(() => {
        const fetchData = async () => {
            await carregarTarefas();
        };
    
        fetchData();
    }, [carregarTarefas]);
      
    return (
        <div>
             <Header></Header>
             <div className="row">
                 {tasks.map((card, index) => (
                    <div key={index} className="col-sm-3">
                         <Card title={card.name} 
                               description={card.description} 
                               name={card.user}
                               email={card.email}
                          />
                    </div>
                 ))}
             </div>
             
        </div>

    )
};
export default Views;