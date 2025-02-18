import BaseService from "./core/BaseService";
import { LoginModel } from "../models/LoginModel";

class LoginService extends BaseService {
    logar = async (login: LoginModel) => {
       return await this.Post(login, "auth/signin");
    };

}
export default LoginService;