import React,{useEffect,useState} from "react";
import {MdOutlineTravelExplore} from "react-icons/md";
import {AiFillCloseCircle} from "react-icons/ai";
import {TbGridDots} from "react-icons/tb";
import { HiOutlineClipboardCheck, HiOutlineLocationMarker } from 'react-icons/hi';


import Aos from 'aos';
import 'aos/dist/aos.css';

import './Travellerpage.css';
import { useNavigate } from "react-router-dom";

import defaultHotelImage1 from './img3.jpg';
import defaultHotelImage2 from './img1.jpg';
import defaultHotelImage3 from './img2.jpg';
import defaultHotelImage4 from './img4.jpg';
import defaultHotelImage5 from './img5.jpg';


const Travellerpage = () => {

    const [hotels, setHotels] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [hotelsPerPage] = useState(6);
    const [hotelImages, setHotelImages] = useState([]);

    const fetchData = async () => {
        try {
            const response = await fetch(`http://localhost:5252/api/Hotel/FetchAllHotels?page=${currentPage}&perPage=${hotelsPerPage}`);
            const jsonData = await response.json();
            setHotels(jsonData);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    var navigate=useNavigate();

    const fetchHotelImages = async () => {
        const imagePromises = hotels.map(async (hotel) => {
            try {
                const response = await fetch(`http://localhost:5252/api/Image/FetchImagesByHotelId`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ hotelId: hotel.id })
                });
                const jsonData = await response.json();
                if (jsonData.length > 0) {
                    return jsonData[0].imageUrl;
                }
            } catch (error) {
                console.error('Error fetching hotel images:', error);
            }
            return null;
        });

        const imageUrls = await Promise.all(imagePromises);
        return imageUrls;
    };

    useEffect(() => {
        fetchData();
        fetchHotelImages().then((imageUrls) => {
            setHotelImages(imageUrls); 
        });
    }, [currentPage]);

    
    useEffect(() => {
        Aos.init({ duration: 2000 });
    }, []);

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    const totalPages = Math.ceil(hotels.length / hotelsPerPage);

    const pagination = (
       <div className="pagination-hotel">
         {Array.from({ length: totalPages }, (_, index) => (
        <button
            key={index}
            onClick={() => handlePageChange(index + 1)}
            className={currentPage === index + 1 ? 'active button' : 'button'}
        >
            {index + 1}
        </button>
        ))}
    </div>

    );

    const handleLogout = () => {
        sessionStorage.clear();
        navigate('/');
    };

   const [active, setActive] = useState('navBar');

   //Function to toggle Navbar
   const showNav  = () =>{
    setActive('navBar activeNavbar')
   }

   //Function to remove Navbar
   const removeNavbar = () =>{
     setActive('navBar')
   }

   const staticImages = [
    defaultHotelImage1,
    defaultHotelImage2,
    defaultHotelImage3,
    defaultHotelImage4,
    defaultHotelImage5
];

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
                         <a href="#" className="navLink">Book Hotel</a>
                       </li>
                       <li className="navItem">
                         <a href="/bookedRooms" className="navLink">Your Bookings</a>
                       </li>
                       <button className="btn">
                         <a onClick={handleLogout}>LOGOUT</a>
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

            <section className="main container-hotel section">
            <div className="secTitle">
                <h3 data-aos="fade-right" className="title">
                    Most visited Destinations
                </h3>
            </div>

            <div className="secContent grid" data-aos="fade-up">
            {hotels
                 .slice((currentPage - 1) * hotelsPerPage, currentPage * hotelsPerPage)
                 .map((hotel, index) => (
                     <div key={hotel.id} className="singleDestination">
                         <div className="imageDiv">
                             <img
                                 className="imgcs"
                                 src={hotelImages[index]?.amenityTypeOrImage || staticImages[index % staticImages.length]}
                                 alt="Hotel"
                             />
                        </div>

                            <div className="cardInfo">
                                <h4 className="desTitle">{hotel.name}</h4>
                                <span className="continent flex">
                                    <HiOutlineLocationMarker className="icon" />
                                    <span className="name">{hotel.country}</span>
                                </span>

                                <div className="fees flex">
                                    <div className="grade">
                                        <span>
                                            Number Of Rooms&nbsp;&nbsp;
                                            <small className="small">{hotel.numberOfRooms}</small>
                                        </span>
                                    </div>

                                    <div className="price">
                                    <h5>
                                        &#x20B9;{hotel.minimumPriceRange}-&#x20B9;{hotel.maximumPriceRange}
                                    </h5>
                                    </div>
                                </div>

                                <div className="desc">
                                    <p>{hotel.description}</p>
                                </div>

                                <button className="btn flex" onClick={()=>{navigate(`/roomDetails/${hotel.id}`)}}>
                                   DETAILS <HiOutlineClipboardCheck className="icon" />
                               </button>

                            </div>
                        </div>
                    ))}
            </div>
            {pagination}
          </section>
        </section>
    )
}

export default Travellerpage;
