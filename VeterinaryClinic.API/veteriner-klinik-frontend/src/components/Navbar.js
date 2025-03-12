import { Link } from 'react-router-dom';
import './Navbar.css';

function Navbar() {
  return (
    <nav className="navbar">
      <div className="nav-brand">
        <img src="/logo.png" alt="Veteriner Kliniği" className="nav-logo" />
        <h1>Veteriner Kliniği</h1>
      </div>
      <ul className="nav-links">
        <li><Link to="/">Ana Sayfa</Link></li>
        <li><Link to="/owners">Hayvan Sahipleri</Link></li>
        <li><Link to="/pets">Hayvanlar</Link></li>
        <li><Link to="/appointments">Randevular</Link></li>
        <li><Link to="/staff">Personel</Link></li>
      </ul>
    </nav>
  );
}

export default Navbar;