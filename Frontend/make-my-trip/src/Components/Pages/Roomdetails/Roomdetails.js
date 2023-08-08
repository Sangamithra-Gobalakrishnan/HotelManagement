import React, { useState, useEffect } from 'react';
import './Roomdetails.css';
import 'react-responsive-carousel/lib/styles/carousel.min.css';
import { Carousel } from 'react-responsive-carousel';
import jsPDF from 'jspdf';
import { useParams } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const Roomdetails = () => {
  var { id } = useParams();

  const [amenities, setAmenities] = useState([]);
  const [newAmenity, setNewAmenity] = useState('');
  const [images, setImages] = useState([]);
  const [roomsData, setRoomsData] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const roomsPerPage = 3;
  const [selectedRooms, setSelectedRooms] = useState([]);
  const [isBookingPopupOpen, setBookingPopupOpen] = useState(false);
  const [hotel, setHotel] = useState(null);
  const [bookingDetails, setBookingDetails] = useState({
    startDate: '',
    endDate: '',
    adultCount: 1,
    childrenCount: 0,
  });

  const totalPages = Math.ceil(roomsData.length / roomsPerPage);

  useEffect(() => {
    if (id) {
      const fetchRoomsData = async () => {
        try {
          const response = await fetch(`http://localhost:5252/api/Room/FetchRoomsByHotelId`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({ hotelId: id }),
          });
          const jsonData = await response.json();
          setRoomsData(jsonData);
        } catch (error) {
          console.error('Error fetching room details:', error);
        }
      };

      fetchRoomsData();
    }
  }, [id]);

  useEffect(() => {
    if (id) {
      const fetchHotelDetails = async () => {
        try {
          const response = await fetch(`http://localhost:5252/api/Hotel/FetchHotelById`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({ hotelId: id }),
          });
          const jsonData = await response.json();
          setHotel(jsonData);
        } catch (error) {
          console.error('Error fetching hotel details:', error);
        }
      };

      fetchHotelDetails();
    }
  }, [id]);

  useEffect(() => {
    if (id) {
      const fetchAmenities = async () => {
        try {
          const response = await fetch(`http://localhost:5252/api/Amenity/FetchAmenitiesByHotelId`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({ hotelId: id }),
          });
          const jsonData = await response.json();
          setAmenities(jsonData);
        } catch (error) {
          console.error('Error fetching amenities:', error);
        }
      };

      fetchAmenities();
    }
  }, [id]);

  useEffect(() => {
    if (id) {
      const fetchImages = async () => {
        try {
          const response = await fetch(`http://localhost:5252/api/Images/FetchImagesByHotelId`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({ hotelId: id }),
          });
          const jsonData = await response.json();
          setImages(jsonData); // Assuming the response is an array of image URLs
        } catch (error) {
          console.error('Error fetching images:', error);
        }
      };

      fetchImages();
    }
  }, [id]);

  const openBookingPopup = () => {
    setBookingPopupOpen(true);
  };

  const closeBookingPopup = () => {
    setBookingPopupOpen(false);
  };


  const handleSelectRoom = (roomId) => {
    if (selectedRooms.length === 0) {
      setSelectedRooms([roomId]);
    } else {
      setSelectedRooms([roomId]);
    }
  };

  const handleConfirmBooking = () => {
    if (selectedRooms.length === 0) {
      alert('Please select at least one room for booking.');
      return;
    }
  
    if (!bookingDetails.startDate || !bookingDetails.endDate) {
      alert('Please select start and end dates for the booking.');
      return;
    }
  
    // Validate that start date is before end date
    const startDate = new Date(bookingDetails.startDate);
    const endDate = new Date(bookingDetails.endDate);
    if (startDate >= endDate) {
      alert('End date must be after start date.');
      return;
    }
  
    // Validate that guest counts are positive numbers
    if (bookingDetails.adultCount <= 0 || bookingDetails.childrenCount < 0) {
      alert('Please enter valid guest counts.');
      return;
    }
  
    const pdf = new jsPDF();
  
    selectedRooms.forEach((roomId) => {
      const room = roomsData.find((room) => room.id === roomId);
  
      const totalGuests = parseInt(bookingDetails.adultCount, 10) + parseInt(bookingDetails.childrenCount, 10);
      if (totalGuests !== room.capacity) {
        toast.warning(`Total guests (${totalGuests}) should match room capacity (${room.capacity}). Please adjust the number of adults and children.`, { autoClose: 5000 });
        return;
      }
  
      pdf.text('Booking Confirmation', 10, 10);
      pdf.text(`Hotel Name: ${hotel.name}`, 10, 20);
      pdf.text(`Room ID: ${room.id}`, 10, 30);
      pdf.text(`Start Date: ${bookingDetails.startDate}`, 10, 40);
      pdf.text(`End Date: ${bookingDetails.endDate}`, 10, 50);
      pdf.text(`Room Type: ${room.roomType}`, 10, 60);
      pdf.text(`Price: ${room.price}`, 10, 70);
      const gst = room.price * 0.18;
      pdf.text(`GST: ${gst}`, 10, 80);
      const total = room.price + gst;
      pdf.text(`Total: ${total}`, 10, 90);
    });
  
    pdf.save('booking_confirmation.pdf');
    closeBookingPopup();
  };
  

  if (!hotel) {
    return <div>Loading...</div>;
  }

  return (
    <div className="hotel-details-container container-hotel">
      <div className="left-side">
        <div className="carousel-room">
          <Carousel>
            {images.map((image, index) => (
              <div key={index}>
                <img src={image.imageUrl} alt={`Image ${index + 1}`} />
              </div>
            ))}
          </Carousel>
        </div>

        <div className="hotel-location">
          <p>Country: {hotel.country}</p>
          <p>City: {hotel.city}</p>
          <p>Number of Rooms: {hotel.numberOfRooms}</p>
          <p>Contact: {hotel.contactNumber}</p>
          <p>Email: {hotel.email}</p>
          <p>Price Range: {hotel.minimumPriceRange} - {hotel.maximumPriceRange}</p>
        </div>
      </div>

      <div className="right-side">
        <div className="hotel-name">
          <span>
            <h2>
              {hotel.name}
            </h2>
          </span>
        </div>

        <div className="amenities">
          <h2>Amenities</h2>
          <div className="amenities-container">
            {amenities.map((amenity, index) => (
              <div key={index} className="amenity-item">
                <span>{amenity.amenityTypeOrImage}</span>
              </div>
            ))}
          </div>
        </div>

        <div className="rooms">
          <h2>Rooms Available</h2>
          <table>
            <thead>
              <tr>
                <th>Select</th>
                <th>Room ID</th>
                <th>Type</th>
                <th>Price</th>
                <th>Capacity</th>
              </tr>
            </thead>
            <tbody>
              {roomsData
                .slice((currentPage - 1) * roomsPerPage, currentPage * roomsPerPage)
                .map((room) => (
                  <tr key={room.id}>
                    <td>
                      <input
                        type="radio"
                        checked={selectedRooms.includes(room.id)}
                        onChange={() => handleSelectRoom(room.id)}
                      />
                    </td>
                    <td>{room.id}</td>
                    <td>{room.roomType}</td>
                    <td>{room.price}</td>
                    <td>{room.capacity}</td>
                  </tr>
                ))}
            </tbody>
          </table>
          <div className="pagination">
            <button
              className="btn"
              onClick={() => setCurrentPage(currentPage - 1)}
              disabled={currentPage === 1}
            >
              Previous
            </button>
            <button
              className="btn"
              onClick={() => setCurrentPage(currentPage + 1)}
              disabled={currentPage === totalPages}
            >
              Next
            </button>
            <button className="btn" onClick={openBookingPopup}>
              Book
            </button>
          </div>
        </div>

        {isBookingPopupOpen && (
          <div className="booking-popup">
            <div className="booking-content">
              <h2>Book Rooms</h2>
              <div className="user-details">
                <label>Start Date:
                <input
                  type="date"
                  value={bookingDetails.startDate}
                  onChange={(e) =>
                    setBookingDetails({ ...bookingDetails, startDate: e.target.value })
                  }
                />
                </label>
                <label>End Date:
                <input
                  type="date"
                  value={bookingDetails.endDate}
                  onChange={(e) =>
                    setBookingDetails({ ...bookingDetails, endDate: e.target.value })
                  }
                />
                </label>
                <label>
                  Adult Count:
                  <input
                    type="number"
                    value={bookingDetails.adultCount}
                    onChange={(e) =>
                      setBookingDetails({ ...bookingDetails, adultCount: e.target.value })
                    }
                  />
                </label>
                <label>
                  Children Count:
                  <input
                    type="number"
                    value={bookingDetails.childrenCount}
                    onChange={(e) =>
                      setBookingDetails({ ...bookingDetails, childrenCount: e.target.value })
                    }
                  />
                </label>
              </div>
              <ul>
                {selectedRooms.map((roomId) => (
                  <li key={roomId}>Room {roomId}</li>
                ))}
              </ul>
              <button onClick={handleConfirmBooking}>Confirm Booking</button>
              <button onClick={closeBookingPopup}>Cancel</button>
            </div>
          </div>
        )}

        <ToastContainer />
      </div>
    </div>
  );
};

export default Roomdetails;
