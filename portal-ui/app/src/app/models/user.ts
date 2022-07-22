export class User {
    displayName: string;
    roles: string [];
    email: string;

    constructor(displayName: string, email: string, roles: string[]) {
        this.displayName = displayName;
        this.roles = roles;
        this.email = email;
    }

}