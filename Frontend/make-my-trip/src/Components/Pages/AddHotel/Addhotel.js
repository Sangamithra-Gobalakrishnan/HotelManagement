import React, { useState } from "react";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { Carousel } from "react-responsive-carousel";
import { MdOutlineTravelExplore } from "react-icons/md";
import { AiFillCloseCircle } from "react-icons/ai";
import { TbGridDots } from "react-icons/tb";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import './Addhotel.css';

import img1 from './img1.jpg';
import img2 from './img2.jpg';
import img3 from './img3.jpg';
import img4 from './img4.jpg';
import { toast } from "react-toastify";

const AddHotel = ({ onClose }) => {
  const [hotelData, setHotelData] = useState({
    agentId: 0,
    name: "",
    description: "",
    email:"",
    address:"",
    contactNumber:"",
    city:"",
    country: "",
    numberOfRooms: "",
    minimumPriceRange: 0,
    maximumPriceRange: 0
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setHotelData((prevData) => ({
      ...prevData,
      [name]: value
    }));
  };

  const [active, setActive] = useState('navBar');

  const showNav = () => {
    setActive('navBar activeNavbar');
  };

  const removeNavbar = () => {
    setActive('navBar');
  };
  
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      var agent = Number(sessionStorage.getItem("userId"));
      hotelData.agentId = agent;
      const response = await fetch("http://localhost:5252/api/Hotel/AddHotelInformation", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(hotelData),
      });
      
      if (response.status === 200) {
        toast.success('Hotel added successfully.');
      } else {
        toast.error('Failed to add hotel.');
      }
    } catch (error) {
      console.error("Error sending data:", error);
    }
   
  };
  
  return (
    <section className="navBarSection">
      <header className="header flex">
        <div className="logoDiv">
          <a href="#" className="logo flex">
            <h1><MdOutlineTravelExplore className="icon"/> Mithra Majesty.</h1>
          </a>
        </div>

        <div className={active}>
          <ul className="navLists flex">
            <li className="navItem">
              <a href="/agentPage" className="navLink">Hotels</a>
            </li>
            <li className="navItem">
              <a href="/addHotel" className="navLink">Add Hotel</a>
            </li>
            <button className="btn">
              <a href="/">LOGOUT</a>
            </button>
          </ul>

          <div onClick={removeNavbar} className="closeNavbar">
            <AiFillCloseCircle className="icon"/>
          </div>
        </div>

        <div onClick={showNav} className="toggleNavbar">
          <TbGridDots className="icon"/>
        </div>
      </header>
      <section className="container-reg">
        <div className="left-section">
          <header>Add a Hotel</header>
          <div action="#" className="form-reg">
            <div className="column-reg">
              <div className="input-box-reg">
                <label>Hotel name</label>
                <input
                  type="text"
                  placeholder="Hotel name"
                  name="name"
                  value={hotelData.name}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="input-box-reg">
                <label>Email Address</label>
                <input
                  type="text"
                  placeholder="Email address"
                  name="email"
                  value={hotelData.email}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
            <div className="column-reg">
              <div className="input-box-reg">
                <label>Description</label>
                <input
                  type="text"
                  placeholder="Description"
                  name="description"
                  value={hotelData.description}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="input-box-reg">
                <label>Address</label>
                <input
                  type="text"
                  placeholder="Address"
                  name="address"
                  value={hotelData.address}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
            <div className="column-reg">
              <div className="input-box-reg">
                <label>Contact</label>
                <input
                  type="text"
                  placeholder="Contact Number"
                  name="contactNumber"
                  value={hotelData.contactNumber}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="input-box-reg">
                <label>Country</label>
                <input
                  type="text"
                  placeholder="Country"
                  name="country"
                  value={hotelData.country}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
            <div className="column-reg">
              <div className="input-box-reg">
                <label>Number of rooms</label>
                <input
                  type="number"
                  placeholder="Number of rooms"
                  name="numberOfRooms"
                  value={hotelData.numberOfRooms}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="input-box-reg">
                <label>City</label>
                <input
                  type="text"
                  placeholder="City"
                  name="city"
                  value={hotelData.city}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
            <div className="column-reg">
              <div className="input-box-reg">
                <label>Minimum price range</label>
                <input
                  type="number"
                  placeholder="Minimum price"
                  name="minimumPriceRange"
                  value={hotelData.minimumPriceRange}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="input-box-reg">
                <label>Maximum price range</label>
                <input
                  type="number"
                  placeholder="Maximum price"
                  name="maximumPriceRange"
                  value={hotelData.maximumPriceRange}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
            <button className="btn" onClick={handleSubmit}>
              Submit
            </button>
          </div>
        </div>
        <div className="right-section">
          <Carousel showThumbs={false}>
            <div>
              <img src={img1} alt="Image 1" />
            </div>
            <div>
              <img src={img2} alt="Image 2" />
            </div>
            <div>
              <img src={img3} alt="Image 3" />
            </div>
            <div>
              <img src={img4} alt="Image 4" />
            </div>
          </Carousel>
        </div>
      </section>
    </section>
  );
};

export default AddHotel;
