export class Aluno {
    constructor() {
        this.id = 0;
        this.nome = '';
        this.sobrenome = '';
        this.telefone = '';
    }
    
    id!: number;
    nome?: string;
    sobrenome?: string;
    telefone?: string;
}
