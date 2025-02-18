import React from "react";
import {
  BrowserRouter as Router,
  Route,
  Routes,
} from 'react-router-dom';
/*--------------- componentes ---------------*/
import Home from './pages/home/Home';
import Login from './pages/login/Login';
import Usuario from './pages/usuarios/usuario';
import Usuarios from './pages/usuarios/Usuarios';
import Register from "./pages/register/Register";
import Atividade from "./pages/tasks/Atividade";
import Atividades from "./pages/tasks/Atividades";
import  Views  from "./pages/view/Views";


const renderLoader = () => {
  return <div>Carregando...</div>;
};

const Main = () => (
  <React.Suspense fallback={renderLoader()}>
    <Router>
      <Routes>
        <Route path="/" element={<Views />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/home" element={<Home />} />
        <Route path="/users" element={<Usuarios />} />
        <Route path="/user/:id?" element={<Usuario />} />
        <Route path="/tasks" element={<Atividades />} />
        <Route path="/task/:id?" element={<Atividade />} />
      </Routes>
    </Router>
  </React.Suspense>
);

export default Main;
