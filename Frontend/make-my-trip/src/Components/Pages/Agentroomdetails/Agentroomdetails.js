import React, { useState, useEffect } from 'react';
import './Agentroomdetails.css';
import 'react-responsive-carousel/lib/styles/carousel.min.css';
import { Carousel } from 'react-responsive-carousel';
import { useParams } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const Agentroomdetails = () => {
  var { id } = useParams();

  const [amenities, setAmenities] = useState([]);
  const [newAmenity, setNewAmenity] = useState('');
  const [images, setImages] = useState([]);
  const [roomsData, setRoomsData] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const roomsPerPage = 5;
  const [hotel, setHotel] = useState(null);

  const [showRoomPopup, setShowRoomPopup] = useState(false); 

  const totalPages = Math.ceil(roomsData.length / roomsPerPage);

  const addAmenity = () => {
    if (newAmenity) {
      setAmenities([...amenities, newAmenity]);
      setNewAmenity('');
      closeAmenityPopup();
    }
  };

  const openAmenityPopup = () => {
    setShowAmenityPopup(true);
  };

  const closeAmenityPopup = () => {
    setShowAmenityPopup(false);
    setNewAmenity('');
  };

  const handleAmenityInputChange = (e) => {
    setNewAmenity(e.target.value);
  };

  const [showAmenityPopup, setShowAmenityPopup] = useState(false);

  const [roomInputs, setRoomInputs] = useState({
    roomId: '',
    price: '',
    capacity: '',
    roomType: '',
  });

  const addRoom = () => {
    if (
      roomInputs.roomId &&
      roomInputs.price &&
      roomInputs.capacity &&
      roomInputs.roomType
    ) {
      setRoomsData([...roomsData, { ...roomInputs }]);
      closeRoomPopup();
    }
  };

  const openRoomPopup = () => {
    setShowRoomPopup(true);
  };

  const closeRoomPopup = () => {
    setShowRoomPopup(false);
    setRoomInputs({
      roomId: '',
      price: '',
      capacity: '',
      roomType: '',
    });
  };

  const handleRoomInputChange = (e) => {
    const { name, value } = e.target;
    setRoomInputs((prevInputs) => ({
      ...prevInputs,
      [name]: value,
    }));
  };

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
          setImages(jsonData);
        } catch (error) {
          console.error('Error fetching images:', error);
        }
      };

      fetchImages();
    }
  }, [id]);

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
              <label className="file-upload-label">
                <input type="file" className="file-upload-input" />
                <button className="btn">Upload Image</button>
              </label>
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

        <div className="add-amenity">
          <button onClick={openAmenityPopup} className="btn">
            Add Amenity
          </button>
        </div>

        <div className="add-room">
          <button onClick={openRoomPopup} className="btn">
            Add Room
          </button>
        </div>

        <div className="rooms">
          <h2>Rooms Available</h2>
          <table>
            <thead>
              <tr>
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
          </div>
        </div>
        <ToastContainer />
      </div>

      {showAmenityPopup && (
        <div className="popup-overlay">
          <div className="popup-content">
            <h2>Add Amenity</h2>
            <input
              type="text"
              placeholder="Amenity Name"
              value={newAmenity}
              onChange={handleAmenityInputChange}
              className="popup-input"
            />
            <button onClick={addAmenity} className="popup-button">
              Add
            </button>
            <button onClick={closeAmenityPopup} className="popup-button btn-secondary">
              Cancel
            </button>
          </div>
        </div>
      )}


      {showRoomPopup && (
        <div className="popup-overlay">
          <div className="popup-content">
            <h2>Add Room</h2>
            <input
              type="text"
              placeholder="Room ID"
              name="roomId"
              value={roomInputs.roomId}
              onChange={handleRoomInputChange}
              className="popup-input"
            />
            <label>Price</label>
            <input
              type="text"
              placeholder="Price"
              name="price"
              value={roomInputs.price}
              onChange={handleRoomInputChange}
              className="popup-input"
            />
             <label>Capacity</label>
            <input
              type="text"
              placeholder="Capacity"
              name="price"
              value={roomInputs.capacity}
              onChange={handleRoomInputChange}
              className="popup-input"
            />
             <label>Room Type</label>
            <input
              type="text"
              placeholder="Room Type"
              name="price"
              value={roomInputs.roomType}
              onChange={handleRoomInputChange}
              className="popup-input"
            />
            <button onClick={addRoom} className="popup-button">
              Add
            </button>
            <button onClick={closeRoomPopup} className="popup-button btn-secondary">
              Cancel
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default Agentroomdetails;
