import React from 'react';

function Dashboard() {
  return (
    <div className="dashboard">
      <div className="stats-container">
        <div className="stats-card">
          <h3>Toplam Hasta</h3>
          <p className="stats-number">150</p>
        </div>
        <div className="stats-card">
          <h3>Günlük Randevu</h3>
          <p className="stats-number">12</p>
        </div>
        <div className="stats-card">
          <h3>Aktif Personel</h3>
          <p className="stats-number">8</p>
        </div>
        <div className="stats-card">
          <h3>Bekleyen Randevu</h3>
          <p className="stats-number">5</p>
        </div>
      </div>
    </div>
  );
}

export default Dashboard;