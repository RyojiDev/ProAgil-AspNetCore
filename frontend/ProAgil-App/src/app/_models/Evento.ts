import { Lote } from './Lote';
import { Palestrante } from './Palestrante';
import { RedeSocial } from './RedeSocial';

export interface Evento {

    id: number   
    dataEvento: Date;
    tema: string; 
    qtdPessoas : number; 
    local: string;  
    imagemUrl: string;  
    telefone: string;  
    email: string;  
    lotes: Lote[];
    redeSocial: RedeSocial[]; 
    palestranteEvento: Palestrante[]; 
    
}
