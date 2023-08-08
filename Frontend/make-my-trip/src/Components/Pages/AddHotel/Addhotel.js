import React,{useEffect,useState} from "react";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { Carousel } from "react-responsive-carousel";
import {MdOutlineTravelExplore} from "react-icons/md";
import {AiFillCloseCircle} from "react-icons/ai";
import {TbGridDots} from "react-icons/tb";
import './Addhotel.css';

import img1 from './img1.jpg';
import img2 from './img2.jpg';
import img3 from './img3.jpg';
import img4 from './img4.jpg';

const AddHotel = ({ onClose }) => {
  const [hotelData, setHotelData] = useState({
    name: "",
    description: "",
    country: "",
    state: "",
    city: "",
    numberOfRooms: "",
    minPrice: "",
    maxPrice: "",
    images: [],
    amenities: []
  });

  const [active, setActive] = useState('navBar');

  //Function to toggle Navbar
  const showNav  = () =>{
   setActive('navBar activeNavbar')
  }

  //Function to remove Navbar
  const removeNavbar = () =>{
    setActive('navBar')
  }

  const handleChange = (e) => {
    const { name, value } = e.target;
    setHotelData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleImageChange = (e) => {
    const files = Array.from(e.target.files);
    setHotelData((prevData) => ({
      ...prevData,
      images: files,
    }));
  };

  const handleAmenityChange = (e, index) => {
    const { value } = e.target;
    setHotelData((prevData) => {
      const newAmenities = [...prevData.amenities];
      newAmenities[index] = value;
      return {
        ...prevData,
        amenities: newAmenities,
      };
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
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
    <section class="container-reg">
         <div className="left-section">
         <header>Add a Hotel</header>  
                <div action="#" class="form-reg">
                    <div className="column-reg">
                        <div class="input-box-reg">
                            <label>Hotel name</label>
                            <input type="text" placeholder="Hotel name" required />
                        </div>
                        <div class="input-box-reg">
                            <label>Email Address</label>
                            <input type="text" placeholder="Email address" required />
                        </div>
                    </div>

                <div class="column-reg">
                    <div class="input-box-reg">
                        <label>Description</label>
                        <input type="text" placeholder="Description" required />
                    </div> 
                    <div class="input-box-reg">
                        <label>Address</label>
                        <input type="text" placeholder="Address" required />
                    </div>
                </div>
                    
                <div class="column-reg">
                    <div class="input-box-reg">
                        <label>Contact</label>
                        <input type="text" placeholder="Contact Number" required />
                    </div> 
                    <div class="input-box-reg">
                        <label>Country,State</label>
                        <input type="text" placeholder="Country,State" required />
                    </div>
                </div>

                <div class="column-reg">
                    <div class="input-box-reg">
                        <label>Number of rooms</label>
                        <input type="number" placeholder="Number of rooms" required />
                    </div> 
                    <div class="input-box-reg">
                        <label>Amenities</label>
                        <input type="text" placeholder="Amenity1,Amenity2" required />
                    </div>
                </div>

                <div class="column-reg">
                    <div class="input-box-reg">
                        <label>Price Range</label>
                        <input type="text" placeholder="Minimum,Maximum" required />
                    </div>
                    <div class="input-box-reg">
                        <label>Upload Image Here</label>
                        <input type="file" />
                    </div>
                </div>
                <button className="btn">Submit</button>
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
                <img src={img3} alt="Image 2" />
            </div>
            <div>
                <img src={img4} alt="Image 2" />
            </div>
            </Carousel>
      </div>
    </section>
 </section>
  );
};

export default AddHotel;
