import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Navbar from './Components/Common/Navbar/Navbar';
import Login from './Components/Pages/Login/Login';
import Hotel from './Components/Pages/Hotel/Hotel';
import Roomdetails from './Components/Pages/Roomdetails/Roomdetails';
import Portfolio from './Components/Pages/Portfolio/Portfolio';
import Footer from './Components/Common/Footer/Footer';
import Approveagents from './Components/Pages/Approveagents/Approveagents';
import Approvedagents from './Components/Pages/Approvedagents/Approvedagents';
import Adminpage from './Components/Pages/Adminpage/adminpage';
import Travellerpage from './Components/Pages/Travellerpage/Travellerpage';
import Agentpage from './Components/Pages/Agentpage/Agentpage';
import Agentroomdetails from './Components/Pages/Agentroomdetails/Agentroomdetails';
import AddHotel from './Components/Pages/AddHotel/Addhotel';
import Gallery from './Components/Pages/Gallery/Gallery';
import BookedRooms from './Components/Pages/BookedRooms/BookedRooms';
import AdminProtected from './Components/ProtectedRoutes/AdminProtected';
import AgentProtected from './Components/ProtectedRoutes/AgentProtected';
import TravellerProtected from './Components/ProtectedRoutes/TravellerProtected';
import './App.css';

function App() {

  var token;

  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Navbar />} />
          <Route path="/login" element = {<Login/>}/>
          <Route path="/hotels" element = {<Hotel/>}/>
          <Route path="/roomDetails/:id" element = {<Roomdetails/>}/>
          <Route path="/portfolio" element = {<Portfolio/>}/>
          <Route path="/footer" element = {<Footer/>}/>
          <Route path="/approveAgents" element = {<Approveagents/>}/>
          <Route path="/approvedAgents" element = {<Approvedagents/>}/>
          <Route path="/agentRoomDetails/:id" element = {<Agentroomdetails/>}/>
          <Route path="/addHotel" element = {<AddHotel/>}/>
          <Route path="/gallery" element = {<Gallery/>}/>
          <Route path="/bookedRooms" element = {<BookedRooms/>}/>
          <Route path='/adminPage' element={
          <AdminProtected token={token}>
            <Adminpage/>
          </AdminProtected>
        } />
         <Route path='/travellerPage' element={
          <TravellerProtected token={token}>
            <Travellerpage/>
          </TravellerProtected>
        } />
         <Route path='/agentPage' element={
          <AgentProtected token={token}>
            <Agentpage/>
          </AgentProtected>
        } />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
