import "./tasks.css";
import Header from "../Header/Header";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Combo } from "../../components/models/Combo";
import Title from "../../components/core/title/title";
import Table from "../../components/core/table/Table";
import { TaskModel } from "../../models/TaskModel";
import Button from "../../components/core/button/Button";
import Search from "../../components/core/search/Search";
import TaskService from "../../services/TaskService";
import { TableData } from "../../components/models/TableData";
import Dropdown from "../../components/core/dropdown/Dropdown";
import { TableColumns } from "../../components/models/TableColumns";
import { TableActions } from "../../components/models/TableActions";
import LocalStorageService from "../../services/LocalStorageService";
import { MessageType } from "../../components/core/message/MessageType";
import { useGlobalContext } from "../../providers/GlobalProvider";

const Atividades: React.FC<any> = () => {
  const navigate = useNavigate();
  const size = 10;
  const { OpenMessage } = useGlobalContext();
  const [page, setPage] = useState<number>(0);
  const [totalPages, setTotalPages] = useState<number>(0);
  const [totalResults, setTotalResults] = useState<number>(0);
  const [search, setSearch] = useState<string | number>("");
  const [tasks, setTasks] = useState<TaskModel[]>([]);


  useEffect(() => {
    if (!LocalStorageService.getToken()) {
      navigate("/login");
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleSearchChange = (value: string | number) => {
      setSearch(value);

  };

  const options: Combo[] = [new Combo("nome", "Nome")];

  const [selectedItem, setSelectedItem] = useState<Combo>(options[0]);

  const handleSelect = (item: Combo) => {
    setSelectedItem(item);
  };

  const columns: TableColumns[] = [
    new TableColumns("name", "Nome", "left"),
  ];

  const actions: TableActions[] = [
    new TableActions("edit", "Editar", "task", null, "Editar atividade"),
    new TableActions("delete", "Remover", "task", null, "Remover atividade"),
   ];

  const carregarAtividades = async () => {
    try {
         let response: any = [];
         const taskService = new TaskService();
           response = await taskService.listarAtividadesPorUsuario(page, size);
          const tasks = (response as any).data.data as TaskModel[];
          let pages = Math.round(tasks.length/10);
          let totalPages = pages === 0 ? 1 : page;
          setTasks(tasks);
          setTotalPages(totalPages);
          setTotalResults(tasks.length);
       } catch (error) {
          console.log(error);
          OpenMessage(MessageType.Error, "Houve um erro ao carregar atividades");
       } finally {
       }
  };

 

  useEffect(() => {
    carregarAtividades();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [page]);
 

  useEffect(() => {

  }, [totalPages, totalResults]);

  const data: TableData = new TableData(
    1,
    1,
    tasks,
    actions,
    "id",
    "Post",
  );

  const buscar = () => {
    carregarAtividades();
  };

  const adicionar = () => {
    navigate("/task");
  };

  const voltar = () => {
    navigate("../home");
  };

  const onChangePage = (page: number) =>
  {
      setPage(page - 1);
  }

  const reload = async() =>
  {
      await carregarAtividades();
  }

 

  return (
    <>
      <Header />
      <div className="container container-user">
        <div className="padding-container">
          <Title value="Lista de Atividades" />
          <div className="row pt-3">
            <div className="filter">
              <Search text={search} onChange={handleSearchChange} />
              <div className="filter-dropdown">
                <Dropdown
                  selected={selectedItem}
                  onSelect={handleSelect}
                  data={options}
                ></Dropdown>
              </div>
              <div className="d-flex justify-content-between ps-4 align-items-center h-100">
                <Button
                  text="Buscar"
                  disabled={false}
                  classe="btn-primary button-container"
                  onClick={buscar}
                  
                />
                <Button
                  text="Novo"
                  disabled={false}
                  classe="btn-outline-primary button-container-outline"
                  onClick={adicionar}
                />
              </div>
            </div>
          </div>

          <Table 
             columns={columns} 
             data={data} 
             totalResults={totalResults}  
             pages={totalPages}
             onPage ={onChangePage}
             onReload = {reload}
            />

          <hr />
          <div className="d-flex justify-content-end mb-3">
            <Button
              text="Fechar"
              disabled={false}
              classe="btn-outline-secondary btn-standard-size "
              onClick={voltar}
            />
          </div>
        </div>
      </div>
    </>
  );
};
export default Atividades;
