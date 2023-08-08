import React, { useState, useEffect } from 'react';
import './Hotel.css';
import { HiOutlineClipboardCheck, HiOutlineLocationMarker } from 'react-icons/hi';
import { Link, useNavigate } from 'react-router-dom';  // Import Link from react-router-dom

import Aos from 'aos';
import 'aos/dist/aos.css';
import Roomdetails from '../Roomdetails/Roomdetails';

const Hotel = () => {
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

  var navigateYoRoom=(data)=>{
    navigate('/roomDetails/'+{data});
  }

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

  return (
    <section className="main container-hotel section">
      <div className="secTitle">
        <h3 data-aos="fade-right" className="title">
          Most visited Destinations
        </h3>
      </div>

      <div className="secContent grid" data-aos="fade-up">
        {hotels.map((hotel, index) => (
          <div key={index} className="singleDestination">
            <div className="imageDiv">
              <img className="imgcs" src={hotelImages[index]} alt="Hotel" />
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
                    {hotel.minimumPriceRange}-{hotel.maximumPriceRange}
                  </h5>
                </div>
              </div>

              <div className="desc">
                <p>{hotel.description} {hotel.id}</p>
              </div>

              <button className="btn flex" onClick={()=>navigateYoRoom(hotel.id)}>
                DETAILS <HiOutlineClipboardCheck className="icon" />
                </button>
            </div>

          </div>
        ))}
      </div>
      {pagination}
    </section>
  );
};

export default Hotel;
