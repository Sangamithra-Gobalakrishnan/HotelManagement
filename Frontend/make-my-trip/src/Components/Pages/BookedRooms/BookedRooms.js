import React, { useEffect, useState } from "react";
import { AiFillDelete } from 'react-icons/ai';
import {MdOutlineTravelExplore} from "react-icons/md";
import {AiFillCloseCircle} from "react-icons/ai";
import {TbGridDots} from "react-icons/tb";
import Aos from 'aos';
import 'aos/dist/aos.css';
import { useNavigate } from "react-router-dom";

import "./BookedRooms.css"; 

const BookedRooms = () => {
    const [bookedHotels, setBookedHotels] = useState([]);
    const [hotelImages, setHotelImages] = useState([]);

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
    
    const userId = sessionStorage.getItem("userId"); 
    
    const fetchData = async () => {
        try {
            const response = await fetch(`http://localhost:5130/api/Booking/FetchBookingByUserId`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ userId })
            });
            const jsonData = await response.json();
            setBookedHotels(jsonData);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };
    
    const fetchHotelImages = async () => {
        const imagePromises = bookedHotels.map(async (booking) => {
            try {
                const response = await fetch(`http://localhost:5252/api/Image/FetchImagesByHotelId`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ hotelId: booking.hotelId })
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
    }, [userId]);

    useEffect(() => {
        fetchHotelImages().then((imageUrls) => {
            setHotelImages(imageUrls); 
        });

        Aos.init({ duration: 2000 });
    }, [bookedHotels]);
    
    const cancelBooking = async (bookingId) => {
        try {
            const response = await fetch(`http://localhost:5130/api/Booking/Cancel`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ userId, bookingId })
            });
            
            if (response.ok) {
                // Successfully canceled, update state or show a confirmation message
                const updatedBookedHotels = bookedHotels.filter(booking => booking.id !== bookingId);
                setBookedHotels(updatedBookedHotels);
            } else {
                console.error('Error canceling booking:', response.statusText);
            }
        } catch (error) {
            console.error('Error canceling booking:', error);
        }
    };
    

    const navigate = useNavigate();

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
                     <a href="/travllerPage" className="navLink">Book Hotel</a>
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
            <div className="booked-rooms-title">
                <h3 data-aos="fade-right" className="title-text">
                    Your Bookings
                </h3>
            </div>

            <div className="booked-rooms-container grid" data-aos="fade-up">
                {bookedHotels.map((booking, index) => (
                    <div key={index} className="single-destination-card">
                        <div className="card-info">
                            <h2>Hotel ID: {booking.hotelId}</h2>
                            <h3>Room ID: {booking.roomId}</h3>
                            <h4>
                                Start Date :
                                {new Date(booking.startDate).toLocaleDateString('en-US', {
                                    year: 'numeric',
                                    month: 'short',
                                    day: 'numeric'
                                })}
                            </h4>
                            <h4>
                                End Date :
                                {new Date(booking.endDate).toLocaleDateString('en-US', {
                                    year: 'numeric',
                                    month: 'short',
                                    day: 'numeric'
                                })}
                            </h4>
                            <h4>Child Count: {booking.childCount}</h4>
                            <h4>Adult Count: {booking.adultCount}</h4>

                            <button className="cancel-button flex" onClick={() => cancelBooking(booking.id)}>
                                CANCEL <AiFillDelete className="icon" />
                            </button>
                        </div>
                    </div>
                ))}
            </div>
        </section>
    </section>
    );
}

export default BookedRooms;
