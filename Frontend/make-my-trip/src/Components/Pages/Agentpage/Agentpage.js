import React, { useEffect, useState } from "react";
import { MdOutlineTravelExplore } from "react-icons/md";
import { AiFillCloseCircle } from "react-icons/ai";
import { TbGridDots } from "react-icons/tb";
import { HiOutlineClipboardCheck, HiOutlineLocationMarker } from 'react-icons/hi';
import AddHotel from "../AddHotel/Addhotel";
import Aos from 'aos';
import 'aos/dist/aos.css';
import { useNavigate } from "react-router-dom";
import './Agentpage.css';

import defaultHotelImage1 from './img3.jpg';
import defaultHotelImage2 from './img1.jpg';
import defaultHotelImage3 from './img2.jpg';
import defaultHotelImage4 from './img4.jpg';
import defaultHotelImage5 from './img5.jpg';

const Agentpage = () => {
    const [hotels, setHotels] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [hotelsPerPage] = useState(6);
    const [hotelImages, setHotelImages] = useState([]);
    const [isAddHotelPopupOpen, setIsAddHotelPopupOpen] = useState(false);

    const handleAddHotelClick = () => {
        setIsAddHotelPopupOpen(true);
    };

    const handleCloseAddHotelPopup = () => {
        setIsAddHotelPopupOpen(false);
    };

    const fetchHotelImages = async () => {
        const newImages = [];
        for (const hotel of hotels) {
            try {
                const response = await fetch("http://localhost:5252/api/Image/FetchImagesByHotelId", {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ hotelId: hotel.hotelId })
                });
                const jsonData = await response.json();
                if (jsonData.length > 0) {
                    newImages.push(jsonData[0]);
                } else {
                    newImages.push(null);
                }
            } catch (error) {
                console.error('Error fetching hotel images:', error);
                newImages.push(null);
            }
        }
        return newImages;
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(`http://localhost:5252/api/Hotel/FetchAllHotels?page=${currentPage}&perPage=${hotelsPerPage}`);
                const jsonData = await response.json();
                setHotels(jsonData);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
        fetchHotelImages().then((imageData) => {
            setHotelImages(imageData);
        });
    }, [currentPage]);

    useEffect(() => {
        Aos.init({ duration: 2000 });
    }, []);

    const navigate = useNavigate();

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
                <div className="navBar">
                    <ul className="navLists flex">
                        <li className="navItem">
                            <a href="#" className="navLink">Hotels</a>
                        </li>
                        <li className="navItem">
                            <a href="/addHotel" className="navLink" onClick={handleAddHotelClick}>Add Hotel</a>
                        </li>
                        <button className="btn">
                            <a href="/">LOGOUT</a>
                        </button>
                    </ul>
                    <div className="closeNavbar">
                        <AiFillCloseCircle className="icon"/>
                    </div>
                </div>
                <div className="toggleNavbar">
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
                                    <button className="btn flex" onClick={() => { navigate(`/agentRoomDetails/${hotel.id}`) }}>
                                        DETAILS <HiOutlineClipboardCheck className="icon" />
                                    </button>
                                </div>
                            </div>
                        ))}
                </div>
                {/* Pagination */}
                <div className="pagination-hotel">
                    {Array.from({ length: Math.ceil(hotels.length / hotelsPerPage) }, (_, index) => (
                        <button
                            key={index}
                            onClick={() => setCurrentPage(index + 1)}
                            className={currentPage === index + 1 ? 'active button' : 'button'}
                        >
                            {index + 1}
                        </button>
                    ))}
                </div>
            </section>
            {isAddHotelPopupOpen && (
                <AddHotel onClose={handleCloseAddHotelPopup} />
            )}
        </section>
    )
}

export default Agentpage;
