import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './App.css';
import Dashboard from './components/Dashboard';
import OwnerList from './components/OwnerList';
import PetsList from './components/PetsList';
import AppointmentsList from './components/AppointmentsList';
import StaffList from './components/StaffList';
import Navbar from './components/Navbar';

function App() {
  return (
    <Router>
      <div className="App">
        <Navbar />
        <main className="main-content">
          <Routes>
            <Route path="/" element={<Dashboard />} />
            <Route path="/owners" element={<OwnerList />} />
            <Route path="/pets" element={<PetsList />} />
            <Route path="/appointments" element={<AppointmentsList />} />
            <Route path="/staff" element={<StaffList />} />
          </Routes>
        </main>
      </div>
    </Router>
  );
}

export default App;