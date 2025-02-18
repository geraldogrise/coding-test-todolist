import { Entity } from "./Entity";

export class TaskUserModel extends Entity
{
    id? = 0;
    id_user = 0;
    name = "";
    description = "";
    registrationDate  = new Date();
    endDate? =  new Date();
    login = "";
    user = "";
    email = "";

}