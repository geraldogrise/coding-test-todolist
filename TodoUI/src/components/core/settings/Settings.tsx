import "./settings.css";
import React from 'react';
import { jwtDecode } from "jwt-decode";
import { useNavigate } from 'react-router-dom';
import { MessageType } from "../message/MessageType";
import LoginService from '../../../services/LoginService';
import { useGlobalContext } from '../../../providers/GlobalProvider';
import LocalStorageService from '../../../services/LocalStorageService';



const Settings = () => {
  const { OpenMessage, GetNomeUsuario } = useGlobalContext();
  const navigate = useNavigate();
  const logout = async () => 
  {
   
      try {
        const loginSrrvice = new LoginService();
        await loginSrrvice.deslogar();
        LocalStorageService.clear();
        navigate("/login");
      } catch (error) {
        OpenMessage(MessageType.Error, (error as any).data.message);
      }
  };    

  const obterNomeUsuario = () => 
  { 
       const token = LocalStorageService.getToken();
       let name = GetNomeUsuario();
       if(name === "") {
          if(token)
          {
            const decoded = jwtDecode<{ name: string; unique_name: string}>(token);
            name = decoded.name;
          }
           
       }
       return name;
  };    

  
  return (
    <div className="dropdown">
        <button
            className="btn btn-primary btn-drop dropdown-toggle custom-dropdown"
            type="button"
            id="dropdownMenuButton"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
           {obterNomeUsuario()}
        </button>
        <ul className="dropdown-menu custom-dropdown-menu" aria-labelledby="dropdownMenuButton">
            <li><a className="dropdown-item" href="#settings">Configurações</a></li>
            <li>
              <a 
                onClick={(e) => {
                  e.preventDefault(); 
                  logout(); 
                }} 
                className="dropdown-item" 
                href="#logout">Sair
             </a>
            </li>
        </ul>
    </div>
  );
};

export default Settings;
