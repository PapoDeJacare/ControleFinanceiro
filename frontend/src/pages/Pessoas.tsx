import { useEffect, useState } from 'react'
import { cadastrarPessoa, getPessoasComTotal } from '../services/api';
import "./styles.css"

function Pessoas(){
    const [pessoas, setPessoas] = useState<any[]>([]);
    const [totalGeral, setTotalGeral] = useState<any>(null);
    const [mostrarModal, setMostrarModal] = useState(false);
    const [nome, setNome] = useState("");
    const [idade, setIdade] = useState(0);
    
    useEffect(() => {
        carregarPessoasComTotais();
    }, []);
    
    async function carregarPessoasComTotais() {
        const data = await getPessoasComTotal();
        setPessoas(data.pessoas);
        setTotalGeral(data.totalGeral)
    }

    function modalCadastro() {
        setMostrarModal(true);
    }

    function limparFormulario(){
        setNome("");
        setIdade(0);
    }

    async function salvarPessoa() {
        const pessoa = {
            nome: nome,
            idade: idade
        };
        
        await cadastrarPessoa(pessoa);
        limparFormulario()
        setMostrarModal(false);
        carregarPessoasComTotais();
    }
    return(
    <div>
        <div>
            <h1>Pessoas</h1>
            <button onClick={modalCadastro}>Cadastrar</button>
        </div>
        {mostrarModal && (
            <div className='modal'>
                <h2>Cadastrar Pessoa</h2>
                <div className='inputs'>
                    <input placeholder='Nome' onChange={(e) => setNome(e.target.value)}/>
                    <input type="number" placeholder='Idade' onChange={(e) => setIdade(Number(e.target.value))} />
                </div>

                <br />
            <div className='botoes'>
                <button onClick={() => {limparFormulario(); setMostrarModal(false)}}>Fechar</button>
                <button onClick={salvarPessoa}>Salvar</button>
            </div>
            </div>
        )}
        
        <div>
            <div>
                <div className='header'>
                    <div className='espacamento'> Nome </div>
                    <div className='espacamento'> Idade </div>
                    <div className='espacamento'> Receitas </div>
                    <div className='espacamento'> Despesas </div>
                    <div className='espacamento'> Saldo </div>
                </div>
                {pessoas.map((p) => (
                    <div key={p.id} className='linha'>
                        <div className='espacamento'>{p.nome}</div> 
                        <div className='espacamento'>{p.idade}</div>
                        <div className='espacamento'>R${p.totalReceitas}</div>
                        <div className='espacamento'>R${p.totalDespesas}</div>
                        <div className='espacamento'>R${p.saldo}</div>
                    </div>
                ))}
                {totalGeral && (
                    <div className='linha'>
                        <div className='espacamento-total'><b>Total Geral</b></div>
                        <div className='espacamento'><b>R${totalGeral.totalReceitas}</b></div>
                        <div className='espacamento'><b>R${totalGeral.totalDespesas}</b></div>
                        <div className='espacamento'><b>R${totalGeral.saldo}</b></div>
                    </div>
                )}
            </div>
        </div>
    </div>
    )
}

export default Pessoas