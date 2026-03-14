import { useEffect, useState } from "react"
import { cadastrarCategoria, getCategoriasComTotal } from "../services/api";
import "./styles.css"


function Categorias(){
    const [categorias, setCategorias] = useState<any[]>([]);
    const [totalGeral, setTotalGeral] = useState<any>(null);
    const [mostrarModal, setMostrarModal] = useState(false);
    const [finalidade, setFinalidade] = useState("");
    const [descricao, setDescricao] = useState("");

    useEffect(() => {
        carregarCategoriasComTotal();
    }, []);
    

    async function carregarCategoriasComTotal(){
        const data = await getCategoriasComTotal();
        setCategorias(data.categorias);
        setTotalGeral(data.totalGeral);
    }

    function modalCadastro(){
        setMostrarModal(true)
    }

    function limparFormulario(){
        setFinalidade("");
        setDescricao("");
    }

    async function salvarCategoria(){
        const categoria = {
            descricao: descricao,
            finalidade: finalidade
        };

        await cadastrarCategoria(categoria);
        limparFormulario();
        setMostrarModal(false);
        carregarCategoriasComTotal();
    }

    return (
        <div>
            <div>
                <h1>Categorias</h1>
                <button onClick={modalCadastro}>Cadastrar</button>
            </div>
            {mostrarModal && (
                <div className="modal">
                    <h2>Cadastrar Categoria</h2>
                    <div className="inputs">
                        <input placeholder='Descrição' onChange={(e) => setDescricao(e.target.value)}/>
                        <select value={finalidade} onChange={(e) => setFinalidade(e.target.value)}>
                            <option value="">Selecione o tipo de categoria</option>
                            <option value="despesa">Despesa</option>
                            <option value="receita">Receita</option>
                        </select>
                    </div>
                    <br />
                    <div className="botoes">
                        <button onClick={() => { limparFormulario(); setMostrarModal(false)}}>Fechar</button>
                        <button onClick={salvarCategoria}>Salvar</button>
                    </div>
                </div>
            )}
            <div>
                <div>
                    <div className="header">
                        <div className="espacamento"> Descrição </div>
                        <div className="espacamento"> Finalidade </div>
                        <div className="espacamento"> Receitas </div>
                        <div className="espacamento"> Despesas </div>
                        <div className="espacamento"> Saldo </div>
                    </div>
                    {categorias.map((c) => (
                        <div key={c.id} className="linha">
                            <div className="espacamento">{c.descricao}</div> 
                            <div className="espacamento">{c.finalidade}</div>
                            <div className="espacamento">R${c.totalReceitas}</div>
                            <div className="espacamento">R${c.totalDespesas}</div>
                            <div className="espacamento">R${c.saldo}</div>
                        </div>
                    ))}
                    {totalGeral && (
                        <div className='linha'>
                            <div className="espacamento-total"><b>Total Geral</b></div>
                            <div className="espacamento"><b>R${totalGeral.totalReceitas}</b></div>
                            <div className="espacamento"><b>R${totalGeral.totalDespesas}</b></div>
                            <div className="espacamento"><b>R${totalGeral.saldo}</b></div>
                        </div>
                    )}
                </div>
            </div>
        </div>
    )
}

export default Categorias