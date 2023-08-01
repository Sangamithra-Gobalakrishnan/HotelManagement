import React from 'react';
//import { BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import Navbar from './Components/Common/Navbar/Navbar';
import Home from './Components/Pages/Home/Home';
// import Booking from './Components/Pages/Booking/Booking';
// import Contact from './Components/Pages/Contact/Contact';
// import Feedback from './Components/Pages/Feedback/Feedback';
// import Gallery from './Components/Pages/Gallery/Gallery';
// import TourSpotDetails from './Components/Pages/Tourspotdetails/Tourspotdetails';
import './App.css';
import Portfolio from './Components/Pages/Portfolio/Portfolio';

function App() {
  return (
    <div>
      <Navbar/>
      <Home/>
      <Portfolio/>
    </div>
    // <Router>
    //   <div>
    //     <Navbar />
    //     <Routes>
    //       <Route exact path="/" component={Home} />
    //       <Route path="/contact" component={Contact} />
    //       <Route path="/feedback" component={Feedback} />
    //       <Route path="/booking" component={Booking} />
    //       <Route path="/gallery" component={Gallery} />
    //       <Route path="/tour/:id" component={TourSpotDetails} />
    //     </Routes>
    //   </div>
    // </Router>
  );
}


export default App;