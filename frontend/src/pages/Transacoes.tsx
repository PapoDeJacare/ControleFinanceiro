import { useEffect, useState } from "react"
import { cadastrarTransacao, getCategorias, getPessoas, getTransacoes } from "../services/api";
import "./styles.css"

function Transacoes(){
    const [mostrarModal, setMostrarModal] = useState(false);
    const [transacoes, setTransacoes] = useState<any[]>([]);
    const [pessoas, setPessoas] = useState<any[]>([]);
    const [pessoa, setPessoa] = useState<number | "">("");
    const [tipo, setTipo] = useState("");
    const [categorias, setCategorias] = useState<any[]>([]);
    const [categoria, setCategoria] = useState<number | "">("");
    const [descricao, setDescricao] = useState("");
    const [valor, setValor] = useState(0);
    const [erro, setErro] = useState("");

    useEffect(() => {
        listarTransacoes();
        listarPessoas();
        listarCategorias();
    }, []);

    async function listarTransacoes(){
        const data = await getTransacoes();
        setTransacoes(data);
    }

    async function listarPessoas() {
        const data = await getPessoas();
        setPessoas(data);
    }

    async function listarCategorias() {
        const data = await getCategorias();
        setCategorias(data);
    }

    function modalCadastro(){
        setMostrarModal(true);
    }

    function limparFormulario(){
        setPessoa("");
        setDescricao("");
        setValor(0);
        setTipo("");
        setCategoria("");
    }

    async function salvarTransacao(){
        const transacao = {
            descricao: descricao,
            valor: valor,
            tipo: tipo,
            pessoaId: pessoa,
            categoriaId: categoria
        }
        
        const response = await cadastrarTransacao(transacao);

        if(!response.ok){
            const erroTexto = await response.text();
            setErro(erroTexto);
            return;
        }

        setErro("");
        limparFormulario();
        setMostrarModal(false);
        listarTransacoes();
    }

    return(
        <div>
            <div>
                <h1>Transações</h1>
                <button onClick={modalCadastro}>Cadastrar</button>
            </div>
            {mostrarModal && (
                <div className="modal">
                    <div className="inputs">
                        <select value={pessoa} onChange={(e) => setPessoa(Number(e.target.value))}>
                            <option value="">Selecione uma pessoa</option>
                            {pessoas.map((p) => (
                                <option key={p.id} value={p.id}>
                                    {p.nome}
                                </option>
                            ))}
                        </select>
                        <input placeholder="Descrição" onChange={(e) => setDescricao(e.target.value)} />
                        <input type="decimal" placeholder="Valor" onChange={(e) => setValor(Number(e.target.value))} />
                        <select value={tipo} onChange={(e) => setTipo(e.target.value)}>
                            <option value="">Selecione o tipo de transação</option>
                            <option value="despesa">Despesa</option>
                            <option value="receita">Receita</option>
                        </select>
                        <select value={categoria} onChange={(e) => setCategoria(Number(e.target.value))}>
                            <option value="">Selecione uma categoria</option>
                            {categorias.map((c) => (
                                <option key={c.id} value={c.id}>
                                    {c.descricao}
                                </option>
                            ))}
                        </select>
                        {erro && (
                            <div style={{color: "red"}}>
                                {erro}
                            </div>
                        )}
                        <br />
                        <div className="botoes">
                            <button onClick={() => {limparFormulario(); setErro(""); setMostrarModal(false)}}>Fechar</button>
                            <button onClick={salvarTransacao}>Salvar</button>
                        </div>
                    </div>
                </div>
            )}
            <div>
                <div>
                    <div className="header">
                        <div className="espacamento-transacao"> Pessoa </div>
                        <div className="espacamento-transacao"> Descrição </div>
                        <div className="espacamento-transacao"> Valor </div>
                        <div className="espacamento-transacao"> Tipo </div>
                    </div>
                    {transacoes.map((t) => (
                        <div key={t.id} className="linha">
                            <div className="espacamento-transacao">{t.pessoa.nome}</div> 
                            <div className="espacamento-transacao">{t.descricao}</div>
                            <div className="espacamento-transacao">R${t.valor}</div>
                            <div className="espacamento-transacao">{t.tipo}</div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    )
}

export default Transacoes