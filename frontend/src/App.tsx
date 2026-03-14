import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import Pessoas from "./pages/Pessoas";
import Categorias from "./pages/Categorias";
import Transacoes from "./pages/Transacoes";
import './App.css'

function App() {
  return (
    <div>
      <h1>Controle Financeiro</h1>
      <BrowserRouter>
        <nav>
          <Link to="/">Pessoas</Link> |{" "}
          <Link to="/categorias">Categorias</Link> |{" "}
          <Link to="/transacoes">Transações</Link>
        </nav>

        <Routes>
          <Route path="/" element={<Pessoas />} />
          <Route path="/categorias" element={<Categorias />} />
          <Route path="/transacoes" element={<Transacoes />} />
        </Routes>
      </BrowserRouter>
    </div>
      
      
    );
}

export default App
