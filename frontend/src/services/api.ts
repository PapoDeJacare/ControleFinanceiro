
const API_URL = "http://localhost:5249/api";

export async function getPessoas() {
    const response = await fetch(`${API_URL}/pessoas`);
    return response.json();
}

export async function cadastrarPessoa(pessoa: any){
    const response = await fetch(`${API_URL}/pessoas`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(pessoa)
    });

    return response;
}

export async function getCategorias() {
    const response = await fetch(`${API_URL}/categorias`);
    return response.json();
}

export async function cadastrarCategoria(categoria: any) {
    const response =  await fetch(`${API_URL}/categorias`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(categoria)
    });

    return response;
}

export async function getTransacoes(){
    const response = await fetch(`${API_URL}/transacoes`);
    return response.json();
}

export async function cadastrarTransacao(transacao: any) {
    const response = await fetch(`${API_URL}/transacoes`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(transacao)
    });

    return response;
}

export async function getPessoasComTotal(){
    const response = await fetch(`${API_URL}/transacoes/totais-por-pessoa`);
    return response.json();
}

export async function getCategoriasComTotal() {
    const response = await fetch(`${API_URL}/transacoes/totais-por-categoria`);
    return response.json();
}